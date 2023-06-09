using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using AbandonMine.Inventory;

namespace AbandonMine.UI
{
    public class EndLevelScreen : MenuScreen
    {
        [SerializeField]
        private TextMeshProUGUI levelPlayTimeText;

        [SerializeField]
        private TextMeshProUGUI collectedCurrencyText;

        [SerializeField]
        private TextMeshProUGUI totalCurrencyText;

        private void OnEnable()
        {
            OnShowEvent += OnShowEventHandler;
            PlayFabManager.OnCurrencyAddedEvent += OnCurrencyAddedEventHandler;
            PlayFabManager.OnCurrencyRetrievedEvent += OnCurrencyRetrievedEventHandler;
        }


        private void OnDisable()
        {
            OnShowEvent -= OnShowEventHandler;
            PlayFabManager.OnCurrencyAddedEvent -= OnCurrencyAddedEventHandler;
            PlayFabManager.OnCurrencyRetrievedEvent -= OnCurrencyRetrievedEventHandler;
        }

        public void OnBackToMainMenuButtonClicked()
        {
            HideScreen(
                () =>
                {
                    SceneManager.LoadScene(0);
                });
        }

        private void OnShowEventHandler()
        {
            PlayFabManager.Instance.AddCurrency("BG", PlayerInventory.Instance.CollectedCurrency);

            StartCoroutine(AnimateTextValueCo(levelPlayTimeText, 0.0f, GameManager.Instance.LevelPlayTime, 40.0f));
            StartCoroutine(AnimateTextValueCo(collectedCurrencyText, 0, PlayerInventory.Instance.CollectedCurrency, 40.0f));
            totalCurrencyText.text = "-:-";
        }

        private void OnCurrencyRetrievedEventHandler(Dictionary<string, int> currencyInfo)
        {
            int blueGoldAmount = currencyInfo.GetValueOrDefault("BG", 0);
            //totalCurrencyText.text = $"{blueGoldAmount} BG";
            StartCoroutine(AnimateTextValueCo(totalCurrencyText, 0, blueGoldAmount, 40.0f));
            
        }

        private void OnCurrencyAddedEventHandler(bool isSuccess)
        {
            PlayFabManager.Instance.GetCurrency();
        }

        private IEnumerator AnimateTextValueCo(TextMeshProUGUI field, int startValue, int endValue, float amountPerSecond)
        {
            var waitFor = new WaitForSeconds(1.0f / amountPerSecond);

            for (int value = startValue; value <= endValue; value++)
            {
                field.text = $"{value}";
                yield return waitFor;
            }
        }

        private IEnumerator AnimateTextValueCo(TextMeshProUGUI field, float startValue, float endValue, float amountPerSecond)
        {
            var waitFor = new WaitForSeconds(1.0f / amountPerSecond);

            for (float value = startValue; value <= endValue; value+=1.0f)
            {
                field.text = $"{value}";
                yield return waitFor;
            }
        }
    }
}
