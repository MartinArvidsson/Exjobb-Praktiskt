using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Completed;

public class PlayerLifescript : MonoBehaviour {

    Text playerlifes;


    // Use this for initialization
    void Awake()
    {
        playerlifes = GetComponent<Text>();
        BoardManager.playerlifes = 3;
    }

    // Update is called once per frame
    void Update()
    {
        playerlifes.text = "Remaining lifes : " + BoardManager.playerlifes;
    }
}
