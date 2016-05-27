using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UIText
{
    public class MainMenu : MonoBehaviour
    {

        public Button startGame;
        public Button howTo;
        public Button backtoMenu;

        private GameObject howToPlayObject;
        //private Canvas backButtonObject;
        

        // Use this for initialization
        void Start()
        {
            startGame = startGame.GetComponent<Button>();
            howTo = howTo.GetComponent<Button>();
            backtoMenu = backtoMenu.GetComponent<Button>();

            howToPlayObject = GameObject.FindGameObjectWithTag("HowToPlayScreen");
            howToPlayObject.SetActive(false);

            //backButtonObject = GameObject.Find("GoBack");
            //backButtonObject.SetActive(false);
        }

        public void StartLevel()
        {
            SceneManager.LoadScene("Mainscene");
        }

        public void ShowHowTo()
        {
            //backButtonObject.SetActive(true);
            howToPlayObject.SetActive(true);
        }

        public void DisableHowTo()
        {
            //backButtonObject.SetActive(false);
            howToPlayObject.SetActive(false);
        }
    }
    
}