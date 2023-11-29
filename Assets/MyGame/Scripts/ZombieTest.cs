using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieTest : MonoBehaviour
{
    private enum ZombieState
    {
        Walking,
        Ragdoll
    }

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private PhysicMaterial zombieMaterials;

    private CharacterJoint[] zombieJoints;

    private Rigidbody[] zombieRBs;
    private Collider[] zombieColliders;
    private ZombieState currentState = ZombieState.Walking;
    private Animator zombieAnimator;
    private CharacterController zombieCC;

    // Start is called before the first frame update
    void Awake()
    {
        zombieRBs =GetComponentsInChildren<Rigidbody>();
        zombieJoints = GetComponentsInChildren<CharacterJoint>();
        zombieAnimator = GetComponent<Animator>();
        zombieCC = GetComponent<CharacterController>();
        zombieColliders = GetComponentsInChildren<Collider>();
        DisableRagdoll();
        SetUpCharacterJoints();
        SetUpPhysicsMaterials();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case ZombieState.Walking:
                WalkingBehavior();
                break;
            case ZombieState.Ragdoll:
                RagdollBehavior();
                break;
        }
    }

    private void SetUpCharacterJoints()
    {
        foreach (var joint in zombieJoints)
        {
            joint.enableProjection = true;
        }
    }

    private void SetUpPhysicsMaterials()
    {
        foreach (var collider in zombieColliders)
        {
            collider.material = zombieMaterials;
        }
    }
    private void DisableRagdoll()
    {
        foreach( var rigidbody in zombieRBs )
        {
            rigidbody.isKinematic = true;
        }

        zombieAnimator.enabled = true;
        zombieCC.enabled = true;
    }

    private void EnableRagdoll()
    {
        foreach (var rigidbody in zombieRBs)
        {
            rigidbody.isKinematic = false;
        }

        zombieAnimator.enabled = false;
        zombieCC.enabled = false;
    }

    private void WalkingBehavior()
    {
        Vector3 direction = mainCamera.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 20 * Time.deltaTime);
        
        
    }

    private void RagdollBehavior()
    {
        this.enabled = false;
    }

    public void TriggerRagdoll(Vector3 force, Vector3 hitPoint)
    {
        EnableRagdoll();

        //Rigidbody hitRb = zombieRBs.OrderBy(rigigbody => Vector3.Distance(rigigbody.position, hitPoint)).First();
        Rigidbody hitRb = FindHitRigidbody(hitPoint);
        hitRb.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);
        currentState = ZombieState.Ragdoll;
    }

    private Rigidbody FindHitRigidbody(Vector3 hitPoint)
    {
        Rigidbody closetRigidbody = null;
        float closetDistance = 0;

        foreach (var rb in zombieRBs)
        {
            float distance = Vector3.Distance(rb.position, hitPoint);
            if(closetRigidbody == null || distance < closetDistance)
            {
                closetDistance = distance;
                closetRigidbody = rb;
            }
        }

        return closetRigidbody;
    }
}
