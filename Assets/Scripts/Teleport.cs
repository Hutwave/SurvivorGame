using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string TeleportFrom;
    public string TeleportTo;
    public SphereCollider SphereCollider;
    public GameObject TeleportPlace;
    private Scene teleportScene;
    private Scene current;
    AsyncOperation operation;
    LevelLogic levelLogic;

    public string getName()
    {
        return TeleportFrom;
    }
    public string getNameTo()
    {
        return TeleportTo;
    }
    public Vector3 getPlace()
    {
        return TeleportPlace.transform.position;
    }

    void Start()
    {
        levelLogic = FindAnyObjectByType<LevelLogic>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelLogic.triggerTeleport(TeleportTo); 
        }
    }
}
