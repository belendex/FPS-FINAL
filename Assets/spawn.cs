using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour
{

    string TheCollider;
    Transform SpawnPoint, Enemigo2;

    void Start()
    {
        SpawnPoint = GameObject.Find("SpawnPoint").transform;
        Enemigo2 = GameObject.Find("FirstPersonController").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        TheCollider = other.tag;
        if (TheCollider == "enemy")
        {
           Enemigo2.transform.position = SpawnPoint.transform.position;
        }
    }

}