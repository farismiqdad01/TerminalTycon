using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Range(0, 100)]
    public float volume;

    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = volume;

        slider.onValueChanged.AddListener(valueChanged);
    }

    private void Update()
    {
        slider.value = volume;
    }

    public void valueChanged(float value)
    {
        print(value);
        volume = value;
        AudioListener.volume = value / 100;
    }
}
