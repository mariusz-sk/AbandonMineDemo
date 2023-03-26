using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AbandonMine.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer audioMixer;

        private const string MIXER_PARAM_MASTER_VOLUME = "MasterVolume";
        private const string MIXER_PARAM_MUSIC_VOLUME = "MusicVolume";
        private const string MIXER_PARAM_EFFECTS_VOLUME = "EffectsVolume";

        private const string PREFS_MASTER_KEY = "MasterVolume";
        private const string PREFS_MUSIC_KEY = "MusicVolume";
        private const string PREFS_EFFECTS_KEY = "EffectsVolume";

        public static AudioManager Instance { get; private set; }

        public float GetMasterVolume()
        {
            if (audioMixer.GetFloat(MIXER_PARAM_MASTER_VOLUME, out float value))
                return value;
            else
                return 0.0f;
        }

        public void SetMasterVolume(float value)
        {
            audioMixer.SetFloat(MIXER_PARAM_MASTER_VOLUME, value);
        }

        public float GetMusicVolume()
        {
            if (audioMixer.GetFloat(MIXER_PARAM_MUSIC_VOLUME, out float value))
                return value;
            else
                return 0.0f;
        }

        public void SetMusicVolume(float value)
        {
            audioMixer.SetFloat(MIXER_PARAM_MUSIC_VOLUME, value);
        }

        public float GetEffectsVolume()
        {
            if (audioMixer.GetFloat(MIXER_PARAM_EFFECTS_VOLUME, out float value))
                return value;
            else
                return 0.0f;
        }

        public void SetEffectsVolume(float value)
        {
            audioMixer.SetFloat(MIXER_PARAM_EFFECTS_VOLUME, value);
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetFloat(PREFS_MASTER_KEY, GetMasterVolume());
            PlayerPrefs.SetFloat(PREFS_MUSIC_KEY, GetMusicVolume());
            PlayerPrefs.SetFloat(PREFS_EFFECTS_KEY, GetEffectsVolume());
            PlayerPrefs.Save();
        }

        public void LoadSettings()
        {
            SetMasterVolume(PlayerPrefs.GetFloat(PREFS_MASTER_KEY, 0.5f));
            SetMusicVolume(PlayerPrefs.GetFloat(PREFS_MUSIC_KEY, 0.5f));
            SetEffectsVolume(PlayerPrefs.GetFloat(PREFS_EFFECTS_KEY, 0.5f));
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            LoadSettings();
        }
    }
}
