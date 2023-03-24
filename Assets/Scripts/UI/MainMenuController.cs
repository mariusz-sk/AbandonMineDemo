using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AbandonMine.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private MenuScreen initialMenuScreen;

        private void Awake()
        {
            // if initial menu screen is not provided in the Inspector
            if (initialMenuScreen == null)
            {
                // then take the first one
                initialMenuScreen = GetComponentInChildren<MenuScreen>();
            }
        }

        private void Start()
        {
            // hide all menu screens
            MenuScreen[] menuScreens = GetComponentsInChildren<MenuScreen>();
            foreach (var menuScreen in menuScreens)
            {
                menuScreen.gameObject.SetActive(false);
            }

            // show initial menu screen (if provided)
            if (initialMenuScreen)
            {
                initialMenuScreen.ShowScreen();
            }
        }
    }
}
