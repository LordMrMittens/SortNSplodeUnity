using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public bool ShouldBeFrozen {get;set;} = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnBoxIsMoving += SetFrozen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        void SetFrozen(bool _isFrozen)
    {

    }
}
