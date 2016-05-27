using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameSetup;
using Board;

namespace UIText
{
    public class GameWonscript : MonoBehaviour
    {
        public BoardManager boardManager;
        Text gameWonText;
        public GameObject levelTransition;
        bool showGUI = false;
        private int nextLevel;
        // Use this for initialization
        void Awake()
        {
            gameWonText = GetComponent<Text>();
            boardManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<BoardManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Scorescript.score >= BoardManager.blocksToWin)
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
                gameWonText.enabled = true;
                gameWonText.text = "Congratulations! You won! Loading level: " + nextLevel;
                levelTransition.SetActive(true);
            }
            else
            {
                levelTransition.SetActive(false);
                gameWonText.enabled = false;
            }
        }

        IEnumerator NextLevel()
        {
            UpdateDisableMovement(true);
            nextLevel = GameManager.GameManagerInstance.level + 1;
            showGUI = true;
            yield return new WaitForSeconds(3);
            showGUI = false;
            boardManager.Reset();
            GameManager.GameManagerInstance.restartedLevel = false;
            GameManager.GameManagerInstance.level++;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            UpdateDisableMovement(false);
        }

        void UpdateDisableMovement(bool movementStatus)
        {
            BoardManager.disableMovement = movementStatus;
        }
    }
    
}