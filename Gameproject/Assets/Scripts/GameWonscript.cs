using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Completed;
using UnityEngine.SceneManagement;

public class GameWonscript : MonoBehaviour {

    Text gamewontext;
    public GameObject leveltransition;
    bool showGUI = false;
    private int nextlevel;
    // Use this for initialization
    void Awake()
    {
        gamewontext = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Scorescript.score >= BoardManager.blockstoWin)
        {
            PlayerWon();
        }
    }

    void PlayerWon()
    {
        StartCoroutine("NextLevel");
    }

    void OnGUI()
    {
        if (showGUI == true)
        {
            gamewontext.enabled = true;
            gamewontext.text = "Congratulations! You won! Loading level: " + nextlevel;
            leveltransition.SetActive(true);
        }
        else
        {
            leveltransition.SetActive(false);
            gamewontext.enabled = false;
        }
    }

    IEnumerator NextLevel()
    {
        UpdateDisableMovement(true);
        nextlevel = GameManager.instance.level + 1;
        showGUI = true;
        yield return new WaitForSeconds(3);
        showGUI = false;
        GameManager.instance.restartedLevel = false;
        GameManager.instance.level++;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        UpdateDisableMovement(false);
    }

    void UpdateDisableMovement(bool movementStatus)
    {
        BoardManager.disableMovement = movementStatus;
    }
}
