using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveForward : MonoBehaviour
{

    float moveSpeed = 1;

    public void speedSliderValue(float newSpeed)
    {
        moveSpeed = newSpeed;
        Debug.Log("Speed = " + moveSpeed);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }
}
