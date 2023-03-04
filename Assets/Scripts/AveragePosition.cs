using UnityEngine;

public class AveragePosition : MonoBehaviour
{
    [SerializeField] private string targetTag;
 // The tag of the GameObjects we want to calculate the average position for

    private void Start()
    {
        // Print the tag to the console
        Debug.Log("The target tag is " + targetTag);
    }
    private void Update()
    {
        GameObject[] targets = null;
        // Find all GameObjects with the specified tag
        targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets != null && targets.Length > 0)
        {
        // Calculate the average position of the GameObjects
        Vector3 averagePosition = Vector3.zero;
        foreach (GameObject target in targets)
        {
            averagePosition += target.transform.position;
        }
        averagePosition /= targets.Length;


        // Print the average position to the console
        //Debug.Log("The average position of all " + targetTag + " is " + averagePosition);
        transform.position = averagePosition;
        }
    }
}
