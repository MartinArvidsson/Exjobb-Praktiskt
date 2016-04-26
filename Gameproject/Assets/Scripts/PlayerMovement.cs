using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float rotationDamping = 20f;
    public float speed = 10f;
    public float raylength;
    public int gravity = 0;
    CharacterController controller;
    // Use this for initialization
    void Start () {
        controller = (CharacterController)GetComponent(typeof(CharacterController));
    }
    // Update is called once per frame
    void Update () 
    {
        UpdateMovement();

        RaycastHit hit;
        Ray groundray = new Ray(transform.position,Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * raylength);

        if (Physics.Raycast(groundray, out hit, raylength))
        {
            if (hit.collider.tag == "Floor")
            {
                Debug.Log("Träff");
            }
        }
    }

    float UpdateMovement()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 inputVec = new Vector3(x, 0, z);
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
