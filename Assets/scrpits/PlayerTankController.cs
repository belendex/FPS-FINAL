using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankController : MonoBehaviour
{
    public float speed = 4f;
    public float angularSpeed = 30f;

    // Update is called once per frame
    void Update()
    {//rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * (-angularSpeed * Time.deltaTime));


        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up* (Time.deltaTime * angularSpeed));
        }
        //movement 
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * (Time.deltaTime * speed));
        }
    }
}
