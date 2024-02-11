using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    string activeSection = "gondolaSim";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setPrototypeDesign(){
        activeSection = "prototypeDesign";
       Debug.Log("Setting to prototype Design");
    }
    public void setPrototypeUseCase(){
        activeSection = "prototypeUseCase";
       Debug.Log("Setting to prototype Design");
    }
    public void setGondolaSim(){
        activeSection = "gondolaSim";
        
    }
}
