using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTest : MonoBehaviour
{
    [SerializeField]
    private Transform playerTF;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        playerTF = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(playerTF.position);
    }
}
