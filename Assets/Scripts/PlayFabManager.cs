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

        public delegate void LoggedInHandler(bool isSuccess);
        public delegate void GetInventoryRetrievedHandler(List<PlayFabInventoryItemData> playFabItemDataList);

        public static event LoggedInHandler OnLoggedInEvent;
        public static event GetInventoryRetrievedHandler OnInventoryRetrievedEvent;

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
        
        public void GetInventoryItemList()
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
    }
}
