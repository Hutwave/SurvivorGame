using UnityEngine;

public class goalTrigger : MonoBehaviour
{
    private GameLogic rngGen;
    // Start is called before the first frame update

    private void Start()
    {
        rngGen = FindAnyObjectByType<GameLogic>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player(Clone)")
        {
            // Old code
        }
    }
}
