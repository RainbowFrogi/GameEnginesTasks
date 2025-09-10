using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blades : MonoBehaviour
{
    int currentSpeedMultiplier = 0;
    
    public float baseSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0, baseSpeed * currentSpeedMultiplier * Time.deltaTime);
    }

    public void ChangeSpeed()
    {
        if (currentSpeedMultiplier < 3)
        {
            currentSpeedMultiplier++;
        }
        else
        {
            currentSpeedMultiplier = 0;
        }
    }
}

