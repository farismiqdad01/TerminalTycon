using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    play,
    paused,
    Lose,
    Win
}
public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public PlayerState state;
    public GameObject pauseCanvas;



    private void Awake() => Instance = this;

    public void ChangeState(bool paused)
    {
        state = paused ? PlayerState.paused : PlayerState.play ;
        
        switch (state)
        {
            case PlayerState.play:

                pauseCanvas.SetActive(false);

                Time.timeScale = 1;
                break;

            case PlayerState.paused:

                pauseCanvas.SetActive(true);

                Time.timeScale = 0;
                break;
        }

        
    }
}