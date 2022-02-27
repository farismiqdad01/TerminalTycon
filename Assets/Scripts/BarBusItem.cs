using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarBusItem : MonoBehaviour
{
    public string nameTitle;
    public int id;
    public bool activate = true;
    [SerializeField] private GameObject activateButton, disactiveButton;
    [SerializeField] private Text Title;
    [SerializeField] private Slider barSlider;

    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        Title.text = nameTitle;

        gm = GameObject.FindObjectOfType<GameManager>();

        if(PlayerPrefs.GetInt("Bus" + id + "Activate", 1) == 1)
        {
            activateButton.SetActive(false);
            disactiveButton.SetActive(true);
        }else if (PlayerPrefs.GetInt("Bus" + id + "Activate", 1) == 0)
        {
            activateButton.SetActive(true);
            disactiveButton.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        barSlider.maxValue = gm.busSpawnTime[id];
        barSlider.value = gm.busSpawnCounter[id];
    }

    public void activating(bool b)
    {
        activate = b;

        if(b)
        {
            PlayerPrefs.SetInt("Bus" + id + "Activate", 1);

        }
        else
        {
            PlayerPrefs.SetInt("Bus" + id + "Activate", 0);

        }
    }
}
