using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLogic : MonoBehaviour
{

    private bool hasFallen;
    // Start is called before the first frame update
    void Start()
    {
        hasFallen = false;
       var gen = FindObjectOfType<GameLogic>();
    }
    public void makeFall(Transform trans)
    {
        if (hasFallen)
        {
            return;
        }
        hasFallen = true;
        gameObject.transform.parent.GetComponent<Animator>().enabled = false;
        Vector3 latva = gameObject.transform.position;
        latva.y = latva.y + 4.25f;
        var towardsPlayer = gameObject.transform.position - trans.position;
        
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(-towardsPlayer.x, -towardsPlayer.y, -towardsPlayer.z).normalized*60, latva, ForceMode.Impulse);
    }
}
