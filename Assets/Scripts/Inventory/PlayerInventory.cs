using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AbandonMine.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField]
        private InventoryItemData[] itemDefinitions;


        public class InventoryItem
        {
            public string displayName;
            public Sprite icon;
        }

        public static PlayerInventory Instance { get; private set; }

        public delegate void OnInventoryListUpdatedHandler();
        public static event OnInventoryListUpdatedHandler OnInventoryListUpdatedEvent;

        public List<InventoryItem> Items { get; private set; } = new List<InventoryItem>();
        public int CollectedCurrency { get; private set; }


        public void UpdateItemList()
        {
            Items.Clear();
            PlayFabManager.Instance.GetInventory();
        }
        
        public void AddCurrencyAmount(int amount)
        {
            if (amount > 0)
                CollectedCurrency += amount;
        }

        public void SubtractCurrencyAmount(int amount)
        {
            if (amount > 0)
                CollectedCurrency -= amount;

            if (CollectedCurrency < 0)
                CollectedCurrency = 0;
        }

        private void Awake()
        {
            if (PlayerInventory.Instance == null)
            {
                PlayerInventory.Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            PlayFabManager.OnInventoryRetrievedEvent += OnInventoryRetrieved;
        }

        private void OnDisable()
        {
            PlayFabManager.OnInventoryRetrievedEvent -= OnInventoryRetrieved;
        }

        private void OnInventoryRetrieved(List<PlayFabManager.PlayFabInventoryItemData> playFabInventoryItems)
        {
            foreach (var playFabItem in playFabInventoryItems)
            {
                var itemDefinition = itemDefinitions.FirstOrDefault(item => { return string.Equals(item.ItemClass, playFabItem.itemClass); });

                Items.Add(new InventoryItem
                {
                    displayName = playFabItem.itemDisplayName,
                    icon = itemDefinition != null ? itemDefinition.InventoryIcon : null
                });
            };

            OnInventoryListUpdatedEvent?.Invoke();
        }
    }
}
