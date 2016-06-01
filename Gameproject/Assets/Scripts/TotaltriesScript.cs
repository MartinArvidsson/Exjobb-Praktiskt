using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Board;

namespace UIText
{
    public class TotaltriesScript : MonoBehaviour
    {

        Text remainingtries;

        // Use this for initialization
        void Awake()
        {
            remainingtries = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update() //Shows total tries remaining, the player has three lifes, once the player looses all lifes, one life will get removed from total tries
                      //so in total the player has 3x3 attempts before the looses
        {
            remainingtries.text = "Remaining attempts : " + BoardManager.remainingTries;
        }
    }
    
}