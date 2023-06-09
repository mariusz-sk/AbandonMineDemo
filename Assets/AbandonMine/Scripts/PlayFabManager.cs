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
        public delegate void OnUserDisplayNameRetrievedHandler(string userDisplayName);
        public delegate void OnChangedUserDisplayNameHandler(bool isSuccess);
        public delegate void OnUserDataRetrievedHandler(Dictionary<string, string> retrievedData);
        public delegate void OnChangedUserDataHandler(bool isSuccess);

        public static event OnLoggedInHandler OnLoggedInEvent;
        public static event OnInventoryRetrievedHandler OnInventoryRetrievedEvent;
        public static event OnCurrencyRetrievedHandler OnCurrencyRetrievedEvent;
        public static event OnCurrencyAddedHandler OnCurrencyAddedEvent;
        public static event OnUserDisplayNameRetrievedHandler OnUserDisplayNameRetrievedEvent;
        public static event OnChangedUserDisplayNameHandler OnChangedUserDisplayNameEvent;
        public static event OnUserDataRetrievedHandler OnUserDataRetrievedEvent;
        public static event OnChangedUserDataHandler OnChangedUserDataEvent;

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

        public void GetUserDisplayName()
        {
            PlayFabClientAPI.GetAccountInfo(
                new GetAccountInfoRequest(),
                result =>
                {
                    Debug.Log($"GetUserDisplayName succeded!");
                    OnUserDisplayNameRetrievedEvent?.Invoke(result.AccountInfo.TitleInfo.DisplayName);
                },
                error =>
                {
                    Debug.Log($"GetUserDisplayName failed!\n{error.ErrorMessage}");
                    OnUserDisplayNameRetrievedEvent?.Invoke(null);
                });
        }

        public void ChangeUserDisplayName(string newDisplayName)
        {
            PlayFabClientAPI.UpdateUserTitleDisplayName(
                new UpdateUserTitleDisplayNameRequest()
                {
                    DisplayName = newDisplayName
                },
                result =>
                {
                    Debug.Log($"ChangeUserDisplayName succeded!");
                    OnChangedUserDisplayNameEvent?.Invoke(true);
                },
                error =>
                {
                    Debug.Log($"ChangeUserDisplayName failed!\n{error.ErrorMessage}");
                    OnChangedUserDisplayNameEvent?.Invoke(false);
                });
        }

        public void GetUserData(List<string> paramNames)
        {
            PlayFabClientAPI.GetUserData(
                new GetUserDataRequest()
                {
                    Keys = paramNames
                },
                result =>
                {
                    Debug.Log($"GetUserData succeded!");
                    var retrievedData = new Dictionary<string, string>();
                    foreach (var item in result.Data)
                    {
                        retrievedData.Add(item.Key, item.Value.Value);
                    }

                    OnUserDataRetrievedEvent?.Invoke(retrievedData);
                },
                error =>
                {
                    Debug.Log($"GetUserDisplayName failed!\n{error.ErrorMessage}");
                    OnUserDataRetrievedEvent?.Invoke(null);
                });
        }

        public void ChangeUserData(Dictionary<string, string> userData)
        {
            PlayFabClientAPI.UpdateUserData(
                new UpdateUserDataRequest()
                {
                    Data = userData
                },
                result =>
                {
                    Debug.Log($"ChangeUserData succeded!");
                    OnChangedUserDataEvent?.Invoke(true);
                },
                error =>
                {
                    Debug.Log($"ChangeUserData failed!\n{error.ErrorMessage}");
                    OnChangedUserDataEvent?.Invoke(false);
                });
        }
    }
}
