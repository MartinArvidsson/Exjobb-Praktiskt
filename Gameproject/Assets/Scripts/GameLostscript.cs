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
        public GameObject leveltransition;
        bool showGUI = false;

        // Use this for initialization
        void Awake()
        {
            gameOverText = GetComponent<Text>();
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
            gameOverText.text = "You lost, restarting level: " + GameManager.instance.level;
            showGUI = true;
            yield return new WaitForSeconds(3);
            showGUI = false;
            BoardManager.outerWallPositions.Clear();
            BoardManager.remainingTries -= 1;
            Scene scene = SceneManager.GetActiveScene();
            GameManager.instance.restartedLevel = true;
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
            SceneManager.LoadScene(0);
            BoardManager.outerWallPositions.Clear();
            BoardManager.remainingTries = 3;
            BoardManager.lifeTimer = 180;
            BoardManager.blocksToWin = 20;
            BoardManager.totalEnemies = 2;
            GameManager.instance.level = 1;
            UpdateDisableMovement(false);

        }

        void UpdateDisableMovement(bool movementStatus)
        {
            BoardManager.disableMovement = movementStatus;
        }
    }
    
}