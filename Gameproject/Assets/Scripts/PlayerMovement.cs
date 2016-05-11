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
    private GameObject Board;
    private GameObject BuildingBlocks;
    private GameObject instance;
    private List<Transform> savedPositions = new List<Transform>();
    private List<GameObject> savedBlocks = new List<GameObject>();
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
        BuildingBlocks = GameObject.Find("BuildingBlocks");
        RaycastHit hit;
        Ray groundray = new Ray(new Vector3(transform.position.x, 2.2f, transform.position.z), Vector3.down);
        Debug.DrawRay(new Vector3(transform.position.x, 2.2f, transform.position.z), Vector3.down * raylength);

        if(Physics.Raycast(groundray,out hit,raylength) && hit.transform.tag == "Outer Wall")
        {
            savedPositions.Add(hit.transform);
        }
        else if (Physics.Raycast(groundray, out hit, raylength) && hit.transform.tag == "Floor")
        {
            GameObject toInstantiate = BuildingWall;
            instance = Instantiate(toInstantiate, new Vector3(hit.transform.position.x, 1.5f, hit.transform.position.z), Quaternion.identity) as GameObject;            
            savedPositions.Add(hit.transform);
            instance.transform.SetParent(BuildingBlocks.transform);
            savedBlocks.Add(instance);

        }
    }
    void FinishWall()
    {
        Board = GameObject.Find("Board");
        var firstItem = savedPositions.First();
        var lastitem = savedPositions.Last();
        if(firstItem.tag == "Outer Wall" && lastitem.tag == "Outer Wall")
        {
            if(firstItem.transform.position != lastitem.transform.position)
            {
                foreach(Transform item in savedPositions)
                {
                    if(item.tag != "Outer Wall")
                    {
                        GameObject toInstantiate = Outerwall;
                        instance = Instantiate(toInstantiate, new Vector3(item.transform.position.x, 1.5f, item.transform.position.z), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(Board.transform);
                        Destroy(item.transform.gameObject);
                    } 
                }
                foreach (GameObject _item in savedBlocks)
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

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

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
