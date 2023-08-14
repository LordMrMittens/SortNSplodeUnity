using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositBox : MonoBehaviour
{
    List<Hamster> containedHamsters = new List<Hamster>();
    public int numberContainedToScore = 10;

    private Vector3 destination;
    private Vector3 startingPoint;
    public float distanceToTravel;
    private float startTime;
    public float movementSpeed = 2.0f;
    public Vector3 direction;

    bool shouldMove = false;
    bool shouldReturn = false;
    
    private void Start() {
        startingPoint = transform.position;
        destination = ((direction - startingPoint).normalized) * distanceToTravel;
    }

    // Update is called once per frame
    void Update()
    {
        if(containedHamsters.Count ==numberContainedToScore){
            StartCoroutine(MoveBoxCoroutine(startingPoint, destination));
            
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Hamster")){

            containedHamsters.Add(other.gameObject.GetComponent<Hamster>());
            other.tag="ContainedHamster";
        }
    }

    IEnumerator MoveBoxCoroutine(Vector3 _startingPoint, Vector3 _destination)
    {

            float journeyLength = Vector3.Distance(_startingPoint, _destination);
            float distanceCovered = (Time.time - startTime) * movementSpeed;

            if (distanceCovered < journeyLength)
            {
                float fractionOfJourney = distanceCovered / journeyLength;
                transform.position = Vector3.Lerp(_startingPoint, _destination, fractionOfJourney);
            }
            else
            {
                transform.position = _destination;
                if (_destination != startingPoint)
                {
                    shouldReturn = true;
                    StartCoroutine(AddScores());
                }
                else
                {
                    shouldReturn = false;
                }
            }
            yield return new WaitUntil(() => shouldReturn == true);
        
    }

    IEnumerator AddScores()
    {

        GameManager.Instance.AddToScore(containedHamsters.Count);
        for (int i = 0; i < containedHamsters.Count; i++)
        {
            Destroy(containedHamsters[i].gameObject);
        }
        containedHamsters.Clear();
        
        yield return new WaitUntil(() => containedHamsters.Count <= 0);
        shouldMove= true;
        StartCoroutine(MoveBoxCoroutine(destination, startingPoint));
    }

}
