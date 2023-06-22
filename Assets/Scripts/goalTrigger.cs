using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalTrigger : MonoBehaviour
{
    private GameLogic rngGen;
    // Start is called before the first frame update

    private void Start()
    {
        rngGen= FindObjectOfType<GameLogic>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player(Clone)")
        {
            rngGen.completeLevel();
        }
    }
}
