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

        public delegate void GetInventoryItemListCallback(List<PlayFabInventoryItemData> itemList);

        public bool IsLogged => isLogged;


        private static PlayFabManager instance;
        private bool isLogged = false;
        private string playFabId;

        public void TryLogin()
        {
            var request = new LoginWithCustomIDRequest { CustomId = "TestPlayerCustomId", CreateAccount = true };
            PlayFabClientAPI.LoginWithCustomID(request,
                result =>
                {
                    Debug.Log("Logged In to PlayFab!");
                    playFabId = result.PlayFabId;
                    isLogged = true;
                },
                error =>
                {
                    Debug.Log("Login to PlayFab failed");
                });
        }
        
        
        public void GetInventoryItemList(GetInventoryItemListCallback callback)
        {
            PlayFabClientAPI.GetUserInventory(
                new GetUserInventoryRequest(),
                result =>
                {
                    if (callback != null)
                    {
                        var playFabItemList = new List<PlayFabInventoryItemData>();

                        foreach (var itemInstance in result.Inventory)
                        {
                            var playFabItem = new PlayFabInventoryItemData
                            {
                                itemId = itemInstance.ItemId,
                                itemClass = itemInstance.ItemClass,
                                itemDisplayName = itemInstance.DisplayName
                            };

                            playFabItemList.Add(playFabItem);
                        }

                        callback?.Invoke(playFabItemList);
                    }
                },
                error =>
                {
                    Debug.Log("GetUserInventory failed!");
                    callback?.Invoke(null);
                });
        }
        
    }
}
