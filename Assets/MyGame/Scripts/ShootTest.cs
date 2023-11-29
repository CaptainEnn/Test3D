using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTest : MonoBehaviour
{
    [SerializeField]
    private float maximumForce;

    [SerializeField]
    private float maximumForceTime;

    private float timeMouseButtonDown;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timeMouseButtonDown = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                ZombieTest zombie = hitInfo.collider.GetComponentInParent<ZombieTest>();

                if (zombie != null)
                {
                    float mouseButtonDownDuration = Time.time - timeMouseButtonDown;
                    float forcePercentage = mouseButtonDownDuration / maximumForceTime;
                    float forceMagnitude = Mathf.Lerp(1, maximumForce, forcePercentage);

                    Vector3 forceDirection = zombie.transform.position - mainCamera.transform.position;
                    forceDirection.y = 1;
                    forceDirection.Normalize();

                    Vector3 force = forceMagnitude * forceDirection;

                    zombie.TriggerRagdoll(force, hitInfo.point);
                }
            }
        }
    }
}
