/*
GondolaBackend.cs

Script to handle the button inputs and calculations for the gondola
*/
using UnityEngine;

public class GondolaBackend : MonoBehaviour {

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }


    public void Reset() {
        Debug.Log("Gondola is reset ");
    }


    public void PlayGondola() {
        animator.SetTrigger("PlayAllGondolas");
    }
    public void PlayReverseGondola() {
        animator.SetTrigger("PlayReverseGondolas");
    }
}