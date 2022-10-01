using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenCtrl : EvaluateManager
{
    public DoorCtrl door;
    public Transform ovenPos;
    private bool isContainedCup;

    void Start()
    {
        door = transform.Find("Door").GetComponent<DoorCtrl>();
        StartCoroutine(LoadingCor());
        StartCoroutine(PlayTimeCor());
    }

    private void Update()
    {
        if(!door.isOpened && isContainedCup)
        {
            isHolding = true;
        }
        else
        {
            isHolding = false;
        }
    }


    new protected void OnTriggerEnter(Collider other)
    {
        if (door.isOpened && other.gameObject.CompareTag("Cup"))
        {
            if (other.gameObject.GetComponent<Rigidbody>() is Rigidbody)
            {
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.position = ovenPos.position;
                other.transform.rotation = ovenPos.rotation;
                cup = other.gameObject.GetComponent<CupCtrl>();
                isContainedCup = true;
            }
        }
    }

    new protected void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Cup"))
        {
            cup = null;
            isHolding = false;
            isContainedCup = false;
        }
    }
}
