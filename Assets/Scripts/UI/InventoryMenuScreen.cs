using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbandonMine.Inventory;
using System;

namespace AbandonMine.UI
{
    public class InventoryMenuScreen : MenuScreen
    {
        [SerializeField]
        private InventoryScreenItem inventoryItemPrefab;
        
        [SerializeField][Range(1, 64)]
        private int maxItemCount = 10;

        [SerializeField]
        private RectTransform inventoryViewportContent;

        private List<InventoryScreenItem> screenItemPool;

        private void OnEnable()
        {
            OnShowEvent += OnShowEventHandler;
            OnHideEvent += OnHideEventHandler;

            PlayerInventory.OnInventoryListUpdatedEvent += OnInventoryListUpdatedEventHandler;
        }

        private void OnDisable()
        {
            OnShowEvent -= OnShowEventHandler;
            OnHideEvent -= OnHideEventHandler;

            PlayerInventory.OnInventoryListUpdatedEvent -= OnInventoryListUpdatedEventHandler;
        }

        private void Start()
        {
            screenItemPool = new List<InventoryScreenItem>(maxItemCount);
            for (int i=0; i<maxItemCount; i++)
            {
                InventoryScreenItem screenItem = GameObject.Instantiate<InventoryScreenItem>(inventoryItemPrefab, inventoryViewportContent);
                screenItem.SetDisplayName("ITEM");
                screenItem.SetIcon(null);
                screenItem.gameObject.SetActive(false);

                screenItemPool.Add(screenItem);
            }
        }

        private void OnShowEventHandler()
        {
            PlayerInventory.Instance.UpdateItemList();
        }

        private void OnHideEventHandler()
        {
            HideAllScreenItemVisuals();
        }


        private void OnInventoryListUpdatedEventHandler()
        {
            //Debug.Log("Inventory list updated");
            StartCoroutine(RegenerateScreenItemVisuals());
        }

        private IEnumerator RegenerateScreenItemVisuals()
        {
            var itemList = PlayerInventory.Instance.GetItemList();
            
            var waitForSeconds = new WaitForSeconds(0.5f);

            int itemIndex = 0;
            foreach (var screenItem in screenItemPool)
            {
                if (itemIndex < itemList.Count)
                {
                    screenItem.gameObject.SetActive(false);
                    screenItem.SetDisplayName(itemList[itemIndex].displayName);
                    screenItem.SetIcon(itemList[itemIndex].icon);
                    screenItem.ShowItem();
                }
                else
                {
                    screenItem.HideItem();
                }

                yield return waitForSeconds;

                itemIndex++;
            }
        }

        private void HideAllScreenItemVisuals()
        {
            foreach (var screenItem in screenItemPool)
            {
                screenItem.gameObject.SetActive(false);
            }
        }
    }
}
