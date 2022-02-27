using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusShopItem : MonoBehaviour
{
    public int id;
    public double harga;
    public Text level;
    public Text hargatext;
    public GameObject beliButton;
    public GameObject upgradeButton;
    public GameObject maxButton;
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updating()
    {
        if(PlayerPrefs.GetInt("Bus" + id) >= gm.BusPrefabsData[id].GetComponent<Bus>().maxLevel)
        {
            upgradeButton.SetActive(false);
            hargatext.gameObject.SetActive(false);
            beliButton.SetActive(false);
            maxButton.SetActive(true);
            level.text = "Level-" + PlayerPrefs.GetInt("Bus" + id).ToString();

        }
        else
        {
            level.text = "Level-" + PlayerPrefs.GetInt("Bus" + id).ToString();
            if(PlayerPrefs.HasKey("Bus" + id))
            {
                harga = gm.BusPrefabsData[id].GetComponent<Bus>().hargaUpgrade[PlayerPrefs.GetInt("Bus" + id)];
                hargatext.text = harga.ToString();
            }else
            {
                harga = gm.BusPrefabsData[id].GetComponent<Bus>().hargaUpgrade[0];
                hargatext.text = harga.ToString();
            }
            maxButton.SetActive(false);

        }
        UangSystem();
    }

    void UangSystem()
    {
        if (harga >= 10000 && harga < 1000000)
        {
            string u = harga.ToString();
            u = u.Remove(u.Length - 3);
            hargatext.text = u + "k";
        }
        else if (harga >= 1000000 && harga < 1000000000)
        {
            string u = harga.ToString();
            u = u.Remove(u.Length - 6);
            hargatext.text =u + "jt";
        }
        else if (harga >= 1000000000 && harga < 1000000000000)
        {
            string u = harga.ToString();
            u = u.Remove(u.Length - 9);
            hargatext.text =u + "m";
        }
        else if (harga >= 1000000000000 && harga <= 9999999999999)
        {
            string u = harga.ToString();
            u = u.Remove(u.Length - 12);
            hargatext.text = u + "b";
        }
        else if (harga >= 9999999999999)
        {
            hargatext.text = "9999" + "b";
        }
        else
        {
            hargatext.text = harga.ToString();

        }
    }


    public void disbleBeli()
    {
        beliButton.SetActive(false);
        upgradeButton.SetActive(true);
        level.gameObject.SetActive(true);
    }
}
