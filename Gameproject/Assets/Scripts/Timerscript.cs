using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Timerscript : MonoBehaviour {
    public static float timer;
    Text timerText;

	// Use this for initialization
	void Awake () {
        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        timerText.text = string.Format("Time remaining: {0:N0}", timer);
	}
}
