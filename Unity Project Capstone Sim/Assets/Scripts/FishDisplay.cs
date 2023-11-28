using UnityEngine;

public class FishDisplay : MonoBehaviour
{
    public float interval = 12f;
    private float startingDepth = -20f;
    private float deepestDepth = -1000f;

    GameObject fishParent; // Parent GameObject for all the fish objects
    public GameObject SchoolOfFish;
    TransformInfo playerDepthScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerDepthScript = playerObject.GetComponent<TransformInfo>();

        // Create an empty GameObject as the parent for all fish objects
        fishParent = new GameObject("FishParent");

        // Instantiate the SchoolOfFish every interval until -1000
        for (float depth = startingDepth; depth >= deepestDepth; depth -= interval)
        {
            InstantiateSchoolOfFish(depth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Have the depth
        float currentDepth = playerDepthScript.GetYPosition();
        Debug.Log("Current Depth: " + currentDepth);
    }

    void InstantiateSchoolOfFish(float depth)
    {
        Vector3 spawnPosition = new Vector3(0f, depth, 0f);

        // Instantiate the SchoolOfFish prefab or whatever object you want to instantiate
        // The prefab should contain the logic for fish behavior, rotation, etc.
        SchoolOfFish = Instantiate(SchoolOfFish, spawnPosition, Quaternion.identity);

        // Set the parent of the instantiated fish to the FishParent
        SchoolOfFish.transform.parent = fishParent.transform;

        // Create two additional objects in the same Y level, one 1 unit higher, and one 1 unit lower
        Instantiate(SchoolOfFish, new Vector3(0f, depth, 0f), Quaternion.identity).transform.parent = fishParent.transform;
        Instantiate(SchoolOfFish, new Vector3(0f, depth + 1, 0f), Quaternion.identity).transform.parent = fishParent.transform;
        Instantiate(SchoolOfFish, new Vector3(0f, depth - 1, 0f), Quaternion.identity).transform.parent = fishParent.transform;
    }
}
