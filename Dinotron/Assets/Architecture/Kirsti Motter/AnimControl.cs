using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimControl : MonoBehaviour {

    public float walkSpeed;

	private Animator anim;
	private string currentBool = "isIdle";

    private KeyCode currentKey;
	private GameCharacter gameCharacter;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		gameCharacter = GetComponent<GameCharacter> ();

        gameCharacter.Death += PlayDead;
	}


	// Update is called once per frame
	void Update () {
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

	void SwitchBool(string _whatBool)
	{
		anim.SetBool (currentBool, false);
		anim.SetBool (_whatBool, true);

		currentBool = _whatBool;
	}

	IEnumerator UncheckJump()
	{
		yield return new WaitForSeconds (.1f);

		anim.SetBool ("isJumping", false);
	}

    void PlayDead()
    {
        SwitchBool("isDead");
    }

}
