using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float rotationDamping = 20f;
    public float speed = 10f;
    public int gravity = 0;
    float verticalVel;  // Used for continuing momentum while in air    
    CharacterController controller;
    // Use this for initialization
    void Start () {
        controller = (CharacterController)GetComponent(typeof(CharacterController));
    }
    float UpdateMovement()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 inputVec = new Vector3(x, 0, z);
        inputVec *= speed;

        controller.Move((inputVec + Vector3.up * -gravity + new Vector3(0, verticalVel, 0)) * Time.deltaTime);

        // Rotation
        if (inputVec != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(inputVec),
                                                  Time.deltaTime * rotationDamping);
        return inputVec.magnitude;
    }

    // Update is called once per frame
    void Update () {
        UpdateMovement();

        //if (controller.isGrounded)
        //    verticalVel = 0f;// Remove any persistent velocity after landing
    }
}
