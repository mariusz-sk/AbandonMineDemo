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

        public event MenuScreenHandler OnShowEvent;
        public event MenuScreenHandler OnHideEvent;
        
            
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowScreen(MenuScreenHandler OnBecameVisibleCallback)
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 0.0f;
            canvasGroup.DOFade(1.0f, 0.3f).OnComplete(
                () =>
                {
                    OnShowEvent?.Invoke();
                    OnBecameVisibleCallback?.Invoke();
                });
        }

        public void HideScreen(MenuScreenHandler OnBecameHiddenCallback)
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.DOFade(0.0f, 0.3f).OnComplete(
                () =>
                {
                    gameObject.SetActive(false);
                    OnHideEvent?.Invoke();
                    OnBecameHiddenCallback?.Invoke();
                });
        }
    }
}
