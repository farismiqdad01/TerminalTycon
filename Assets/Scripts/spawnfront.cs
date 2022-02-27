using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnfront : MonoBehaviour
{
    public string value;
    public bool useSprite;
    [SerializeField] private Text displayText;
    public Vector2 sizeSpawnPos;
    [SerializeField] Color color1, color2;
    [SerializeField] Sprite smile, angry;
    [SerializeField] AudioClip sound1, sound2;
    // Start is called before the first frame update
    void Start()
    {
        displayText.text = value;
        if(useSprite)
        {
            int i;
            int.TryParse(value, out i);
            if (i < 0)
            {
                displayText.color = color2;
                GetComponent<Image>().sprite = angry;
                GetComponent<AudioSource>().clip = sound2;
                GetComponent<AudioSource>().Play();
            }
            else
            {
                displayText.color = color1;
                GetComponent<Image>().sprite = smile;
                GetComponent<AudioSource>().clip = sound1;
                GetComponent<AudioSource>().Play();
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
