using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Button StartGame;
	// Use this for initialization
	void Start () {
        StartGame = StartGame.GetComponent<Button>();
	}

    public void StartLevel()
    {
        SceneManager.LoadScene("Mainscene");
    }
}
