using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFilter : MonoBehaviour{


    //the final depth for the fading of the underwater depth and fog
    private float FinalDepth = 200f;

    //materials for aboive water and below water 
    public Material aboveWaterSkybox;
    public Material underwaterSkybox;

    //starting and ending color for the sky tint based on finalDepth
    public Color underwaterSkyTintStart = new Color(0.6f, 0.8f, 0.886f, 1.0f);
    public Color underwaterSkyTintEnd = new Color(0.02f, 0.02f, 0.698f, 1.0f);

    //---- Fog ----
    //colour of the fog
    public Color underwaterFogColor = new Color(0.2f, 0.4f, 0.8f, 1f); // Adjust the values to your desired color

    //fog setup for start value end value and 
    public float underwaterFogDensityStart = 0.05f;
    public float underwaterFogDensityEnd = 0.15f;

    //the script from the player/device
    TransformInfo cameraPosInfo;


    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("MainCamera");
        cameraPosInfo = playerObject.GetComponent<TransformInfo>();
    }

    void Update(){

        float yPosition = cameraPosInfo.GetYPosition();


        if (yPosition >= 0){

            // Set skybox to above water
            RenderSettings.skybox = aboveWaterSkybox;

            // Turn off fog
            RenderSettings.fog = false;

        }else{

            float depth = Mathf.Abs(yPosition);
            //underwater
            // Turn on fog
            RenderSettings.fog = true;

            // Adjust fog density and color
            RenderSettings.fogColor = underwaterFogColor;

            // Normalize x to be between 0 and 1
            float depthRatio = Mathf.Clamp01(depth / FinalDepth);

            //the fog density get more dense as the depth goes from 0 to 1000
            RenderSettings.fogDensity = Mathf.Lerp(underwaterFogDensityStart, underwaterFogDensityEnd, depthRatio);

            //the underwater skybox
            RenderSettings.skybox = underwaterSkybox;

            // Linear interpolation between underwaterSkyTintStart and underwaterSkyTintEnd
            RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(underwaterSkyTintStart, underwaterSkyTintEnd, depthRatio));


        }


    }
}
