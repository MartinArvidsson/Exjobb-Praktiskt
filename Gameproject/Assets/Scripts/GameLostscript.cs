using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameSetup;
using Board;

namespace UIText
{
    public class GameLostscript : MonoBehaviour
    {

        Text gameOverText;
        private bool showGUI = false;
        public GameObject leveltransition;
        public BoardManager boardManager;

        // Use this for initialization
        void Awake()
        {
            gameOverText = GetComponent<Text>();
            boardManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<BoardManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (BoardManager.playerLifes <= 0 || BoardManager.lifeTimer <= 0)
            {
                PlayerLost();
                if (BoardManager.remainingTries <= 1)
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
                gameOverText.enabled = true;

                leveltransition.SetActive(true);
            }
            else
            {
                leveltransition.SetActive(false);
                gameOverText.enabled = false;
            }
        }

        IEnumerator RestartLevel()
        {
            UpdateDisableMovement(true);
            gameOverText.text = "You lost, restarting level: " + GameManager.GameManagerInstance.level;
            showGUI = true;
            yield return new WaitForSeconds(3);
            showGUI = false;

            boardManager.Reset();
            BoardManager.remainingTries -= 1;
            GameManager.GameManagerInstance.restartedLevel = true;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            UpdateDisableMovement(false);

        }

        IEnumerator Gameisover()
        {
            UpdateDisableMovement(true);
            gameOverText.text = "Game over. Exiting to Main Menu..";
            showGUI = true;
            yield return new WaitForSeconds(3);
            showGUI = false;
            boardManager.TotalReset();
            SceneManager.LoadScene(0);
            GameManager.GameManagerInstance.level = 1;
            UpdateDisableMovement(false);

        }

        void UpdateDisableMovement(bool movementStatus)
        {
            BoardManager.disableMovement = movementStatus;
        }
    }
    
}