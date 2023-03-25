using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine.Gameplay
{
    public class EndLevelTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(EndLevelCo());
            }
        }

        private IEnumerator EndLevelCo()
        {
            yield return new WaitForSeconds(1.0f);
            GameManager.Instance.PlayerFinishedLevel();
        }
    }
}
