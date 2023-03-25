using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine.Gameplay
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomAudioTrigger : MonoBehaviour
    {
        [SerializeField][Range(0.0f, 120.0f)]
        private float minSecondsDelay = 1.0f;

        [SerializeField]
        [Range(0.0f, 120.0f)]
        private float maxSecondsDelay = 1.0f;

        [SerializeField]
        private AudioClip[] audioClips;

        private AudioSource audioSource;
        private float nextTriggerTime;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioClips.Length < 1)
                enabled = false;
        }

        private void OnValidate()
        {
            if (maxSecondsDelay < minSecondsDelay)
                maxSecondsDelay = minSecondsDelay;
        }

        private void Update()
        {
            if (Time.timeSinceLevelLoad >= nextTriggerTime)
            {
                audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);

                nextTriggerTime = Time.timeSinceLevelLoad + Random.Range(minSecondsDelay, maxSecondsDelay);
            }
        }
    }
}
