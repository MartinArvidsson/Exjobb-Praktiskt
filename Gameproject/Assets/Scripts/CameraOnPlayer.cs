﻿using UnityEngine;
using System.Collections;

namespace Player
{
    public class CameraOnPlayer : MonoBehaviour
    {
        public float cameraDistOffset = 3.5f;
        private Camera mainCamera;
        private GameObject player;
        void Start()
        {
            mainCamera = GetComponent<Camera>();
            player = GameObject.Find("Player(Clone)");
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 playerInfo = player.transform.transform.position; //Players position

            mainCamera.transform.position = new Vector3(playerInfo.x, 12, playerInfo.z - cameraDistOffset);
            //The camera follows the player based on the players position
        }
    }
    
}