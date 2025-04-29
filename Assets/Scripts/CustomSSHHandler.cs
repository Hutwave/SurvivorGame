using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public enum WindowType
{
    Inventory,
    Equipment
}
public class CustomSSHHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameLogic gameLogic;
    
    void Start()
    {
        gameLogic = FindAnyObjectByType<GameLogic>();
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            gameLogic.toggleWindow(WindowType.Inventory);
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            gameLogic.toggleWindow(WindowType.Equipment);
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            
        }
    }
    */
}
