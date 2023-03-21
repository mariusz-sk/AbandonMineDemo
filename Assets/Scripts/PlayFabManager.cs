using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        private static PlayFabManager instance;

        public bool TryLogin()
        {
            bool loggedIn = false;
            var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true };
            PlayFab.PlayFabClientAPI.LoginWithCustomID(request,
                x =>
                {
                    Debug.Log("Logged In to PlayFab!");
                    loggedIn = true;
                },
                x =>
                {
                    Debug.Log("Login to PlayFab failed");
                });

            return loggedIn;
        }
    }
}
