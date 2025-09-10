using UnityEngine;

public class LeverRotate : MonoBehaviour
{
    bool inLevelTrigger;
    public GameObject blades;

    void Update()
    {
        if (inLevelTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            transform.Rotate(0, 0, 90);
            blades.GetComponent<Blades>().ChangeSpeed();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inLevelTrigger = true;
            Debug.Log("In Level Trigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inLevelTrigger = false;
            Debug.Log("Out of Level Trigger");
        }
    }
}
