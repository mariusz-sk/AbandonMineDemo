using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AbandonMine.Inventory;
using System;

namespace AbandonMine.UI
{
    public class IngameMenuController : MonoBehaviour
    {
        [SerializeField]
        private InventoryMenuScreen inventoryMenuScreen;

        [SerializeField]
        private TextMeshProUGUI currencyText;

        private void OnEnable()
        {
            PlayerInventory.OnCurrencyAmountChangedEvent += OnCurrencyAmountChangedHandler;
        }

        private void OnDisable()
        {
            PlayerInventory.OnCurrencyAmountChangedEvent -= OnCurrencyAmountChangedHandler;
        }

        private void Start()
        {
            MenuScreen[] menuScreens = GetComponentsInChildren<MenuScreen>();
            foreach (var menuScreen in menuScreens)
            {
                menuScreen.gameObject.SetActive(false);
            }

            UpdateCurrencyText();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I) && inventoryMenuScreen != null)
            {
                if (inventoryMenuScreen.IsVisible)
                    inventoryMenuScreen.HideScreen();
                else
                    inventoryMenuScreen.ShowScreen();
            }
        }

        private void OnCurrencyAmountChangedHandler()
        {
            UpdateCurrencyText();
        }

        private void UpdateCurrencyText()
        {
            if (currencyText != null)
            {
                currencyText.text = $"Blue Gold = {PlayerInventory.Instance.CollectedCurrency}";
            }
        }

    }
}
