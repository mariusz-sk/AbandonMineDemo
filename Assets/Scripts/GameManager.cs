using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }


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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {

            }
        }
    }
}