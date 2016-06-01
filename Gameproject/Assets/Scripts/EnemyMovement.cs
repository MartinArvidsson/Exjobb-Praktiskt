using UnityEngine;
using System.Collections;
using Board;
using Observer;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        //private PlayerObserver Iobserver;
        public float minSpeed, maxSpeed, constantSpeed;
        private float horizontalMovement, verticalMovement;
        private Rigidbody rb;
        private bool invunerable;

        // Use this for initialization
        void Start()
        {
            //Iobserver = new PlayerAnimations(); Observer NYI, Work in progress
            rb = GetComponent<Rigidbody>();
            horizontalMovement = Random.Range(minSpeed, maxSpeed);
            verticalMovement = Random.Range(minSpeed, maxSpeed);
            rb.velocity = new Vector3(horizontalMovement, 0.0f, verticalMovement);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            rb.velocity = constantSpeed * (rb.velocity.normalized);
        }

        IEnumerator OnCollisionEnter(Collision collision)//Checks what the enemy collides with, and depending on the tag we do the specified thing
        {
            if (collision.collider.tag == "Outer Wall")
            {
                Vector3 myCollisionNormal = collision.contacts[0].normal;
                rb.velocity = Vector3.Reflect(rb.velocity, myCollisionNormal);
            }
            if (collision.collider.tag == "BuildingWall")
            {
                Vector3 myCollisionNormal = collision.contacts[0].normal;
                rb.velocity = Vector3.Reflect(rb.velocity, myCollisionNormal);

                if (!invunerable)
                {
                    //Iobserver.PlayerDamaged();
                    BoardManager.playerLifes -= 1;
                    invunerable = true;
                    yield return new WaitForSeconds(2);
                    invunerable = false;
                }
            }
        }
    } 
}