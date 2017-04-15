using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimToRagdoll : MonoBehaviour
{
    //This script makes the dinosaur turn into a ragdoll when it dies
    public GameObject ragdollDino;
    public GameObject animDinosaur;
    public Image healthBar;
	void Start ()
    {
        ragdollDino.SetActive(false);
	}
	
	void Update ()
    {
        ragdollDino.transform.position = animDinosaur.transform.position;
        if (healthBar.fillAmount <= 0)
        {
            ragdollDino.SetActive(true);
            animDinosaur.SetActive(false);
        }
    }
}
/* In the editor add the health bar image to the dinosaur.
Add this script to the dinosaur.*/
