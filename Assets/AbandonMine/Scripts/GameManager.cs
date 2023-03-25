using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbandonMine.UI;

namespace AbandonMine
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }

        public static event Action OnPlayerHasFinishedLevelEvent;

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

        void Start()
        {
            PlayFabManager.Instance.Login();
        }

        
    }
}