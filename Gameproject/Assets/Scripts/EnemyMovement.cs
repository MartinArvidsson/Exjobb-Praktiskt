using UnityEngine;
using System.Collections;
using Completed;
using Observer;
public class EnemyMovement : MonoBehaviour
{
    //private PlayerObserver Iobserver;
    private float horizontalmovement, verticalmovement;
    public float minspeed,maxspeed,constantspeed;
    private Rigidbody rb;
    private bool invunerable;
    public Transform originalObject, reflectedObject;
    
    // Use this for initialization
    void Start () {
        //Iobserver = new PlayerAnimations();
        rb = GetComponent<Rigidbody>();
        horizontalmovement = Random.Range(minspeed, maxspeed);
        verticalmovement = Random.Range(minspeed, maxspeed);
        rb.velocity = new Vector3(horizontalmovement, 0.0f, verticalmovement);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = constantspeed * (rb.velocity.normalized);
    }

    IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Outer Wall")
        {
            //Debug.Log("hit");
            Vector3 myCollisionNormal = collision.contacts[0].normal;
            rb.velocity = Vector3.Reflect(rb.velocity, myCollisionNormal);
        }
        if(collision.collider.tag =="BuildingWall")
        {
            Vector3 myCollisionNormal = collision.contacts[0].normal;
            rb.velocity = Vector3.Reflect(rb.velocity, myCollisionNormal);

            if(!invunerable)
            {
                //Iobserver.PlayerDamaged();
                BoardManager.playerlifes -= 1;
                invunerable = true;
                yield return new WaitForSeconds(2);
                invunerable = false;
            }
        }
    }
}