using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AbandonMine.UI
{
    public class OptionsMenuScreen : MenuScreen
    {
        [SerializeField]
        private TMP_InputField userDisplayNameInputField;


        public void OnSaveDisplayNameButtonClicked()
        {
            if (userDisplayNameInputField == null)
                return;

            PlayFabManager.Instance.ChangeUserDisplayName(userDisplayNameInputField.text);
        }


        private void OnEnable()
        {
            OnShowEvent += OnShowEventHandler;
            OnHideEvent += OnHideEventHandler;
            PlayFabManager.OnUserDisplayNameRetrievedEvent += OnUserDisplayNameRetrievedHandler;
            PlayFabManager.OnChangedUserDisplayNameEvent += OnChangedUserDisplayNameHandler;
        }


        private void OnDisable()
        {
            OnShowEvent -= OnShowEventHandler;
            OnHideEvent -= OnHideEventHandler;
            PlayFabManager.OnUserDisplayNameRetrievedEvent -= OnUserDisplayNameRetrievedHandler;
            PlayFabManager.OnChangedUserDisplayNameEvent -= OnChangedUserDisplayNameHandler;
        }

        private void OnShowEventHandler()
        {
            PlayFabManager.Instance.GetUserDisplayName();
        }

        private void OnHideEventHandler()
        {
            
        }

        private void OnUserDisplayNameRetrievedHandler(string userDisplayName)
        {
            if (userDisplayNameInputField == null)
                return;

            userDisplayNameInputField.text = userDisplayName;
        }

        private void OnChangedUserDisplayNameHandler(bool isSuccess)
        {
            PlayFabManager.Instance.GetUserDisplayName();
        }
    }
}
