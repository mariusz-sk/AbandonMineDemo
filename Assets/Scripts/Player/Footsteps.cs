using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine.Player
{
    public class Footsteps : MonoBehaviour
    {
        [SerializeField]
        private float footstepDelay = 0.5f;

        [SerializeField]
        private AudioClip defaultAudioClip;

        [SerializeField]
        private FootstepsDataSO[] footstepsData;


        private Transform myTransform;
        private Rigidbody myRigidbody;
        private AudioSource myAudioSource;

        private float footstepTimer = 0.0f;

        private void Awake()
        {
            myTransform = GetComponent<Transform>();
            myRigidbody = GetComponent<Rigidbody>();
            myAudioSource = GetComponent<AudioSource>();

            if (myRigidbody == null || myAudioSource == null)
                enabled = false;
        }

        private void Update()
        {
            if (myRigidbody.velocity.sqrMagnitude < 0.1f)
                return;

            //stepTimer -= myRigidbody.velocity.magnitude * Time.deltaTime;
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0.0f)
            {
                if (Physics.Raycast(myTransform.position, Vector3.down, out RaycastHit hitInfo, 1.5f))
                {
                    AudioClip audioClip = defaultAudioClip;

                    string colliderTag = hitInfo.collider.tag;
                    for (int i=0; i<footstepsData.Length; i++)
                    {
                        if (footstepsData[i] != null && footstepsData[i].TagName.Equals(colliderTag))
                        {
                            AudioClip[] audioClips = footstepsData[i].AudioClips;
                            audioClip = audioClips[Random.Range(0, audioClips.Length)];
                            break;
                        }
                    }

                    if (audioClip != null)
                        myAudioSource.PlayOneShot(audioClip);
                }

                footstepTimer = footstepDelay;
            }
        }
    }
}
