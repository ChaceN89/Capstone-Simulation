/*
PDesignBackend.cs

Script to handle the button inputs and calculations for the p design 
*/
using UnityEngine;

public class PDesignBackend : MonoBehaviour {
    
    private PDesignCamera pDesignCamera; 
    private Animator pDesignAnimator;

    // findthe camera script to change where the camera is focused on 
    private void Start() {
        pDesignCamera = FindObjectOfType<PDesignCamera>();
        pDesignAnimator = GetComponent<Animator>();
    }

    // explode the model 
    public void ExplodeModel(){
        pDesignAnimator.SetTrigger("Explode");
    }

    // rebuild the model 
    public void RebuildModel(){
        pDesignAnimator.SetTrigger("Rebuild");
    }


    // dummy button call for now to snap to lid or the design 
    public void OnSnapClick(){
        Debug.Log("Change hthe camera ");
        if(pDesignCamera.GetObjectTag() =="pDesign"){
            pDesignCamera.SetObjectTag("pLid");
        }else{
            pDesignCamera.SetObjectTag("pDesign");
        }
    }

    public void Reset(){
        Debug.Log("P Design is reset ");
    }


}
