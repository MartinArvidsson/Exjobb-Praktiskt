using UnityEngine;
using System.Collections;
using Board;

namespace GameSetup
{
    using System.Collections.Generic;       //Allows us to use Lists. 

    public class GameManager : MonoBehaviour
    {
        public bool restartedLevel = false;
        public static GameManager GameManagerInstance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
        private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
        public int level;
        //Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (GameManagerInstance == null)

                //if not, set instance to this
                GameManagerInstance = this;

            //If instance already exists and it's not this:
            else if (GameManagerInstance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

            //Check if instance already exists

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

            //Get a component reference to the attached BoardManager script
            boardScript = GetComponent<BoardManager>();

            //Call the InitGame function to initialize the first level 
            InitGame();
        }

        //Initializes the game for each level.
        void InitGame()
        {
            //Call the SetupScene function of the BoardManager script, pass it current level number.
            boardScript.SetupScene(GameManagerInstance.level,GameManagerInstance.restartedLevel);

        }
    }
}
