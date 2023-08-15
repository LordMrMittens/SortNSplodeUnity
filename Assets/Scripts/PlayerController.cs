using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask hamsterLayerMask;
    [SerializeField] LayerMask floorLayerMask;
    int ignoreLayer;
    int detectLayer;
    float grabbingVerticalOffset;
    bool hoveringOnFloor;
    public bool ShouldBeFrozen {get;set;} = false;
    Hamster grabbedHamster;

    void Start()
    {
        ignoreLayer = LayerMask.NameToLayer("IgnoreHamsters");
        detectLayer = LayerMask.NameToLayer("Hamsters");
        GameManager.OnBoxIsMoving += SetFrozen;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, hamsterLayerMask))
            {
                if (hit.collider.CompareTag("Hamster"))
                {
                    grabbedHamster = hit.collider.gameObject.GetComponent<Hamster>();
                    grabbedHamster.GetComponent<NavMeshAgent>().enabled = false;
                    grabbedHamster.gameObject.layer = ignoreLayer;
                    grabbingVerticalOffset = hit.collider.bounds.max.y;
                    
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, floorLayerMask) && grabbedHamster != null)
            {
                Vector3 draggingPosition = new Vector3(hit.point.x, grabbingVerticalOffset, hit.point.z);
                grabbedHamster.transform.position = draggingPosition;
                hoveringOnFloor = hit.collider.CompareTag("Floor");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (grabbedHamster != null)
            {
                if (hoveringOnFloor)
                {
                    grabbedHamster.gameObject.layer = detectLayer;
                    grabbedHamster.GetComponent<NavMeshAgent>().enabled = true;
                    grabbedHamster.GetComponent<Hamster>().SetNewDestination();}
                }

                grabbedHamster = null;
            }
        }

            void SetFrozen(bool _isFrozen)
    {

    }
    }
