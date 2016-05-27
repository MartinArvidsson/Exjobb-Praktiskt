using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UIText
{
    public class MainMenu : MonoBehaviour
    {

        public Button startGame;
        public GameObject howToButton;
        public Button howTo;
        public Button backtoMenu;
        public GameObject howToPlay;

        // Use this for initialization
        void Start()
        {
            startGame = startGame.GetComponent<Button>();
            howTo = howTo.GetComponent<Button>();
            howToButton = GameObject.Find("GoBack");
            howToButton.SetActive(false);
            backtoMenu = backtoMenu.GetComponent<Button>();
            howToPlay = GameObject.Find("howToplay");
            howToPlay.SetActive(false);
        }

        public void StartLevel()
        {
            SceneManager.LoadScene("Mainscene");
        }

        public void ShowHowTo()
        {
            howToPlay.SetActive(true);
            howToButton.SetActive(true);
        }

        public void DisableHowTo()
        {
            howToPlay.SetActive(false);
            howToButton.SetActive(false);
        }
    }
    
}