using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_SoundManager : MonoBehaviour
{

    public void PlaySound(AudioSource _pisteAudio)
    {
        _pisteAudio.Play();
    }

}
