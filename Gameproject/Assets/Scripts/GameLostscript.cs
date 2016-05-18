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
    }

    // Update is called once per frame
    void Update()
    {
        if(BoardManager.playerlifes <= 0 || BoardManager.lifetimer <= 0)
        {
            PlayerLost();
        }
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
            gameovertext.text = "You lost, restarting level: "+ GameManager.instance.level;
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
        showGUI = true;
        yield return new WaitForSeconds(3);
        showGUI = false;
        Scene scene = SceneManager.GetActiveScene();
        GameManager.restartedLevel = true;
        SceneManager.LoadScene(scene.name);
    }
}
