using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityStandardAssets.CrossPlatformInput;
using Board;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float rotationDamping = 20f;
        public float speed = 10f;
        public float rayLength;
        public int gravity = 0;
        public GameObject outerWall;
        public GameObject buildingWall;

        private GameObject Board;
        private GameObject instance;
        private List<Transform> savedPositions = new List<Transform>();
        private List<GameObject> savedBlocks = new List<GameObject>();
        private BoardManager boardManager;
        private bool isTracing = false;

        CharacterController controller;

        // Use this for initialization
        void Start()
        {
            controller = (CharacterController)GetComponent(typeof(CharacterController));
            boardManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<BoardManager>();
        }
        // Update is called once per frame
        void Update()
        {
            if (BoardManager.disableMovement == false)
            {
                UpdateMovement();
            }
            BuildWall();
        }

        void BuildWall()
        {
            int x = (int)(transform.position.x + Mathf.Sign(transform.position.x) * 0.5f);
            int y = (int)(transform.position.z + Mathf.Sign(transform.position.z) * 0.5f);

            int hit = boardManager.GetCell(x, y);

            if (hit == 2 || hit == -1)
            {
                if (isTracing)
                {
                    isTracing = false;
                    boardManager.StopTrace();
                }
            }
            else if (hit == 0)
            {
                if (!isTracing)
                {
                    isTracing = true;
                    boardManager.StartTrace();
                    boardManager.Trace(x, y);
                }
                else
                {
                    boardManager.Trace(x, y);
                }
            }
        }
        

        float UpdateMovement()
        {
            BoardManager.lifeTimer -= Time.deltaTime;
            Vector3 inputVec = new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0, CrossPlatformInputManager.GetAxisRaw("Vertical"));
            inputVec *= speed;

            controller.Move((inputVec + Vector3.up * -gravity + new Vector3(0, 0, 0)) * Time.deltaTime);

            // Rotation
            if (inputVec != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(inputVec),
                                                      Time.deltaTime * rotationDamping);
            return inputVec.magnitude;
        }
    } 
}
