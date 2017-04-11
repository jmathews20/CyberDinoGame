using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimControl : MonoBehaviour {

    public float walkSpeed;

	private Animator anim;
	private string currentBool = "isIdle";
    
	private GameCharacter gameCharacter;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		gameCharacter = GetComponent<GameCharacter> ();

        gameCharacter.Death += PlayDead;
	}


	// Update is called once per frame
	void Update () {
        //A lengthy if statement to handle animation states based on character motion.
        if (gameCharacter.jumpInput)
        {
            anim.SetBool("isJumping", true);
            StartCoroutine(UncheckJump());
        }
        else if (gameCharacter.moveInput.magnitude == 0)
        {
            SwitchBool("isIdle");
        }
        else if (gameCharacter.motor.Velocity.magnitude >= walkSpeed)
        {

            SwitchBool("isRunning");
        }
        else if (gameCharacter.motor.Velocity.magnitude <= walkSpeed)
        {

            SwitchBool("isWalking");
        }
	}

    /// <summary>
    /// Switches animation states using the bool parameters in the animator.
    /// </summary>
    /// <param name="_whatBool">The animator parameter connected to the animation to be switched to, ie "isWalking".
    /// This could probably be done better with an enumerator.</param>
	void SwitchBool(string _whatBool)
	{
		anim.SetBool (currentBool, false);
		anim.SetBool (_whatBool, true);

		currentBool = _whatBool;
	}

    /// <summary>
    /// This prevents continuous jumping. It should not be done this way - if this script carries forward far enough, then scrap this function and rework the jump handling.
    /// </summary>
    /// <returns></returns>
	IEnumerator UncheckJump()
	{
		yield return new WaitForSeconds (.1f);

		anim.SetBool ("isJumping", false);
	}

    /// <summary>
    /// Plays the death animation.
    /// </summary>
    void PlayDead()
    {
        SwitchBool("isDead");
    }

}
