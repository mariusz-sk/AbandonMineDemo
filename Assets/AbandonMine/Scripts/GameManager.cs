using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbandonMine.UI;
using AbandonMine.Inventory;

namespace AbandonMine
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }

        public static event Action OnStartNewGameEvent;
        public static event Action OnPauseGameplayEvent;
        public static event Action OnResumeGameplayEvent;
        public static event Action OnPlayerHasFinishedLevelEvent;

        public float LevelPlayTime;

        private bool isPaused = true;

        public void PlayerFinishedLevel()
        {
            OnPlayerHasFinishedLevelEvent?.Invoke();
        }

        private void Awake()
        {
            if (GameManager.Instance == null)
            {
                GameManager.Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            IngameMenuController.OnIngameMenuScreenShowEvent += OnIngameMenuScreenShowHandler;
            IngameMenuController.OnIngameMenuScreenHideEvent += OnIngameMenuScreenHideHandler;
        }

        private void OnDisable()
        {
            IngameMenuController.OnIngameMenuScreenShowEvent -= OnIngameMenuScreenShowHandler;
            IngameMenuController.OnIngameMenuScreenHideEvent -= OnIngameMenuScreenHideHandler;
        }

        void Start()
        {
            PlayFabManager.Instance.Login();

            StartGame();
        }

        private void OnLevelWasLoaded(int level)
        {
            if (level > 0)
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            isPaused = false;
            LevelPlayTime = 0.0f;

            OnStartNewGameEvent?.Invoke();
            OnResumeGameplayEvent?.Invoke();
        }

        private void Update()
        {
            if (!isPaused)
                LevelPlayTime += Time.deltaTime;
        }

        private void OnIngameMenuScreenShowHandler()
        {
            isPaused = true;
            OnPauseGameplayEvent?.Invoke();
        }

        private void OnIngameMenuScreenHideHandler()
        {
            isPaused = false;
            OnResumeGameplayEvent?.Invoke();
        }
        
    }
}