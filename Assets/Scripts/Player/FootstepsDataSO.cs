using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine.Player
{
    [CreateAssetMenu(fileName = "NewFootstepsData", menuName = "Game/New Footsteps Data")]
    public class FootstepsDataSO : ScriptableObject
    {
        [SerializeField]
        private string surfaceTagName;

        [SerializeField]
        AudioClip[] audioClips;

        public string TagName { get => surfaceTagName; }

        public AudioClip[] AudioClips { get => audioClips; }
    }
}
