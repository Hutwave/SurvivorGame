using UnityEngine;
using static UnityEngine.Rendering.HDROutputUtils;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

public class LevelLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Scene currentScene;
    List<Teleport> teleports = new List<Teleport>();
    Dictionary<string, AsyncOperation> teleportOperations = new Dictionary<string, AsyncOperation>();
    Awaitable awaa;

    private void Start()
    {
        SceneManager.sceneLoaded += OnActiveSceneChanged;
        SceneManager.sceneUnloaded += SceneUnloaded;

        NewTeleports();
    }

    void SceneUnloaded(Scene scenee)
    {
        teleportOperations.Remove(scenee.name);
        NewTeleports();
    }
    void NewTeleports()
    {
        teleports = FindObjectsByType<Teleport>(FindObjectsSortMode.None).ToList();
        currentScene = SceneManager.GetActiveScene();
        
        foreach (Teleport teleport in teleports)
        {
            var teleportScene = SceneManager.GetSceneByName(teleport.TeleportTo);
            if (!teleportOperations.ContainsKey(teleport.getNameTo()))
            {
                var operation = SceneManager.LoadSceneAsync(teleport.getNameTo(), LoadSceneMode.Additive);
                operation.allowSceneActivation = false;
                teleportOperations.Add(teleport.getNameTo(), operation);
            }
        }
    }

    public void triggerTeleport(string teleportTo)
    {
        teleportOperations.TryGetValue(teleportTo, out AsyncOperation operation);
        if(!operation.allowSceneActivation)
        operation.allowSceneActivation = true;
    }

    public void OnActiveSceneChanged(Scene teleportScene, LoadSceneMode loadSceneMode)
    {
        var playerObj = FindAnyObjectByType<PlayerMove>().gameObject;
        SceneManager.MoveGameObjectToScene(playerObj, teleportScene);
        SceneManager.MoveGameObjectToScene(FindAnyObjectByType<GameLogic>().gameObject, teleportScene);
        var teles = FindObjectsByType<Teleport>(FindObjectsSortMode.None);
        var location = teles.First(oneTele => oneTele.getName() == teleportScene.name).getPlace();
        location.y += 4; 
        playerObj.transform.position = location;
        currentScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
