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

                    GetInventory();
                },
                error =>
                {
                    Debug.Log("Login to PlayFab failed");
                });
        }
        
        
        public void GetInventory()
        {
            var request = new GetUserInventoryRequest();
            PlayFabClientAPI.GetUserInventory(request,
                result =>
                {
                    foreach (var item in result.Inventory)
                    {
                        Debug.Log($"Id: {item.ItemId}, class: {item.ItemClass}, name: {item.DisplayName}");
                    }
                },
                error =>
                {
                    Debug.Log("GetUserInventory failed!");
                });
        }
        
    }
}
