using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AbandonMine.UI
{
    [RequireComponent(typeof(Button))]
    public class SwitchMenuButton : MonoBehaviour
    {
        [SerializeField]
        private MenuScreen targetMenuScreen;

        private MenuScreen currentMenuScreen;

        private Button myButton;

        private void Awake()
        {
            myButton = GetComponent<Button>();
            currentMenuScreen = GetComponentInParent<MenuScreen>();
        }

        private void OnEnable()
        {
            myButton.onClick.AddListener(SwitchToScreen);
        }

        private void OnDisable()
        {
            myButton.onClick.RemoveListener(SwitchToScreen);
        }

        private void SwitchToScreen()
        {
            if (currentMenuScreen != null)
            {
                currentMenuScreen.HideScreen(
                    () =>
                    {
                        if (targetMenuScreen != null)
                            targetMenuScreen.ShowScreen();
                    });
            }
            else if (targetMenuScreen)
            {
                targetMenuScreen.ShowScreen();
            }
        }
    }
}
