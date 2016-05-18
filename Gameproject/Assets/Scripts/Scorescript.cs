using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Completed;

public class Scorescript : MonoBehaviour {

    public static int score;
    Text blockcounter;
    

	// Use this for initialization
	void Awake () {
        blockcounter = GetComponent<Text>();
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        blockcounter.text = "Currently placed blocks : " + score +" of "+ BoardManager.blockstoWin;
	}
}
