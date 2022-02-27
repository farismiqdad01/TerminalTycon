using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 1)
        {
            GetComponent<AudioSource>().mute = true;

        }
        else
        {
            GetComponent<AudioSource>().mute = false;

        }

    }
}