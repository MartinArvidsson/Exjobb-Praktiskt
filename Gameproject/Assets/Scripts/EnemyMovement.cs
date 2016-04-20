using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    private float horizontalmovement, verticalmovement;
    public float minspeed,maxspeed,constantspeed;
    private Rigidbody rb;
    public Transform originalObject, reflectedObject;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        horizontalmovement = Random.Range(minspeed, maxspeed);
        verticalmovement = Random.Range(minspeed, maxspeed);
        rb.velocity = new Vector3(horizontalmovement, 0.0f, verticalmovement);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = constantspeed * (rb.velocity.normalized);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Outer Wall")
        {
            //Debug.Log("hit");
            Vector3 myCollisionNormal = collision.contacts[0].normal;
            rb.velocity = Vector3.Reflect(rb.velocity, myCollisionNormal);
        }
    }
}