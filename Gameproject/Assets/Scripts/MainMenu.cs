using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Button StartGame;
    public GameObject HowToButton;
    public Button HowTo;
    public Button BacktoMenu;
    public GameObject HowToPlay;
    
	// Use this for initialization
	void Start () {
        StartGame = StartGame.GetComponent<Button>();
        HowTo = HowTo.GetComponent<Button>();
        HowToButton = GameObject.Find("GoBack");
        HowToButton.SetActive(false);
        BacktoMenu = BacktoMenu.GetComponent<Button>();
        HowToPlay = GameObject.Find("Howtoplay");
        HowToPlay.SetActive(false);
	}

    public void StartLevel()
    {
        SceneManager.LoadScene("Mainscene");
    }

    public void ShowHowTo()
    {
        HowToPlay.SetActive(true);
        HowToButton.SetActive(true);
    }

    public void DisableHowTo()
    {
        HowToPlay.SetActive(false);
        HowToButton.SetActive(false);
    }
}
