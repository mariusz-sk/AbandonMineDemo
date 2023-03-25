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

        [SerializeField]
        private TMP_InputField characterClassNameInputField;

        [SerializeField]
        private TMP_InputField characterGenderInputField;

        // For now this is here (must be moved to better place)
        private static readonly string CHAR_CLASS_PARAM_NAME = "class";
        private static readonly string CHAR_GENDER_PARAM_NAME = "gender";

        public void OnSaveButtonClicked()
        {
            PlayFabManager.Instance.ChangeUserDisplayName(userDisplayNameInputField.text);

            var userData = new Dictionary<string, string>();
            userData.Add(CHAR_CLASS_PARAM_NAME, characterClassNameInputField.text);
            userData.Add(CHAR_GENDER_PARAM_NAME, characterGenderInputField.text);

            PlayFabManager.Instance.ChangeUserData(userData);
        }


        private void OnEnable()
        {
            OnShowEvent += OnShowEventHandler;
            OnHideEvent += OnHideEventHandler;

            PlayFabManager.OnUserDisplayNameRetrievedEvent += OnUserDisplayNameRetrievedHandler;
            PlayFabManager.OnChangedUserDisplayNameEvent += OnChangedUserDisplayNameHandler;

            PlayFabManager.OnUserDataRetrievedEvent += OnUserDataRetrievedHandler;
            PlayFabManager.OnChangedUserDataEvent += OnChangedUserDataHandler;
        }


        private void OnDisable()
        {
            OnShowEvent -= OnShowEventHandler;
            OnHideEvent -= OnHideEventHandler;

            PlayFabManager.OnUserDisplayNameRetrievedEvent -= OnUserDisplayNameRetrievedHandler;
            PlayFabManager.OnChangedUserDisplayNameEvent -= OnChangedUserDisplayNameHandler;

            PlayFabManager.OnUserDataRetrievedEvent -= OnUserDataRetrievedHandler;
            PlayFabManager.OnChangedUserDataEvent -= OnChangedUserDataHandler;
        }

        private void OnShowEventHandler()
        {
            PlayFabManager.Instance.GetUserDisplayName();

            var paramNames = new List<string>();
            paramNames.Add(CHAR_CLASS_PARAM_NAME);
            paramNames.Add(CHAR_GENDER_PARAM_NAME);

            PlayFabManager.Instance.GetUserData(paramNames);
        }

        private void OnHideEventHandler()
        {
            userDisplayNameInputField.text = "";
            characterClassNameInputField.text = "";
            characterGenderInputField.text = "";
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

        private void OnUserDataRetrievedHandler(Dictionary<string, string> retrievedData)
        {
            characterClassNameInputField.text = retrievedData.GetValueOrDefault(CHAR_CLASS_PARAM_NAME, "");

            characterGenderInputField.text = retrievedData.GetValueOrDefault(CHAR_GENDER_PARAM_NAME, "");
        }

        private void OnChangedUserDataHandler(bool isSuccess)
        {
            PlayFabManager.Instance.GetUserData(null);
        }
    }
}
