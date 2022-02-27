using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateText : MonoBehaviour
{
    public float interval;

    private Text text;

    private void OnEnable()
    {
        text = GetComponent<Text>();
        StartCoroutine(TextAnimator());
    }

    IEnumerator TextAnimator()
    {
        var textTemp = text.text;
        text.text = "";
        
        foreach (char txt in textTemp)
        {
            text.text += txt;
            yield return new WaitForSeconds(interval);
        }
    }
}
