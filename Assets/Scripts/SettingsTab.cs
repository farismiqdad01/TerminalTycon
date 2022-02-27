using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsTab : MonoBehaviour
{
    private Animator animator;
    [Header("Credit")]
    public GameObject[] panelCloseCredit;
    public Text tittle;
    public Text buttonText;

    public bool muted;


    private void Start()
    {
        animator = GetComponent<Animator>();
        //panelCloseCredit[0].GetComponent<Slider>().value = PlayerPrefs.GetFloat("BgmVolume", 0.5f);

        Load();
    }

    private void Update()
    {
        //PlayerPrefs.SetFloat("BgmVolume", panelCloseCredit[0].GetComponent<Slider>().value);
    }
    public void BackToMenu()
    {
        animator.Play("PopOut");
    }
    public void DisableTab()
    {
        gameObject.SetActive(false);
    }
    //public void OpenCredit()
    //{
    //    if (buttonText.text != "Kembali")
    //    {
    //        panelCloseCredit[0].SetActive(false);
    //        panelCloseCredit[1].SetActive(false);
    //        panelCloseCredit[2].SetActive(true);
    //        buttonText.text = "Kembali";
    //        tittle.text = "Credit";
    //    }
    //    else
    //    {
    //        OpenSettings();
    //    }
    //}
    public void OpenSettings()
    {
        panelCloseCredit[1].SetActive(true);
        panelCloseCredit[2].SetActive(false);
        tittle.text = "Pengaturan";
    }

    void Load()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 1)
        {
            muted = false;
            buttonText.text = "Music Off";
        }
        else
        {
            muted = true;
            buttonText.text = "Music On";
        }
    }

    public void toggleMute()
    {
        if (muted == false)
        {
            muted = true;
            PlayerPrefs.SetInt("Muted", 0);
            buttonText.text = "Music On";

        }
        else
        {
            muted = false;
            PlayerPrefs.SetInt("Muted", 1);
            buttonText.text = "Music Off";

        }
    }
}
