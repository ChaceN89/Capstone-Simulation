using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro


public class Infomation : MonoBehaviour{
    private GameManager gameManager;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>(); 
    }


    public GameObject CreatePopUp(GameObject popupPrefab, GameObject currentPopup, string objectName ){
        if (popupPrefab != null) {
            currentPopup = Instantiate(popupPrefab);
            Canvas canvas = GameObject.FindObjectOfType<Canvas>(); // Find the Canvas in the scene
            if (canvas != null) {
                // Set the popup as a child of the canvas with correct positioning
                currentPopup.transform.SetParent(canvas.transform, false);

                // Optional: Adjust position based on mouse or object position
                // Here you might need to convert world position to canvas position if needed

                string scenario = gameManager.GetActiveScenerio();

                TMP_Text text1 = GameObject.Find("Text 1").GetComponent<TMP_Text>();
                TMP_Text text2 = GameObject.Find("Text 2").GetComponent<TMP_Text>();
                RectTransform panel1 = GameObject.Find("Panel 1").GetComponent<RectTransform>();
                RectTransform panel2 = GameObject.Find("Panel 2").GetComponent<RectTransform>();



                if (scenario == "gondola"){
                    // deactivate text 1
                    text1.gameObject.SetActive(false);
                    panel1.gameObject.SetActive(false);
                    text2.gameObject.SetActive(true);
                    panel2.gameObject.SetActive(true);
                }else{
                    text1.gameObject.SetActive(true);
                    panel1.gameObject.SetActive(true);
                    text2.gameObject.SetActive(false);
                    panel2.gameObject.SetActive(false);

                    //deactivate text 2
                }
           
                // Set the text of the popup using the GetPopupText method.
                TMP_Text popupText = currentPopup.GetComponentInChildren<TMP_Text>();
                if (popupText != null) {
                    popupText.text = GetPopupText(objectName); // Use the GetPopupText function
                }
            }
        }
        return currentPopup;
    }

   public string GetPopupText(string objectName) {
        switch (objectName) {
            case "Box":
                return "The box is used to contain and protect the various components. The design features rounded edges to reduce drag in the water. It contains 38 screws to hold the components in place, and is filled with mineral oil to help alleviate hydrostatic pressure.";
            case "Lid":
                return "The lid is used to access the components while sealing them off when in use. It features a waterproof gasket and four latches to secure the box and the components within.";
            case "Ardunio":
                return "The Arduino is a board-based microcontroller that serves as the brain for the system. It reads measurement data from the connected instruments, controls the solenoid based on this data, and records the data to the SD card.";
            case "Balloon Top":
            case "Balloon Bottom":
                return "The balloons are used as a compressible canister for the CO<sub>2</sub>. With gravity and the balloon's unique design, compressed liquid CO<sub>2</sub> can flow from the top balloon to the bottom. Each balloon also has a clamp to keep it in place.";
            case "Funnel Top":
            case "Funnel Bottom":
                return "The funnel assists with directing the flow of liquid CO<sub>2</sub> from the top balloon to the bottom. The top of the funnel is used to attach the balloon with the help of a clamp, while the bottom of the funnel attaches to the valve using a short rubber hose.";
            case "Air Valve Top":
            case "Air Valve Bottom":
                return "The air valve is used in conjunction with an air compressor to fill the balloons to the target pressure. They also allow for extraction of the separated gasses once the device has emerged from the water.";
            case "C02 Sensors Top":
            case "CO<sub>2</sub> Sensor Bottom":
                return "The CO<sub>2</sub> Sensors are used to show the difference in parts per million (PPM) of carbon dioxide between the two balloons. They use non-dispersive infrared sensors to detect CO<sub>2</sub> in the range of 0-50000 ppm.";
            case "Battery case":
                return "The battery case stores and protects the battery, allowing the battery to be mounted to the side of the box.";
            case "Battery":
                return "The battery provides up to 12 volts of electrical power to all components in the setup. It is mounted into the battery case, and is connected directly to the Arduino, which routes power to the rest of the components.";
            case "Gasket":
                return "The gasket is used to seal the gap between the opening in the box and the lid in order to prevent leaks. It is stuck directly to the edge of the box with a strong, waterproof adhesive..";
            case "BreadBoards":
                return "The breadboards maintain a strong electrical connection with the wires while using the breadboard's ability to provide various parallel circuits.";
            case "Depth Sensor":
                return "The depth sensor is used to determine the depth that the device is at. It does this by measuring the hydrostatic pressure and converting it to depth, where 1 bar of pressure is equivalent to 10 meters of depth. Features a depth resolution of 3 centimeters.";
            case "Mini Bread Board Back":
                return "The mini breadboards function in the same manner as the larger breadboard, but with a smaller footprint.";
            case "Mini Bread Boards Side":
                return "The mini breadboards function in the same manner as the larger breadboard, but with a smaller footprint.";
            case "MicroSD Card Module ":
                return "The MicroSD Card Module is used to write measurement data to the connected MicroSD card. The MicroSD card can be removed and connected to a computer to read the collected data once the device has emerged from the water.";
            case "Relay":
                return "The relay is an electrically operated switch used to control a circuit by a separate low-power signal. Specifically, it is used to provide enough voltage to open the main valve.";
            case "Valve bracket":
                return "The valve bracket is used to mount and secure the air valve in place. Since this valve is quite heavy, the bracket features thick reinforcement and six screws to mount the bracket to the box.";
            case "Hose Fitting":
            case "Hose Fitting.001":
                return "The hose fitting is used as an adapter between the rubber hose from the funnel to the air valve. It features a ribbed exterior to keep the hose from sliding off.";
            case "Valve":
                return "The valve is used to control the airflow between the balloons. It remains closed until the device has reached the target depth, in which the Arduino tells the valve to open. After a set amount of time, the valve closes again, sealing the liquid CO<sub>2</sub> in the bottom balloon.";


            // for the gondola infomation 
            case "Gondola Connection":
            case "Gondola Connection.001":
            case "Gondola Connection.002":
            case "Gondola Connection.003":
            case "Gondola Connection.004":
            case "Gondola Connection.005":
            case "Gondola Connection.006":
            case "Gondola Connection.007":
            case "Gondola Connection.008":
            case "Gondola Connection.009":
            case "Gondola Connection.010":
            case "Gondola Connection.011":
            case "Gondola Connection.012":
            case "Gondola Connection.013":
                return "The gondola is connected to the cable using a spring-loaded grip, in the same way that cable cars, chairlifts, and other gondolas are fastened. This allows for CO<sub>2</sub> to be extracted from the returning gondola without disturbing the pace of the other gondolas.";

            case "Sherical Enclosure":
            case "Sherical Enclosure.001":
            case "Sherical Enclosure.002":
            case "Sherical Enclosure.003":
            case "Sherical Enclosure.004":
            case "Sherical Enclosure.005":
            case "Sherical Enclosure.006":
            case "Sherical Enclosure.007":
            case "Sherical Enclosure.008":
            case "Sherical Enclosure.009":
            case "Sherical Enclosure.010":
            case "Sherical Enclosure.011":
            case "Sherical Enclosure.012":
            case "Sherical Enclosure.013":
                return "The spherical enclosure helps to protect the internal components from the harsh ocean environment without compromising the need for the balloons to compress. It is made of a more resistant material than the balloons, such as Kevlar.";

            case "Inner Enclosure":
            case "Inner Enclosure.001":
            case "Inner Enclosure.002":
            case "Inner Enclosure.003":
            case "Inner Enclosure.004":
            case "Inner Enclosure.005":
            case "Inner Enclosure.006":
            case "Inner Enclosure.007":
            case "Inner Enclosure.008":
            case "Inner Enclosure.009":
            case "Inner Enclosure.010":
            case "Inner Enclosure.011":
            case "Inner Enclosure.012":
            case "Inner Enclosure.013":
                return "The inner enclosure houses all of the critical components, apart from the balloons and the funnels. This is equivalent to the box seen in the prototype design.";

            case "Inner Mechanisms":
            case "Inner Mechanisms.001":
            case "Inner Mechanisms.002":
            case "Inner Mechanisms.003":
            case "Inner Mechanisms.004":
            case "Inner Mechanisms.005":
            case "Inner Mechanisms.006":
            case "Inner Mechanisms.007":
            case "Inner Mechanisms.008":
            case "Inner Mechanisms.009":
            case "Inner Mechanisms.010":
            case "Inner Mechanisms.011":
            case "Inner Mechanisms.012":
            case "Inner Mechanisms.013":
                return "The inner mechanisms include the critical mechanical and electronic components. This includes the valves, the sensors, the battery, and the microcontroller.";

            case "BalloonTop":
            case "BalloonTop.001":
            case "BalloonTop.002":
            case "BalloonTop.003":
            case "BalloonTop.004":
            case "BalloonTop.005":
            case "BalloonTop.006":
            case "BalloonTop.007":
            case "BalloonTop.008":
            case "BalloonTop.009":
            case "BalloonTop.010":
            case "BalloonTop.011":
            case "BalloonTop.012":
            case "BalloonTop.013":
            case "BalloonBottom":
            case "BalloonBottom.001":
            case "BalloonBottom.002":
            case "BalloonBottom.003":
            case "BalloonBottom.004":
            case "BalloonBottom.005":
            case "BalloonBottom.006":
            case "BalloonBottom.007":
            case "BalloonBottom.008":
            case "BalloonBottom.009":
            case "BalloonBottom.010":
            case "BalloonBottom.011":
            case "BalloonBottom.012":
            case "BalloonBottom.013":
                return "The balloons contain the air that is supplied by the compressor and used in the extraction of the CO<sub>2</sub>. When in the storage rack, the bottom balloon will contain liquid CO<sub>2</sub>.";

            case "Funnels":
            case "Funnels.001":
            case "Funnels.002":
            case "Funnels.003":
            case "Funnels.004":
            case "Funnels.005":
            case "Funnels.006":
            case "Funnels.007":
            case "Funnels.008":
            case "Funnels.009":
            case "Funnels.010":
            case "Funnels.011":
            case "Funnels.012":
            case "Funnels.013":
                return "The funnels are used to connect the compressible balloons to the inner mechanisms. The shape was chosen to maximize the efficiency of fluid flow between the balloons.";

            // non gondola components 
        case "Driving Rack Bottom":
            return "The bottom driving rack is designed to collect and release the gondolas from the ocean floor. It also triggers the inner mechanism in each balloon to open the main valve.";
        // combined non-powered bullwheels as functionality is the same
        case "bullwheel 2":
        case "bullwheel 3": 
        case "bullwheel 2 Bottom":
        case "bullwheel 3 Bottom":        
        case "bullwheel Driver Bottom":
            return "The bullwheel allows for the cable to change direction while adding minimal resistance, like a pulley system.";

        case "C02 Storage":
            return "The CO<sub>2</sub> storage tanks are designed to contain the captured CO<sub>2</sub> when the gondolas return to the surface. Furthermore, they can be exchanged for empty tanks when they are full.";
        
        case "Compressor":
            return "The compressor is used to compress air into each gondola before they are submerged. A Direct Air Capture (DAC) device may be used to capture CO<sub>2</sub> more efficiently.";
        
        case "Motor":
            return "The motor powers the driver bullwheel, rotating it clockwise or counterclockwise depending on where the gondolas are going.";
        
        case "Wiring":
            return "The wiring provides power from the windmill to the motor, compressor, and to the main electrical grid.";
        
        case "Driving Rack":
            return "The driving rack is designed to collect and release the gondolas from the surface platform. It slows down to allow air into the gondolas on the way down, and CO<sub>2</sub> out on the way up.";
        
        case "bullwheel Driver":
            return "The bullwheel driver is connected to the motor and the cable. It spins clockwise or counterclockwise, pulling on the cable to ultimately move the gondolas.";
        
        case "Cable":
            return "The cable holds all of the gondolas while being pulled into or out of the ocean. With woven metal fibers, it functions like a chairlift cable.";
        
        case "Storage Rack":
            return "The storage rack holds the gondolas in place while grid power demand remains stable. If an excess of power is generated, the gondolas are submerged.";
        case "Storage Rack Bottom":
            return "The storage rack holds the gondolas in place while grid power demand remains stable. If more power is needed, the gondolas are released up to the serface.";
        
        case "windTurbine":
            return "The wind turbine is the primary source of energy and is used to drive the gondola down to the bottom platform when energy is high.";



        // for the P use Case infomation 
        case "PUseCase":
            return "The Deep Sea CO<sub>2</sub> Separator, as seen within the 'Prototype Design' tab. Due to the amount of compressed air within, it is naturally buoyant.";

        case "Weight":
            return "The weight is used to counteract the buoyant force produced by the CO<sub>2</sub> Separator.";

        case "Pully":
            return "The pulley system is designed to naturally let the weighted CO<sub>2</sub> Separator sink to a specific depth and retrived from the ocean with an external force.";
        case "winch":
            return "The winch is used to raise and lower the device.";

        default:
            return "";

        }
    }
}
