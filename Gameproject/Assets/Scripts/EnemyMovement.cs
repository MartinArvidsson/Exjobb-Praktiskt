using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    public float speed;
    public float horizontalmovement;
    public float verticalmovement;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 movement = new Vector3(horizontalmovement,0.0f,verticalmovement);

        rb.AddForce(movement * speed);
	}
}
