using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Board;

namespace UIText
{
    public class Scorescript : MonoBehaviour
    {

        public static int score;
        Text blockCounter;
        // Use this for initialization
        void Awake()
        {
            blockCounter = GetComponent<Text>();
            score = 0;
        }

        // Update is called once per frame
        void Update() //Shows number of placed blocks by showing the BoardManager.blocksToWin variable that gets increased as the player places more and more tiles
        {   
            blockCounter.text = "Currently placed blocks : " + score + " of " + BoardManager.blocksToWin;
        }
    }
    
}