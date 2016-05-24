using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Completed

{

    public class BoardManager : MonoBehaviour
    {
        public static int blockstoWin = 20;
        public static float lifetimer = 180;                                     //Time before the player dies, this will get divided by the current level
                                                                        //to allow increasing difficulty.

        public int columns = 9;                                         //Number of columns in our game board.
        public int rows = 9;                                            //Number of rows in our game board.

        private int timerreduction;
        public GameObject player;                                       //The player gameobject. in this case the droid.
        public GameObject[] floorTiles;                                 //Array of floor prefabs.
        public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.
        public GameObject[] enemies;                                    //Array of different enemies.
        public GameObject[] inviswall;                                  //Invis wall to prevent player from running to far
        public static int remainingtries = 3;                           //Tries before the game quits.
        public static int playerlifes = 3;                              //Playerlives is always equals to 3 when the game starts. by calling on it in different
                                                                        //scripts we can decrease it's value when different triggers happends.
                                                                        //When the lifetotal is = 0 the game will end.

        public static int totalenemies;                                        //Number of total enemies, will get multiplied by current level to
                                                                        //Allow increasing difficulty.
        
        private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
        private Transform enemiesHolder;                                //A variable to store a reference to the transform of our Enemies object.
        private Transform buildingblocksHolder;                         //A variable to store a reference to the transform of our Buoldingblocks object.
        private List<Vector3> gridPositions = new List<Vector3>();      //A list of possible locations to place tiles.
        private GameObject instance;                                    //The gameobject we use for instantiating new object and placing them as childs to
                                                                        //Their respective holders

        //Clears our list gridPositions and prepares it to generate a new board.
        void InitialiseList()
        {
            //Clear our list gridPositions.
            gridPositions.Clear();

            //Loop through x axis (columns).
            for (int x = 1; x < columns - 1; x++)
            {
                //Within each column, loop through y axis (rows).
                for (int y = 1; y < rows - 1; y++)
                {
                    //At each index add a new Vector3 to our list with the x and y coordinates of that position. Will be used for placing enemies.
                    gridPositions.Add(new Vector3(x, 1f,y));
                }
            }
        }


        //Sets up the outer walls and floor (background) of the game board.
        void BoardSetup()
        {
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject("Board").transform;
            buildingblocksHolder = new GameObject("BuildingBlocks").transform;
            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for (int x = -2; x < columns + 1; x++)
            {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for (int y = -2; y < rows + 1; y++)
                {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                    //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                    if (x == -2 || x == columns || y == -2 || y == rows)
                    {
                        toInstantiate = inviswall[Random.Range(0, inviswall.Length)];
                        instance =
                        Instantiate(toInstantiate, new Vector3(x, 2.5f, y), Quaternion.identity) as GameObject;
                    }
                    else if (x == -1 || x == columns - 1 || y == -1 || y == rows -1)
                    {
                        toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                        instance =
                        Instantiate(toInstantiate, new Vector3(x, 1.5f, y), Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        instance =
                        Instantiate(toInstantiate, new Vector3(x, 0.5f, y), Quaternion.identity) as GameObject;
                    }
                        

                    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                    instance.transform.SetParent(boardHolder);
                }
            }
        }

        Vector3 randomplacement()
        {
            int randomIndex = Random.Range(0, gridPositions.Count);

            //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
            Vector3 randomPosition = gridPositions[randomIndex];
            randomPosition.y = 1.25f;

            //Remove the entry at randomIndex from the list so that it can't be re-used.
            gridPositions.RemoveAt(randomIndex);

            //Return the randomly selected Vector3 position.
            return randomPosition;
        }

        void randomEnemies(GameObject[] enemiesarray, int totalenemies)
        {
            enemiesHolder = new GameObject("Enemies").transform;

            for (int i = 0; i < totalenemies; i++)
            {
                Vector3 randompos = randomplacement();
                GameObject chosenememy = enemiesarray[Random.Range(0, enemiesarray.Length)];

                instance = Instantiate(chosenememy, randompos, Quaternion.identity) as GameObject;

                instance.transform.SetParent(enemiesHolder);
            }

        }

        void spawnplayer()
        {
            Vector3 playerpos = new Vector3(4f,0.4f,-1f);
            Instantiate(player, playerpos, Quaternion.identity);
        }


        //SetupScene initializes our level and calls the previous functions to lay out the game board
        public void SetupScene(int level,bool restartedlevel)
        {
            timerreduction -= level * 2;
            if(restartedlevel == false)
            {
                lifetimer -= timerreduction;

                totalenemies += 2;

                blockstoWin += 3;//Fungerar
            }

            //Creates the outer walls and floor.
            BoardSetup();

            //Reset our list of gridpositions.
            InitialiseList();

            randomEnemies(enemies, totalenemies);

            spawnplayer();
        }
    }
}


