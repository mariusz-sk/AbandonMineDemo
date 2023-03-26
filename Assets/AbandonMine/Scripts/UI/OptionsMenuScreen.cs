using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AbandonMine.Audio;

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

        [SerializeField]
        private Slider masterVolumeSlider;

        [SerializeField]
        private Slider musicVolumeSlider;

        [SerializeField]
        private Slider effectsVolumeSlider;

        

        // For now this is here (must be moved to better place)
        private const string CHAR_CLASS_PARAM_NAME = "class";
        private const string CHAR_GENDER_PARAM_NAME = "gender";

        public void OnSaveButtonClicked()
        {
            AudioManager.Instance.SaveSettings();

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

            masterVolumeSlider.onValueChanged.AddListener(UpdateMasterVolume);
            musicVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
            effectsVolumeSlider.onValueChanged.AddListener(UpdateEffectsVolume);

            PlayFabManager.OnUserDisplayNameRetrievedEvent += OnUserDisplayNameRetrievedHandler;
            PlayFabManager.OnChangedUserDisplayNameEvent += OnChangedUserDisplayNameHandler;

            PlayFabManager.OnUserDataRetrievedEvent += OnUserDataRetrievedHandler;
            PlayFabManager.OnChangedUserDataEvent += OnChangedUserDataHandler;
        }


        private void OnDisable()
        {
            OnShowEvent -= OnShowEventHandler;
            OnHideEvent -= OnHideEventHandler;

            masterVolumeSlider.onValueChanged.RemoveListener(UpdateMasterVolume);
            musicVolumeSlider.onValueChanged.RemoveListener(UpdateMusicVolume);
            effectsVolumeSlider.onValueChanged.RemoveListener(UpdateEffectsVolume);

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

            masterVolumeSlider.value = Mathf.Pow(10.0f, AudioManager.Instance.GetMasterVolume() / 20.0f);
            musicVolumeSlider.value = Mathf.Pow(10.0f, AudioManager.Instance.GetMusicVolume() / 20.0f);
            effectsVolumeSlider.value = Mathf.Pow(10.0f, AudioManager.Instance.GetEffectsVolume() / 20.0f);
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

        private void UpdateMasterVolume(float value)
        {
            AudioManager.Instance.SetMasterVolume(Mathf.Log10(value) * 20.0f);
        }

        private void UpdateMusicVolume(float value)
        {
            AudioManager.Instance.SetMusicVolume(Mathf.Log10(value) * 20.0f);
        }

        private void UpdateEffectsVolume(float value)
        {
            AudioManager.Instance.SetEffectsVolume(Mathf.Log10(value) * 20.0f);
        }
    }
}
