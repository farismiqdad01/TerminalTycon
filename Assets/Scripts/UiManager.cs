using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public float Waktu;
    [Space(10)]
    public List<GameObject> busBarItem = new List<GameObject>();
    public GameObject busBarItemPrefabs;
    public GameObject coinsPrefabs, coinsPrefabsRed;
    public GameObject popularPrefabs;
    public Transform busBarGrid;
    public Transform coinsSpawnerPoint;
    public Transform popularSpawnerPoint;
    public BusShopItem[] tokoBus;

    [Header("Button")]
    public GameObject berangkatButton;
    public GameObject buttonClickObject;
    public Sprite berangkatClickSprite;
    public Sprite berangkatNothingSprite;
    public AudioClip[] buttonClickClip;
    [Space(15)]
    [SerializeField] private Text waktu_Text;
    [SerializeField] private Text popularity_Text;
    [SerializeField] private Text Hari_Text;

    [Header("Lighting System")]
    [SerializeField] private Light lampuJalan;
    [SerializeField] private GameObject[] lampuToko;

    [SerializeField] private Text _fpsText;
    [SerializeField] private float _hudRefreshRate = 1f;

    private float _timer;


    GameManager gm;
    [HideInInspector]public int jam, menit;
    [HideInInspector]public int hari;
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
        updateLevelsDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        waktuSystem();
        popularity_Text.text = gm.Popularity.ToString();
        Hari_Text.text = "Hari ke-" + hari;

        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsText.text = "FPS: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }

    }


    void waktuSystem()
    {
        Waktu += 3 * Time.deltaTime;
        jam = Mathf.FloorToInt(Waktu) / 60;
        menit = Mathf.FloorToInt(Waktu) % 60;

        waktu_Text.text = jam.ToString("00") + ":" + menit.ToString("00");



        if (jam >= 24)
        {
            Waktu = 0;
            hari += 1;
            PlayerPrefs.SetInt("Hari", hari);

        }
    }
    public void updateLevelsDisplay()
    {
        if(gm != null)
        {
            for (int i = 0; i < tokoBus.Length; i++)
            {
                tokoBus[i].id = i;
                tokoBus[i].harga = gm.BusPrefabsData[i].GetComponent<Bus>().hargaUpgrade[0];
                tokoBus[i].updating();
            }
        }
        
    }

    public void updateBusBar()
    {

    }

    public void disbleBeli(int index)
    {
        tokoBus[index].disbleBeli();
    }


}
