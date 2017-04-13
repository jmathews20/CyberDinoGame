using UnityEngine;
using System.Collections;

public class TempControl : MonoBehaviour {

    private Animator anim;
    private string currentBool = "isIdle";

    private KeyCode currentKey;


    // Use this for initialization
    void Start() {

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentKey = KeyCode.Space;

            anim.SetBool("isJumping", true);
            StartCoroutine(UncheckJump());
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            currentKey = KeyCode.W;
            SwitchBool("isRunning");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentKey = KeyCode.W;
            SwitchBool("isWalking");
        }
        else if (Input.GetKeyUp(currentKey))
        {
            SwitchBool("isIdle");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlayDead();
        }
    }


    /// <summary>
    /// Switches animation states using the bool parameters in the animator.
    /// </summary>
    /// <param name="_whatBool">The animator parameter connected to the animation to be switched to, ie "isWalking".
    /// This could probably be done better with an enumerator.</param>
    void SwitchBool(string _whatBool)
    {
        anim.SetBool(currentBool, false);
        anim.SetBool(_whatBool, true);

        currentBool = _whatBool;
    }

    /// <summary>
    /// This prevents continuous jumping. It should not be done this way - if this script carries forward far enough, then scrap this function and rework the jump handling.
    /// </summary>
    /// <returns></returns>
    IEnumerator UncheckJump()

    {
        yield return new WaitForSeconds(.1f);

        anim.SetBool("isJumping", false);
    }
    /// <summary>
    /// Plays the death animation.
    /// </summary>
    void PlayDead()
    {
        SwitchBool("isDead");
    }
}
