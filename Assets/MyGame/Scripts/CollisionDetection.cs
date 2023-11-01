using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public float radius;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter" + other.name);
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay " + other.name);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit " + other.name);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollsionEnter " + collision.gameObject.name);
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollsionStay " + collision.gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollsionExit " + collision.gameObject.name);
    }
}
