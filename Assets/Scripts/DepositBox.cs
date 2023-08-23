using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DepositBox : MonoBehaviour
{
    List<Hamster> containedHamsters = new List<Hamster>();
    public int numberContainedToScore = 10;
    public bool ShouldBeFrozen {get;set;} = false;

    private Vector3 destination;
    private Vector3 startingPoint;
    public float distanceToTravel;
    public Vector3 direction;
    private float startTime;
    public float movementSpeed = 2.0f;
    bool shouldMove = false;
    bool shouldReturn = false;
    bool hasInvoked =false;

    private void Start()
    {
        startingPoint = transform.position;
        startTime =0;
        destination = startingPoint + direction.normalized * distanceToTravel;
        GameManager.OnBoxIsMoving += SetFrozen;
    }

    void Update()
    {
        if (containedHamsters.Count >= numberContainedToScore)
        {
            shouldMove = true;
            if(startTime ==0){
            startTime = Time.time;}
        }
        if (shouldMove)
        {
            if (shouldReturn)
            {
                MoveBox(destination, startingPoint);
            }
            else
            {
                MoveBox(startingPoint, destination);
            }
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

    void MoveBox(Vector3 _startingPoint, Vector3 _Destination)
    {
        
        float journeyLength = Vector3.Distance(_startingPoint, _Destination);
        float distanceCovered = (Time.time - startTime) * movementSpeed;
        if(hasInvoked == false){
            GameManager.Instance.ToggleOnBoxIsMoving(true, this);
            hasInvoked = true;
        }

        if (distanceCovered < journeyLength)
        {
            
            float fractionOfJourney = distanceCovered / journeyLength;
            Debug.Log($"moving from here, {_startingPoint} to here{_Destination} fraction {fractionOfJourney} ");
            transform.position = Vector3.Lerp(_startingPoint, _Destination, fractionOfJourney);
        }
        else
        {
            Debug.Log("elseing here");
            transform.position = _Destination;
            if (shouldReturn == false)
            {
                shouldMove = true;
                shouldReturn = true;
                startTime =0;
                AddScores();
            }
            else
            {
                shouldMove = false;
                shouldReturn = false;
                GameManager.Instance.ToggleOnBoxIsMoving(false, this);
                hasInvoked = false;
                startTime =0;
            }
        }
    }

    void AddScores()
    {
        GameManager.Instance.AddToScore(containedHamsters.Count);

        foreach (Hamster hamster in containedHamsters)
        {
            Destroy(hamster.gameObject);
        }
        containedHamsters.Clear();
        startTime = Time.time;


    }

    void SetFrozen(bool _isFrozen, DepositBox box = null)
    {
        if(box != this){
           // ShouldBeFrozen =_isFrozen;
            Debug.Log($"{gameObject.name} should freeze : {_isFrozen}");
        }
        
        
    }
}







