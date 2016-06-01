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
        void Start()//Finds the objects in the scene to use for buttons
        {
            startGame = startGame.GetComponent<Button>();
            howTo = howTo.GetComponent<Button>();
            backtoMenu = backtoMenu.GetComponent<Button>();

            howToPlayObject = GameObject.FindGameObjectWithTag("HowToPlayScreen");
            howToPlayObject.SetActive(false);

            //backButtonObject = GameObject.Find("GoBack");
            //backButtonObject.SetActive(false);
        }

        public void StartLevel()//Starts the game
        {
            SceneManager.LoadScene("Mainscene");
        }

        public void ShowHowTo()//Shows howtoplay canvas when pressed
        {
            //backButtonObject.SetActive(true);
            howToPlayObject.SetActive(true);
        }

        public void DisableHowTo()//Disables the howtoplay canvas when pressed
        {
            //backButtonObject.SetActive(false);
            howToPlayObject.SetActive(false);
        }
    }
    
}