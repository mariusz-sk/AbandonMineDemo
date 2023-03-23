using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbandonMine.Inventory;

namespace AbandonMine.UI
{
    public class InventoryMenuScreen : MenuScreen
    {
        [SerializeField]
        private InventoryScreenItem inventoryItemPrefab;

        [SerializeField]
        private RectTransform inventoryViewportContent;

        private void OnEnable()
        {
            OnShowEvent += OnShowEventHandler;
            PlayerInventory.OnInventoryListUpdatedEvent += OnInventoryListUpdatedEventHandler;
        }


        private void OnDisable()
        {
            OnShowEvent -= OnShowEventHandler;
            PlayerInventory.OnInventoryListUpdatedEvent -= OnInventoryListUpdatedEventHandler;
        }

        private void OnShowEventHandler()
        {
            PlayerInventory.Instance.UpdateItemList();
        }

        private void OnInventoryListUpdatedEventHandler()
        {
            Debug.Log("Inventory list updated");
            RegenerateItemListVisuals();
        }

        private void RegenerateItemListVisuals()
        {
            DestroyItemListVisuals();

            var itemList = PlayerInventory.Instance.GetItemList();

            foreach (var item in itemList)
            {
                InventoryScreenItem screenItem = GameObject.Instantiate<InventoryScreenItem>(inventoryItemPrefab, inventoryViewportContent);
                screenItem.SetDisplayName(item.displayName);
                screenItem.SetIcon(item.icon);
            }
        }

        private void DestroyItemListVisuals()
        {
            if (inventoryViewportContent == null)
                return;

            foreach (Transform child in inventoryViewportContent.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
