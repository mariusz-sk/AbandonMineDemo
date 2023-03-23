using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AbandonMine.UI
{
    public class MainMenuController : MenuScreen
    {
        public void OnNewGameButtonClicked()
        {
            HideScreen(
                () =>
                {
                    SceneManager.LoadScene(1);
                });
        }

        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}
