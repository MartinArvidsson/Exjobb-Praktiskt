using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerMovement : MonoBehaviour {
    public float rotationDamping = 20f;
    public float speed = 10f;
    public float raylength;
    public int gravity = 0;
    CharacterController controller;
    public GameObject Outerwall;
    public GameObject BuildingWall;
    private List<Transform> savedPositions = new List<Transform>();
    private List<Object> savedBlocks = new List<Object>();
    // Use this for initialization
    void Start () {
        controller = (CharacterController)GetComponent(typeof(CharacterController));
    }
    // Update is called once per frame
    void Update () 
    {
        UpdateMovement();
        BuildWall();
        if(savedPositions.Count != 0)
        {
            FinishWall();
        }
    }

    void BuildWall()
    {
        RaycastHit hit;
        Ray groundray = new Ray(new Vector3(transform.position.x, 2.2f, transform.position.z), Vector3.down);
        Debug.DrawRay(new Vector3(transform.position.x, 2.2f, transform.position.z), Vector3.down * raylength);

        if(Physics.Raycast(groundray,out hit,raylength) && hit.transform.tag == "OuterWall")
        {
            savedPositions.Add(hit.transform);
        }

        if (Physics.Raycast(groundray, out hit, raylength) && hit.transform.tag == "Floor")
        {
            GameObject toInstantiate = BuildingWall;
            var blockclone = Instantiate(toInstantiate, new Vector3(hit.transform.position.x, 1.5f, hit.transform.position.z), Quaternion.identity);
            Destroy(hit.transform.gameObject);
            savedPositions.Add(toInstantiate.transform);
            savedBlocks.Add(blockclone);
        }
    }
    void FinishWall()
    {
        var firstItem = savedPositions.First();
        var lastitem = savedPositions.Last();
        if(firstItem.tag == "OuterWall" && lastitem.tag == "OuterWall")
        {
            if(firstItem.transform.position != lastitem.transform.position)
            {
                foreach(Transform item in savedPositions)
                {
                    if(item != firstItem || item != lastitem)
                    {
                        GameObject toInstantiate = Outerwall;
                        Instantiate(toInstantiate, new Vector3(item.transform.position.x, 1.5f, item.transform.position.z), Quaternion.identity);   
                    } 
                }
                foreach (Object _item in savedBlocks)
                {
                    Destroy(_item);
                }
                savedPositions.Clear();
                savedBlocks.Clear();
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
