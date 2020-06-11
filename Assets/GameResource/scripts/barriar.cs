using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barriar : MonoBehaviour {

    public AudioClip hitAudio;


    public void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
    }
}
