using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine.Collectables
{
    public abstract class CollectableItem : MonoBehaviour
    {
        public event Action OnCollectedEvent;

        public virtual void Collect(GameObject collector)
        {
            OnCollectedEvent?.Invoke();
            Destroy(gameObject, 0.5f);
        }
    }
}
