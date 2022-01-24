using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    private GameObject[] otherMusic;

    private void Update()
    {
        DontDestroyOnLoad(transform.gameObject);
        otherMusic = GameObject.FindGameObjectsWithTag("Music");

        foreach(GameObject music in otherMusic)
        {
            if(music.GetComponent<AudioSource>().clip != GetComponent<AudioSource>().clip)
            {
                GetComponent<AudioSource>().clip = music.GetComponent<AudioSource>().clip;
                GetComponent<AudioSource>().Play();
            }
        }
    }

}
