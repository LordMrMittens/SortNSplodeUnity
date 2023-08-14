using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DepositBox : MonoBehaviour
{
    List<Hamster> containedHamsters = new List<Hamster>();
    public int numberContainedToScore = 10;

    private Vector3 destination;
    private Vector3 startingPoint;
    public float distanceToTravel;
    public Vector3 direction;
    private float startTime;
    public float movementSpeed = 2.0f;

    bool shouldMove = false;
    bool shouldReturn = false;

    private void Start()
    {
        startingPoint = transform.position;
        direction = // Define your direction here.
        destination = startingPoint + direction.normalized * distanceToTravel;
    }

    void Update()
    {
        if (containedHamsters.Count == numberContainedToScore && !shouldMove)
        {
            shouldMove = true;
            startTime = Time.time;
            
            StartCoroutine(MoveBoxCoroutine(startingPoint, destination));
        }
        if(shouldReturn && !shouldMove){
            shouldMove = true;
            StartCoroutine(MoveBoxCoroutine(destination, startingPoint));
            Debug.Log("returning");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hamster"))
        {
            containedHamsters.Add(other.gameObject.GetComponent<Hamster>());
            other.tag = "ContainedHamster";
        }
    }

    IEnumerator MoveBoxCoroutine(Vector3 _startingPoint, Vector3 _Destination)
    {
        while (shouldMove)
        {
            float journeyLength = Vector3.Distance(_startingPoint, _Destination);
            float distanceCovered = (Time.time - startTime) * movementSpeed;

            if (distanceCovered < journeyLength)
            {
                float fractionOfJourney = distanceCovered / journeyLength;
                transform.position = Vector3.Lerp(_startingPoint, _Destination, fractionOfJourney);
            }
            else
            {
                transform.position = _Destination;
                shouldMove = false;
            }
            
            yield return null;
        }
        Debug.Log("logging this");
        if (shouldReturn == false)
        {
            
            StartCoroutine(AddScores());
            Debug.Log("attempting this");

        } else {
            shouldMove=false;
            shouldReturn=false;
        }
        
    }

    IEnumerator AddScores()
    {
        GameManager.Instance.AddToScore(containedHamsters.Count);
        
        foreach (Hamster hamster in containedHamsters)
        {
            Destroy(hamster.gameObject);
        }
        containedHamsters.Clear();
        startTime = Time.time;
        shouldReturn = true;
        Debug.Log("finished Scores");
        yield return null;
    }
}





