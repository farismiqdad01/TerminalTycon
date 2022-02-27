using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGMusic : MonoBehaviour
{
    public AudioSource musicBackground;
    public bool muted = false;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {

    }

    private void Update()
    {
        Load();
        musicBackground.volume = PlayerPrefs.GetFloat("BgmVolume");
        musicBackground.mute = muted;
    }
    void Load()
    {
        if(PlayerPrefs.GetInt("Muted", 0) == 1)
        {
            muted = true;
        }
        else
        {
            muted = false;
        }
    }
}
