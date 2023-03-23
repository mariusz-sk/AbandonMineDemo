using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace AbandonMine.Inventory
{
    public class InventoryScreenItem : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private TextMeshProUGUI displayName;

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetIcon(Sprite sprite)
        {
            if (icon == null)
                return;

            icon.sprite = sprite;
        }

        public void SetDisplayName(string text)
        {
            if (displayName == null)
                return;

            displayName.text = text;
        }

        public void ShowItem()
        {
            gameObject.SetActive(true);
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0.0f;
                canvasGroup.DOFade(1.0f, 0.8f);
            }

            rectTransform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f);
        }

        public void HideItem()
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0.0f;
                canvasGroup.DOFade(1.0f, 0.5f);
            }

            rectTransform
                .DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f)
                .OnComplete( () => gameObject.SetActive(false) );
        }
    }
}
