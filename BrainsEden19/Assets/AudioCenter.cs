using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCenter : MonoBehaviour
{
    [SerializeField]
    private AudioSource thisaudiosource;

    private void Start()
    {
        thisaudiosource = GetComponent<AudioSource>();
    }
    public void PlayLayered(AudioClip clip)
    {
        thisaudiosource.PlayOneShot(clip,0.5f);
    }
}
