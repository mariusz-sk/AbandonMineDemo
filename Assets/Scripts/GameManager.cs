using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine
{
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
            PlayFabManager.Instance.TryLogin();
        }
    }
}