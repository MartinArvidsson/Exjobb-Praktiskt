using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Completed;
using UnityEngine.SceneManagement;

public class GameLostscript : MonoBehaviour {
    bool showGUI = false;
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
            PlayerLost();
        }
        //{
        //    gameovertext.enabled = true;
        //    gameovertext.text = "Game over, You lost";
        //}
        //else
        //{
        //    gameovertext.enabled = false;
        //}
    }

    void PlayerLost()
    {
        StartCoroutine("RestartLevel");
    }
    void OnGUI()
    {
        if(showGUI == true)
        {
            gameovertext.enabled = true;
            gameovertext.text = "Game over, You lost";
        }
        else
        {
            gameovertext.enabled = false;
        }
    }
    IEnumerator RestartLevel()
    {
        showGUI = true;
        yield return new WaitForSeconds(3);
        showGUI = false;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
