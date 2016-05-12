using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Completed;

public class GameLostscript : MonoBehaviour {

    Text gameovertext;


    // Use this for initialization
    void Awake()
    {
        gameovertext = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(BoardManager.playerlifes <= 0 || Timerscript.timer <= 0)
        {
            gameovertext.enabled = true;
            gameovertext.text = "Game over, You lost";
        }
        else
        {
            gameovertext.enabled = false;
        }
    }
}
