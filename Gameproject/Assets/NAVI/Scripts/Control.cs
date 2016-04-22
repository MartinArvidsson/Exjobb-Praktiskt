using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour 
{
	public Animator animator;

	void Start()
	{
	}
	void AnimationControl ()
	{
		if(Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("w")) 
		{
			animator.SetBool ("Moving" , true);
		}
		else
		{
			animator.SetBool ("Moving" , false);
		}
		
		if(Input.GetKey("space")) 
		{
			animator.SetBool ("Angry" , true);
		}
		else
		{
			animator.SetBool ("Angry" , false);
		}

		if(Input.GetKey("mouse 0")) 
		{
			animator.SetBool ("Scared" , true);
		}
		else
		{
			animator.SetBool ("Scared" , false);
		}
	
	}

	void Update()
	{
        AnimationControl();
    }
}
