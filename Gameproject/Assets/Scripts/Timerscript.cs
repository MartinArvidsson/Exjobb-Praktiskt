using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Board;


namespace UIText
{
    public class Timerscript : MonoBehaviour
    {
        Text timerText;

        // Use this for initialization
        void Awake()
        {
            timerText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            timerText.text = string.Format("Time remaining: {0:N0}", BoardManager.lifeTimer);
        }
    }
    
}