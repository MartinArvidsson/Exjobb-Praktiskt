using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Completed

{

    public class BoardManager : MonoBehaviour
    {
        // Using Serializable allows us to embed a class with sub properties in the inspector.
        [Serializable]
        public class Count
        {
            public int minimum;             //Minimum value for our Count class.
            public int maximum;             //Maximum value for our Count class.


            //Assignment constructor.
            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }


        public int columns = 9;                                         //Number of columns in our game board.
        public int rows = 9;                                            //Number of rows in our game board.
        public GameObject[] floorTiles;                                 //Array of floor prefabs.
        public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.
        public GameObject[] enemies;
        public int totalenemies;
        private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
        private List<Vector3> gridPositions = new List<Vector3>();   //A list of possible locations to place tiles.


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
                    //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                    gridPositions.Add(new Vector3(x, 1f,y));
                }
            }
        }


        //Sets up the outer walls and floor (background) of the game board.
        void BoardSetup()
        {
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject("Board").transform;
            GameObject instance = new GameObject();
            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for (int x = -1; x < columns + 1; x++)
            {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for (int y = -1; y < rows + 1; y++)
                {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                    //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                    if (x == -1 || x == columns || y == -1 || y == rows)
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

        void randomEnemies(GameObject[] enemiesarray, int minimum,int maximum)
        {
            int numbertocreate = Random.Range(minimum, maximum);

            for (int i = 0; i < numbertocreate; i++)
            {
                Vector3 randompos = randomplacement();
                GameObject chosenememy = enemiesarray[Random.Range(0, enemiesarray.Length)];

                Instantiate(chosenememy, randompos, Quaternion.identity);
            }

        }


        //SetupScene initializes our level and calls the previous functions to lay out the game board
        public void SetupScene(int level)
        {
            //Level ska användas för att bestämma antalet bollar.

            //Creates the outer walls and floor.
            BoardSetup();

            //Reset our list of gridpositions.
            InitialiseList();

            randomEnemies(enemies, totalenemies, totalenemies);
        }
    }
}


