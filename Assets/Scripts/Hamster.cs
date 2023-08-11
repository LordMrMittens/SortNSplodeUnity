using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hamster : Explosive
{
    NavMeshAgent navMeshAgent;
    Bounds levelBounds;
    Vector3 targetLocation;
    [SerializeField] float DistanceToTargetOffset;
    [SerializeField] float DistanceFromEdgesOffset = 1.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        levelBounds = GameObject.FindGameObjectWithTag("Floor").GetComponent<BoxCollider>().bounds;
        SetNewDestination();
    }


    // Update is called once per frame
    void Update()
    {
        if( Vector3.Distance(transform.position, targetLocation)< DistanceToTargetOffset){
            SetNewDestination();
        }
    }
    public void SetNewDestination()
    {
        targetLocation = VerifyDestination();
        navMeshAgent.SetDestination(targetLocation);
    }
        Vector3 VerifyDestination()
    {
        Vector3 destination;
        float randX = Random.Range(levelBounds.min.x+DistanceFromEdgesOffset, levelBounds.max.x-DistanceFromEdgesOffset);
        float randZ = Random.Range(levelBounds.min.z+DistanceFromEdgesOffset, levelBounds.max.z-DistanceFromEdgesOffset);
        destination = new Vector3(randX, 0, randZ);
        return destination;
    }
}
