using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    public GameObject[] tutorialPage;
    public Animator anim;
    public bool onSecondPage;

    int page = 0;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        PlayerPrefs.SetInt("tutor", 1);

    }
    public void changePage()
    {
        page++;

        if (page>= tutorialPage.Length)
        {
            anim.SetBool("isDisabled", true);
            PlayerPrefs.SetInt("tutorfinish", 1);
            page = 0;
        }
        else
        {
            for (int i = 0; i < tutorialPage.Length; i++)
            {
                tutorialPage[i].SetActive(false);
            }
            tutorialPage[page].SetActive(true);

        }


    }

    public void disableTab()
    {
        gameObject.SetActive(false);
    }
}
