using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.
using UIText;
namespace Board

{

    public class BoardManager : MonoBehaviour
    {
        public static bool disableMovement;
        public static int totalEnemies;                                 //Number of total enemies, will get multiplied by current level to
                                                                        //Allow increasing difficulty.
        public static int blocksToWin = 27;                             //Blocks to place before winning the game
        public static float lifeTimer = 180;                            //Time before the player dies, this will get divided by the current level
                                                                        //to allow increasing difficulty.

        public int columns = 9;                                         //Number of columns in our game board.
        public int rows = 9;                                            //Number of rows in our game board.

        public GameObject player;                                       //The player gameobject. in this case the droid.
        public GameObject[] floorTiles;                                 //Array of floor prefabs.
        public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.
        public GameObject[] enemyPrefabs;                                    //Array of different enemies.
        public GameObject[] inviswall;                                  //Invis wall to prevent player from running to far
        public GameObject BuildingWall;
        public static int remainingTries = 3;                           //Tries before the game quits.

       

        public static int playerLifes = 3;                              //Playerlives is always equals to 3 when the game starts. by calling on it in different
                                                                        //scripts we can decrease it's value when different triggers happends.
                                                                        //When the lifetotal is = 0 the game will end.
        
        private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
        private Transform enemiesHolder;                                //A variable to store a reference to the transform of our Enemies object.
        private Transform buildingBlocksHolder;                         //A variable to store a reference to the transform of our Buoldingblocks object.
        private List<Vector3> gridPositions = new List<Vector3>();      //A list of possible locations to place tiles.
                                                                        //Their respective holders

        private int[,] cells;
        private List<GameObject> path;

        //Clears our list gridPositions and prepares it to generate a new board.
        void InitialiseList()
        {
            //Clear our list gridPositions.
            gridPositions.Clear();


            //Loop through x axis (columns).
            for (int x = 1; x < columns; x++)
            {
                //Within each column, loop through y axis (rows).
                for (int y = 1; y < rows; y++)
                {
                    //At each index add a new Vector3 to our list with the x and y coordinates of that position. Will be used for placing enemies.
                    gridPositions.Add(new Vector3(x, 1f, y));
                }
            }
        }

        public void Reset()//Reset the tiles marked as completed on death or win
        {
            cells = new int[columns, rows];
        }

        public void TotalReset()//Reset the game when the player returns to startmenu
        {
            cells = new int[columns, rows];
            remainingTries = 3;
            lifeTimer = 180;
            blocksToWin = 27;
            totalEnemies = 2;
        }


