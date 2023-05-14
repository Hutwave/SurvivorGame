using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalTrigger : MonoBehaviour
{
    private HazardGen rngGen;
    // Start is called before the first frame update

    private void Start()
    {
        rngGen= FindObjectOfType<HazardGen>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player(Clone)")
        {
            rngGen.completeLevel();
        }
    }
}
