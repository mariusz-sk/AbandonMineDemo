using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AbandonMine.Inventory
{
    public class InventoryScreenItem : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private TextMeshProUGUI displayName;

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
    }
}
