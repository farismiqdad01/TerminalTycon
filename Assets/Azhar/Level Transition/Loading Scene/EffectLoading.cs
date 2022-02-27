using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectLoading : MonoBehaviour
{
    public RawImage rotateCircle;
    public Text loadingText;
    public Image background;

    public string text;
    public float speedRate;
    // Start is called before the first frame update
    void OnEnable()
    {
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        rotateCircle.transform.rotation = Quaternion.identity;

        if (loadingText == null) return;
        StartCoroutine(Effect());
    }

    // Update is called once per frame
    void Update()
    {
        rotateCircle.transform.Rotate(0, 0, speedRate * Time.deltaTime);
        background.color = new Color(background.color.r, background.color.g, background.color.b, Mathf.MoveTowards(background.color.a, SceneChanger.instance.target + .1f, 3 * Time.deltaTime));
    }

    IEnumerator Effect()
    {
        while (true)
        {
            foreach (var item in text)
            {
                loadingText.text += item;
                yield return new WaitForSeconds(speedRate / 1000);
            }
            loadingText.text = "";
            yield return null;
        }
    }
}
