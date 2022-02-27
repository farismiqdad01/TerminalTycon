using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScene : MonoBehaviour
{
    public void ChangeScene(string sceneName) => SceneChanger.instance.LoadScene(sceneName);
}

