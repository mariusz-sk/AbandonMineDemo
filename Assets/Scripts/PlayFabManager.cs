using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

namespace AbandonMine
{
    public class PlayFabManager
    {
        public static PlayFabManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayFabManager();
                }

                return instance;
            }
        }

        public class PlayFabInventoryItemData
        {
            public string itemId;
            public string itemClass;
            public string itemDisplayName;
        }

        public delegate void OnLoggedInHandler(bool isSuccess);
        public delegate void OnInventoryRetrievedHandler(List<PlayFabInventoryItemData> playFabInventoryItems);
        public delegate void OnCurrencyRetrievedHandler(Dictionary<string, int> currencyInfo);
        public delegate void OnCurrencyAddedHandler(bool isSuccess);

        public static event OnLoggedInHandler OnLoggedInEvent;
        public static event OnInventoryRetrievedHandler OnInventoryRetrievedEvent;
        public static event OnCurrencyRetrievedHandler OnCurrencyRetrievedEvent;
        public static event OnCurrencyAddedHandler OnCurrencyAddedEvent;

        public bool IsLogged => isLoggedIn;


        private static PlayFabManager instance;
        private bool isLoggedIn = false;
        private string playFabId;

        public void Login()
        {
            PlayFabClientAPI.LoginWithCustomID(
                new LoginWithCustomIDRequest
                {
                    CustomId = "TestPlayerCustomId",
                    CreateAccount = true
                },
                result =>
                {
                    Debug.Log("Logged In to PlayFab!");
                    playFabId = result.PlayFabId;
                    isLoggedIn = true;

                    OnLoggedInEvent?.Invoke(true);
                },
                error =>
                {
                    Debug.Log($"Login to PlayFab failed\n{error.ErrorMessage}");
                    playFabId = "";
                    isLoggedIn = false;

                    OnLoggedInEvent?.Invoke(false);
                });
        }
        
        public void GetInventory()
        {
            PlayFabClientAPI.GetUserInventory(
                new GetUserInventoryRequest(),
                result =>
                {
                    var playFabItemDataList = new List<PlayFabInventoryItemData>();

                    foreach (var itemInstance in result.Inventory)
                    {
                        var playFabItemData = new PlayFabInventoryItemData
                        {
                            itemId = itemInstance.ItemId,
                            itemClass = itemInstance.ItemClass,
                            itemDisplayName = itemInstance.DisplayName
                        };

                        playFabItemDataList.Add(playFabItemData);
                    }

                    OnInventoryRetrievedEvent?.Invoke(playFabItemDataList);
                },
                error =>
                {
                    Debug.Log("GetUserInventory failed!");
                    OnInventoryRetrievedEvent?.Invoke(null);
                });
        }

        public void GetCurrency()
        {
            PlayFabClientAPI.GetUserInventory(
                new GetUserInventoryRequest(),
                result =>
                {
                    OnCurrencyRetrievedEvent?.Invoke(result.VirtualCurrency);
                },
                error =>
                {
                    Debug.Log($"GetCurrency failed!\n{error.ErrorMessage}");
                    OnCurrencyRetrievedEvent?.Invoke(null);
                });
        }

        public void AddCurrency(string currency, int amount)
        {
            PlayFabClientAPI.AddUserVirtualCurrency(
                new AddUserVirtualCurrencyRequest
                {
                    VirtualCurrency = currency,
                    Amount = amount
                },
                result =>
                {
                    OnCurrencyAddedEvent?.Invoke(true);
                },
                error =>
                {
                    Debug.Log($"AddCurrency failed!\n{error.ErrorMessage}");
                    OnCurrencyAddedEvent?.Invoke(false);
                });
        }
    }
}
