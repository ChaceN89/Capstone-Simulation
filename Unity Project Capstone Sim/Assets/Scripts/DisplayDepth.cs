
using UnityEngine;
using TMPro;

public class DisplayDepth : MonoBehaviour {
  
    public TextMeshProUGUI textMeshProObject; // This variable will hold your TextMeshPro object reference

    //the device transform infomaiton script
    TransformInfo transformInfo;

    void Start(){
        GameObject playerObject = GameObject.FindWithTag("Player");
        transformInfo = playerObject.GetComponent<TransformInfo>();

    }

    // Update is called once per frame
    void Update(){
        float depth = transformInfo.GetYPosition();
        textMeshProObject.text = "Depth: " + depth.ToString("0") + " m" + "\nPressure: " + CalculatePressureInAtms(depth) + " Atm";
    }



    public float CalculatePressureInAtms(float depth)
    {

        if (depth >= 0){
            depth = 0;
        } else {
            depth = Mathf.Abs(depth);
        }

        // Constants
        float atmosphericPressure = 1.0f; // Standard atmospheric pressure at sea level in atm
        float densityOfSeawater = 1025.0f; // Typical density of seawater in kg/m^3
        float accelerationDueToGravity = 9.8f; // Acceleration due to gravity in m/s^2

        // Calculate pressure using the hydrostatic pressure formula
        float pressure = atmosphericPressure + (densityOfSeawater * accelerationDueToGravity * depth) / 101325.0f;

        return pressure;
    }
}
