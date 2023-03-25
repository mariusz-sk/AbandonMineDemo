using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbandonMine.Inventory;

namespace AbandonMine.Collectables
{
    public class CollectableCurrency : CollectableItem
    {
        [SerializeField]
        private int amount = 10;

        public override void Collect(GameObject collector)
        {
            base.Collect(collector);

            PlayerInventory.Instance.AddCurrencyAmount(amount);
        }
    }
}
