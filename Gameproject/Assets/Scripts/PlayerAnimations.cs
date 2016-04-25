using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {
    public Animator animator;
    // Use this for initialization
    void Start () {
	
	}
    void AnimationControl()
    {
        if (Input.GetKey("space"))
        {
            animator.SetBool("Angry", true);
        }
        else
        {
            animator.SetBool("Angry", false);
        }

        if (Input.GetKey("mouse 0"))
        {
            animator.SetBool("Scared", true);
        }
        else
        {
            animator.SetBool("Scared", false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        AnimationControl();
    }
}
