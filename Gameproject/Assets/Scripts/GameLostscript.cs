using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Completed;
using UnityEngine.SceneManagement;

public class GameLostscript : MonoBehaviour {
    
    Text gameovertext;
    public GameObject leveltransition;
    bool showGUI = false;

    // Use this for initialization
    void Awake()
    {
        gameovertext = GetComponent<Text>();
        //BoardManager.remainingtries = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(BoardManager.playerlifes <= 0 || BoardManager.lifetimer <= 0)
        {
            PlayerLost();
            if (BoardManager.remainingtries <= 1)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        StartCoroutine("Gameisover");
    }

    void PlayerLost()
    {
        StartCoroutine("RestartLevel");
    }

    void OnGUI()
    {
        if (showGUI == true)
        {
            gameovertext.enabled = true;
            
            leveltransition.SetActive(true);
        }
        else
        {
            leveltransition.SetActive(false);
            gameovertext.enabled = false;
        }
    }

    IEnumerator RestartLevel()
    {
        UpdateDisableMovement(true);
        gameovertext.text = "You lost, restarting level: " + GameManager.instance.level;
        showGUI = true;
        yield return new WaitForSeconds(3);
        showGUI = false;
        BoardManager.remainingtries -= 1;
        Scene scene = SceneManager.GetActiveScene();
        GameManager.instance.restartedLevel = true;
        SceneManager.LoadScene(scene.name);
        UpdateDisableMovement(false);

    }

    IEnumerator Gameisover()
    {
        UpdateDisableMovement(true);
        gameovertext.text = "Game over. Exiting to Main Menu..";
        showGUI = true;
        yield return new WaitForSeconds(3);
        showGUI = false;
        SceneManager.LoadScene(0);
        BoardManager.remainingtries = 3;
        UpdateDisableMovement(false);

    }

    void UpdateDisableMovement(bool movementStatus)
    {
        BoardManager.disableMovement = movementStatus;
    }
}
