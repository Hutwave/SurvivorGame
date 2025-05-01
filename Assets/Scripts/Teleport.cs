using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.HDROutputUtils;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelLogic = FindAnyObjectByType<LevelLogic>();

        //SceneManager.LoadSceneAsync()
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelLogic.triggerTeleport(TeleportTo); 
        }
    }
}
