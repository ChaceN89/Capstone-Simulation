using UnityEngine;
using TMPro; // Namespace for TextMeshPro

public class DisplayActiveScenario : MonoBehaviour {
    private TextMeshProUGUI textMesh; // Reference to the TextMeshPro component
    private GameManager gameManager; // Reference to the GameManager script
                                     // Reference to the GameManager script

    void Start() {
        // Get the TextMeshPro component attached to the same GameObject
        if (!TryGetComponent<TextMeshProUGUI>(out textMesh)) {
            Debug.LogError("TextMeshPro component not found on the GameObject.");
        }

        // Find the GameManager instance in the scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null) {
            Debug.LogError("GameManager script not found in the scene.");
        }

    }

    void Update() {
        // Set the text of the TextMeshPro component to the active scenario from the GameManager
        if (gameManager != null) {
            textMesh.text = "Active Scenario: " + gameManager.GetActiveScenerio();
        }
    }
}