        //Sets up the outer walls and floor (background) of the game board.
        void BoardSetup()
        {
            cells = new int[columns, rows];
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject("Board").transform;
            buildingBlocksHolder = new GameObject("BuildingBlocks").transform;
            GameObject instance;
            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for (int x = -2; x <= columns + 1; x++)
            {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for (int y = -2; y <= rows + 1; y++)
                {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                    //Places inviswall to prevent player from exiting the gamearea
                    if (x == -2 || x == columns + 1 || y == -2 || y == rows + 1)
                    {
                        toInstantiate = inviswall[Random.Range(0, inviswall.Length)];
                        instance = Instantiate(toInstantiate, new Vector3(x, 2.5f, y), Quaternion.identity) as GameObject;
                    }
                    else if (x == -1 || x == columns || y == -1 || y == rows)//Places the walltiles at the outer edge of board
                    {
                        toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                        instance = Instantiate(toInstantiate, new Vector3(x, 0.5f, y), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(boardHolder);
                        instance = Instantiate(toInstantiate, new Vector3(x, 1.5f, y), Quaternion.identity) as GameObject;
                    }
                    else//places floortile
                    {
                        instance = Instantiate(toInstantiate, new Vector3(x, 0.5f, y), Quaternion.identity) as GameObject;
                    }
                    

                    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                    instance.transform.SetParent(boardHolder);
                }
            }
        }

        Vector3 RandomPlacement()
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

        void RandomEnemies(GameObject[] enemiesarray, int totalEnemies)
        {
            enemiesHolder = new GameObject("Enemies").transform;

            for (int i = 0; i < totalEnemies; i++)//Places a random enemy at a random position
            {
                Vector3 randompos = RandomPlacement();
                GameObject chosenememy = enemiesarray[Random.Range(0, enemiesarray.Length)];

                var instance = Instantiate(chosenememy, randompos, Quaternion.identity) as GameObject;

                instance.transform.SetParent(enemiesHolder);
            }

        }

        void SpawnPlayer()//Spawns the player
        {
            Vector3 playerpos = new Vector3(4f,0.4f,-1f);
            Instantiate(player, playerpos, Quaternion.identity);
        }

        //SetupScene initializes our level and calls the previous functions to lay out the game board
        public void SetupScene(int level,bool restartedlevel)
        {
            if(restartedlevel == false)
            {
                
                totalEnemies += 2;

                blocksToWin += 3;//Fungerar
            }

            //Creates the outer walls and floor.
            BoardSetup();

            //Reset our list of gridpositions.
            InitialiseList();

            RandomEnemies(enemyPrefabs, totalEnemies);//Random enemy

            SpawnPlayer();//Spawn player
        }

        public void StartTrace()//Starts tracking playerpositions
        {
            path = new List<GameObject>();
            
        }

        public void Trace(int x, int y)
        {
            if (!path.Find(obj => obj.transform.position.x == x && obj.transform.localPosition.z == y))
            {
                var newPlacement = new Vector3(x,1.5f,y);

                if (x >= 0 && y >= 0 && x < cells.GetLength(0) && y < cells.GetLength(1) && cells[x, y] == 0)
                {
                    var instance = Instantiate(BuildingWall, newPlacement, Quaternion.identity) as GameObject;
                    path.Add(instance);
                    instance.transform.SetParent(buildingBlocksHolder);
                    int cellX = (int)(instance.transform.position.x + 0.5f);
                    int cellY = (int)(instance.transform.position.z + 0.5f);
                    cells[x, y] = 1;
                }
            }
        }

        public void StopTrace()
        {
            var enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
            
            if (path.Count > 0)
            {
                Filler filler = new Filler(cells);
                foreach (GameObject gameObject in path)
                {
                    var instance = Instantiate(outerWallTiles[0], gameObject.transform.position, Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    Destroy(gameObject);
                    int x = (int)(instance.transform.position.x + 0.5f);
                    int y = (int)(instance.transform.position.z + 0.5f);
                    Scorescript.score++;
                    cells[x, y] = 2;

                    filler.Fill(x, y);
                }

                var groups = filler.GetGroups();
                
                foreach (var group in groups)
                {
                    bool fill = true;
                    Transform container = (new GameObject("group")).transform;
                    int[,] tempCells = (int[,])cells.Clone();
                    int score = 0;
                    foreach (var cell in group)
                    {
                        if (enemies.Find( enemy => (int)(enemy.transform.position.x + 0.5f) == cell.x && (int)(enemy.transform.position.z + 0.5f) == cell.y) != null)
                        {
                            fill = false;
                            break;
                        }
                        
                        var instance = Instantiate(outerWallTiles[0], new Vector3(cell.x, 1.5f, cell.y) , Quaternion.identity) as GameObject;
                        instance.transform.SetParent(container);
                        score++;
                        tempCells[cell.x, cell.y] = 2;
                    }

                    if (fill)
                    {
                        container.SetParent(boardHolder);
                        cells = tempCells;
                        Scorescript.score += score;
                    }
                    else
                    {
                        Destroy(container.gameObject);
                    }
                }
            }
        }

        public int GetCell(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < columns && y < rows)
                return cells[x, y];
            return -1;
        }
    }
}