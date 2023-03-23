using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AbandonMine.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuScreen : MonoBehaviour
    {
        public delegate void MenuScreenHandler();

        public MenuScreenHandler onBecameVisibleEvent;
        public MenuScreenHandler onBecameHiddenEvent;
        
            
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowScreen(MenuScreenHandler onBecameVisibleCallback)
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 0.0f;
            canvasGroup.DOFade(1.0f, 0.3f).OnComplete(
                () =>
                {
                    onBecameVisibleEvent?.Invoke();
                    onBecameVisibleCallback?.Invoke();
                });
        }

        public void HideScreen(MenuScreenHandler onBecameHiddenCallback)
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.DOFade(0.0f, 0.3f).OnComplete(
                () =>
                {
                    gameObject.SetActive(false);
                    onBecameHiddenEvent?.Invoke();
                    onBecameHiddenCallback?.Invoke();
                });
        }
    }
}
