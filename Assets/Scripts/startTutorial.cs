using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class startTutorial : MonoBehaviour
{
    public GameObject tutorialTab;
    public GameObject settingsTab;
    public Transition transition;
    public string startScene;


    private void Start()
    {
          
    }


    public void enableTutorial()
    {
        tutorialTab.GetComponent<Animator>().SetBool("isDisabled", false);
        tutorialTab.SetActive(true);
        tutorialTab.GetComponent<TutorialButton>().tutorialPage[1].SetActive(false);
        tutorialTab.GetComponent<TutorialButton>().onSecondPage = false;
    }

    public void EnableSettings()
    {
        settingsTab.SetActive(true);
        settingsTab.GetComponent<SettingsTab>().OpenSettings();
    }

    public void startGame()
    {

        StartCoroutine(start());

    }

    IEnumerator start()
    {
        transition.start();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(startScene);

    }
}
