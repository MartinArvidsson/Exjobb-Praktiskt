using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Completed;


public class Timerscript : MonoBehaviour {
    Text timerText;

	// Use this for initialization
	void Awake () {
        timerText = GetComponent<Text>();
        BoardManager.lifetimer = 180;
	}
	
	// Update is called once per frame
	void Update () {
        timerText.text = string.Format("Time remaining: {0:N0}", BoardManager.lifetimer);
	}
}
