using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Board;

namespace UIText
{
    public class PlayerLifescript : MonoBehaviour
    {
        //Simple script to show the current lifes of the player. Presents the Boardmananger playerlifes varible that get's reduced when the player takes a hit from the enemy
        Text playerlifes;


        // Use this for initialization
        void Awake()
        {
            playerlifes = GetComponent<Text>();
            BoardManager.playerLifes = 3;
        }

        // Update is called once per frame
        void Update()
        {
            playerlifes.text = "Remaining lifes : " + BoardManager.playerLifes;
        }
    }
    
}