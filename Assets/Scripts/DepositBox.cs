using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositBox : MonoBehaviour
{
    List<Hamster> containedHamsters = new List<Hamster>();
    public int numberContainedToScore = 10;
    

    // Update is called once per frame
    void Update()
    {
        if(containedHamsters.Count ==numberContainedToScore){
            GameManager.Instance.AddToScore(containedHamsters.Count);
            for (int i = 0; i < containedHamsters.Count; i++)
            {
                Destroy(containedHamsters[i].gameObject);
            }
            containedHamsters.Clear();
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Hamster")){

            containedHamsters.Add(other.gameObject.GetComponent<Hamster>());
            other.tag="ContainedHamster";
        }
    }
}
