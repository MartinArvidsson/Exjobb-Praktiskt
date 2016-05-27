using UnityEngine;
using System.Collections;
using Observer;

namespace Player
{
    public class PlayerAnimations : MonoBehaviour, PlayerObserver
    {
        public Animator animator;
        private bool damaged = false;
        // Use this for initialization
        void Start()
        {
            //TODO Animator
        }
        void AnimationControl(bool damaged)
        {
            if (Input.GetKey("space"))
            {
                animator.SetBool("Angry", true);
            }
            else
            {
                animator.SetBool("Angry", false);
            }

            if (damaged == true)
            {
                animator.SetBool("Scared", true);
            }
            else
            {
                animator.SetBool("Scared", false);
            }
        }

        public void PlayerDamaged() //WIP Getting an UnknownReferenceException. Leaving animation for future works. Leaving the framework for the observer still in the code
        {
            //Debug.Log(animator);
            //animator.SetBool("Idle_Dmg", true);
            //AnimationControl(damaged = true);
            //damaged = false;
        }

        // Update is called once per frame
        void Update()
        {
            AnimationControl(damaged);
        }
    }
    
}