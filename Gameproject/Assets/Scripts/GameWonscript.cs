using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Completed;

public class GameWonscript : MonoBehaviour {

    Text gamewontext;


    // Use this for initialization
    void Awake()
    {
        gamewontext = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Scorescript.score >= BoardManager.blockstoWin)
        {
            gamewontext.enabled = true;
            gamewontext.text = "Congratulations! You won!";
        }
        else
        {
            gamewontext.enabled = false;
        }
    }
}
