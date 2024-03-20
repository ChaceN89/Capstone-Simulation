using UnityEngine;

public class CamUnderwaterEffect : MonoBehaviour {
    // Materials for above water and below water 
    public Material aboveWaterSkybox;
    public Material underwaterSkybox;

    // Minimal fog density for underwater
    public float minimalFogDensity = 0.0015f; // Adjust this value to your preference for fog distance

    void Update() {
        float yPosition = transform.position.y;

        if (yPosition >= 0) {
            // Set skybox to above water
            RenderSettings.skybox = aboveWaterSkybox;

            // Turn off fog
            // RenderSettings.fog = false;
        }
        else {
            // Underwater

            // Turn on fog with minimal density
            // RenderSettings.fog = true;
            // RenderSettings.fogDensity = minimalFogDensity; // Use a constant value for simplicity

            // Change the skybox to underwater
            RenderSettings.skybox = underwaterSkybox;
        }
    }
}
