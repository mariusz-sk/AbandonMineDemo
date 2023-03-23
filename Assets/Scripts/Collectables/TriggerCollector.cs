using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine.Collectables
{
    public class TriggerCollector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var collectableItem = other.GetComponentInParent<CollectableItem>();
            if (collectableItem != null)
            {
                collectableItem.Collect(gameObject);
            }
        }
    }
}
