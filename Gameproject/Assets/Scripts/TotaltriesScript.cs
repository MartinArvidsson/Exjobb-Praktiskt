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
        void Update()
        {
            remainingtries.text = "Remaining attempts : " + BoardManager.remainingTries;
        }
    }
    
}