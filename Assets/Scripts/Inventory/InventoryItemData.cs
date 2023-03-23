using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine.Inventory
{
    [CreateAssetMenu(fileName = "NewInventoryItemData", menuName = "Game/New Inventory Item Data")]
    public class InventoryItemData : ScriptableObject
    {
        [SerializeField]
        private string itemClass;

        [SerializeField]
        private Sprite inventoryIcon;

        public string ItemClass => itemClass;

        public Sprite InventoryIcon => inventoryIcon;
    }
}
