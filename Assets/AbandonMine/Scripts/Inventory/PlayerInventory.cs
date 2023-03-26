using System;
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

        public static event Action OnInventoryListUpdatedEvent;
        public static event Action OnCurrencyAmountChangedEvent;

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
            {
                CollectedCurrency += amount;
                OnCurrencyAmountChangedEvent?.Invoke();
            }
        }

        public void SubtractCurrencyAmount(int amount)
        {
            if (amount > 0)
            {
                CollectedCurrency -= amount;

                if (CollectedCurrency < 0)
                    CollectedCurrency = 0;

                OnCurrencyAmountChangedEvent?.Invoke();
            }
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
            GameManager.OnStartNewGameEvent += OnStartNewGameHandler;
            PlayFabManager.OnInventoryRetrievedEvent += OnInventoryRetrieved;
        }


        private void OnDisable()
        {
            GameManager.OnStartNewGameEvent -= OnStartNewGameHandler;
            PlayFabManager.OnInventoryRetrievedEvent -= OnInventoryRetrieved;
        }
        
        private void OnStartNewGameHandler()
        {
            CollectedCurrency = 0;
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
