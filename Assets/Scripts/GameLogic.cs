using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Cinemachine;

public class GameLogic : MonoBehaviour
{
    private int playerLevel;
    private int playerExp;
    private int playerMeso;
    private float expRate;
    private float mesoRate;

    public GameObject uiObject;

    public AnimationCurve expCurve;
    public GameObject startPlaceObject;
    public GameObject goalPlaceObject;
    public GameObject meteorFlying;
    public GameObject playerObject;
    public GameObject tree1;

    private GameObject playerInstance;
    private GameObject goal;
    private GameObject treeFolder;
    private GameObject generatedFolder;

    private List<GameObject> hazards = new List<GameObject>();
    private ExpBar expBar;
    private HealthBar healthBar;

    private bool canDie;


    public int maxHealth = 100;
    public int currentHealth;

    private List<GameObject> trees = new List<GameObject>();
    private int levelNumber;
    private bool levelComplete;

    float rngCd = 1.1f;
    float minCd, maxCd;
    float levelUpDash;
    float levelUpDashCd;
    float levelUpSpeed;
    float levelUpRng;
    float cameraFov;

    private Cinemachine.CinemachineVirtualCamera asd;

    private int playerExpNeededToLevel;

    private int valueForReducingLoad = 5;
    private int valueForReducingLoadCounting = 0;

    private Dictionary<VictoriaMobNames, ObjectPool<GameObject>> enemyPools = new();
    private List<VictoriaMobNames> levelEnemies;

    bool initialized;

    // Start is called before the first frame update
    void Start()
    {

        treeFolder = new GameObject("GeneratedTrees");
        generatedFolder = new GameObject("GeneratedFolder");
        expBar = FindAnyObjectByType<ExpBar>();
        healthBar = FindAnyObjectByType<HealthBar>();

        levelNumber = 1;
        newLevel();
        initializeThings(levelNumber);

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        RenderSettings.fogStartDistance = -100f;
        RenderSettings.fogEndDistance = 180f;
        levelUpDash = 1f;
        levelUpDashCd = 1f;
        levelUpSpeed = 1f;
        levelUpRng = 1f;
        cameraFov = 22f;
        minCd = 1f;
        maxCd = 2f;
    }

    public void cantDie()
    {
        canDie = false;
    }

    private void gameOver()
    {
        if (canDie && currentHealth < 0f)
        {
            // tähän game over!
        }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        gameOver();
    }

    public GameObject getPlayer()
    {
        return playerInstance;
    }


    public void initializeThings(int levelNum)
    {
        canDie = true;
        if (playerLevel < 1)
        {
            playerLevel = 1;
        }
        foreach (ObjectPool<GameObject> tempPool in enemyPools.Values)
        {
            tempPool.Clear();
        }
        enemyPools.Clear();

        switch (levelNum)
        {
            case 1:
                levelEnemies = new List<VictoriaMobNames>() { VictoriaMobNames.Blue_Snail, VictoriaMobNames.Slime, VictoriaMobNames.Orange_Mushroom };
                break;
            case 2:
                levelEnemies = new List<VictoriaMobNames>() { VictoriaMobNames.Blue_Snail, VictoriaMobNames.Slime, VictoriaMobNames.Orange_Mushroom };
                break;
            case 3:
                levelEnemies = new List<VictoriaMobNames>() { VictoriaMobNames.Slime, VictoriaMobNames.Orange_Mushroom };
                break;
        }


        levelEnemies.ForEach(x =>
        {
            GameObject tempMob = VictoriaMobs.getEnemy(x);
            ObjectPool<GameObject> tempPool = new(() =>
            {
                return Instantiate(tempMob);
            }, tempMob =>
            {
                tempMob.SetActive(true);
            }, tempMob =>
            {
                tempMob.SetActive(false);
            }, tempMob =>
            {
                Destroy(tempMob);
            }, false, defaultCapacity: 20);
            enemyPools.Add(x, tempPool);
        });

        int expNeeded = Mathf.RoundToInt(expCurve.Evaluate(playerLevel / 10)*25);
        expBar.SetMaxExp(expNeeded);
        initialized = true;
    }

    public void receiveGameObject(GameObject haz)
    {
        hazards.Add(haz);
    }

    public void completeLevel()
    {
        if (!levelComplete)
        {
            uiObject.SetActive(true);
            var playerComp = playerInstance.GetComponent<PlayerMove>();
            cantDie();
            levelComplete = true;
        }
    }

    public void onPlayerDeath()
    {
        // CODE FOR DEATH
        float loseExp = playerExpNeededToLevel * Random.Range(0.03f, 0.1f);
        playerExp -= Mathf.RoundToInt(loseExp);
    }

    private void levelUpPlayer()
    {
        playerLevel++;
        playerExp = 0;
        int expNeeded = Mathf.RoundToInt(expCurve.Evaluate(playerLevel / 10) * 100);
        playerExpNeededToLevel = expNeeded;
        expBar.SetMaxExp(expNeeded);
    }

    public void newLevel()
    {
        initialized = false;
        levelUpPlayer();

        uiObject.SetActive(false);
        levelNumber++;
        Destroy(goal);

        placeTrees();

        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerObject, new Vector3(startPlaceObject.transform.position.x, startPlaceObject.transform.position.y + 0.35f, startPlaceObject.transform.position.z), Quaternion.identity);
        }

        Vector3 newPlace = RandomizeLocation(20, 70, 20, out _);
        Vector3 newGoal = RandomizeLocation(40, 90, 150, out _);

        startPlaceObject.transform.position = new Vector3(newPlace.x, 0.5f, newPlace.z);
        goal = Instantiate(goalPlaceObject, new Vector3(newGoal.x, 0.5f, newGoal.z), Quaternion.identity);
        playerInstance.transform.position = new Vector3(startPlaceObject.transform.position.x, startPlaceObject.transform.position.y + 0.35f, startPlaceObject.transform.position.z);

        playerInstance.GetComponent<PlayerMove>().setStats(levelUpDash, levelUpDashCd, levelUpSpeed);
        levelComplete = false;

        /*
        if (RenderSettings.fogEndDistance < 150f)
        {
            playerInstance.GetComponent<PlayerMove>().lightsOn();
        }
        
        playerInstance.GetComponent<PlayerMove>().camerafov(1f);
        RenderSettings.fogStartDistance -= ((10f+(1.5f*levelNumber)) * levelUpRng);
        if(RenderSettings.fogEndDistance > 133f)
        {
            RenderSettings.fogEndDistance -= (levelNumber/1.5f * levelUpRng);
            if(RenderSettings.fogEndDistance < 130f)
            {
                RenderSettings.fogEndDistance = 132f;
            }
        }
        foreach(var haz in hazards)
        {
            Destroy(haz);
        }
        hazards.Clear();
        cameraFov -= (levelNumber/10);
        */

    }

    public void getEnemyDrops(EnemyObject obj)
    {
        float actualExpRate = expRate > 1f ? expRate : 1f;
        float actualMesoRate = mesoRate > 1f ? mesoRate : 1f;

        playerExp += Mathf.RoundToInt(obj.exp * actualExpRate);
        playerMeso += Mathf.RoundToInt(obj.meso * actualMesoRate);

        if (playerExp > playerExpNeededToLevel)
        {
            levelUpPlayer();
        }
    }

    private void killAction(EnemyStats enemystats, VictoriaMobNames poolKey)
    {
        GameObject aa = (GameObject)enemystats.transform.gameObject;
        enemyPools[poolKey].Release(aa);
    }
    void spawnEnemy(VictoriaMobNames poolKey)
    {

        GameObject mob = enemyPools[poolKey].Get();
        var enemyStatObject = mob.GetComponent<EnemyStats>();
        enemyStatObject.returnToPoolAction(killAction);
        enemyStatObject.setPool(poolKey);
        Vector3 rngLfocation = RandomizeLocation(5, 95, 60, out float randomized);
        mob.transform.SetPositionAndRotation(rngLfocation, Quaternion.identity);

        return;

        /*
        int randomHappening = Random.Range(1, 100);
        int value = 0;
        switch (randomHappening)
        {
            case (< 5): // hint
                value = 99;
                break;
            case (< 25): // tree falls down 28%
                trees[Random.Range(0, trees.Count - 1)].gameObject.transform.GetChild(0).GetComponent<TreeLogic>().makeFall(playerInstance.transform);
                break;
            case (< 33): // tree falls down 28%
                trees[Random.Range(0, trees.Count - 1)].gameObject.transform.GetChild(0).GetComponent<TreeLogic>().makeFall(playerInstance.transform);
                trees[Random.Range(0, trees.Count - 1)].gameObject.transform.GetChild(0).GetComponent<TreeLogic>().makeFall(playerInstance.transform);
                trees[Random.Range(0, trees.Count - 1)].gameObject.transform.GetChild(0).GetComponent<TreeLogic>().makeFall(playerInstance.transform);
                break;
            case (< 55): // deer rampage 22%
                value = 1;
                break;
            case (< 71): // bear attack 16%
                value = 2;
                break;
            case (< 84): // something 13%
                value = 3;
                break;
            case (< 95): // something bad 11%
                value = 4;
                break;
            default: // meteor last 6%
                Vector3 rngLocation = RandomizeLocation(5, 95, 60, out float randomized2);
                var meteorfall = Instantiate(meteorFlying, new Vector3(rngLocation.x + randomized, rngLocation.y + 55f, rngLocation.z - (Mathf.Abs(randomized))), Quaternion.identity);
                meteorfall.transform.parent = generatedFolder.transform;
                meteorfall.GetComponent<MeteorFall>().xzChange = randomized;
                hazards.Add(meteorfall);
                break;
        }
        */
    }

    void Update()
    {
        if (!initialized)
        {
            return;
        }
        levelEnemies.ForEach(x =>
        {
            if (enemyPools[x].CountActive < 4)
                spawnEnemy(x);
        });

        rngCd -= (1f * Time.deltaTime);
        if (rngCd < 1f)
        {
            float randomNumber = Random.Range(0.02f, 2f) * levelUpRng;
            rngCd = (float)(minCd + (maxCd - minCd) * System.Math.Pow(randomNumber, 0.4f));

            //MeteorFall met = gameObject.AddComponent<MeteorFall>() as MeteorFall;
        }

        if (valueForReducingLoadCounting < valueForReducingLoad)
        {
            valueForReducingLoadCounting++;
        }
        else
        {
            valueForReducingLoadCounting = 0;
            expBar.SetCurrentExp(playerExp);
        }
    }

    public Vector3 RandomizeLocation(int min, int max, float distanceMin, out float randomized)
    {
        GameObject safePlace = GameObject.Find("SafeSphere");

        Vector3 startPlace = new Vector3(startPlaceObject.transform.position.x, 0.5f, startPlaceObject.transform.position.z);
        float direction1 = Random.Range(-1f, 1f);
        float direction2 = Random.Range(-1f, 1f);
        Vector3 vector = new Vector3(direction1, 0, direction2);

        float randomNumber = Random.Range(0f, 1f);
        randomized = (float)(min + (max - min) * System.Math.Pow(randomNumber, 0.6));
        RaycastHit hitInfo1;
        Physics.Raycast(safePlace.transform.position, vector, out hitInfo1, 1000, LayerMask.GetMask("Walls"));
        while (hitInfo1.distance < distanceMin)
        {
            direction1 = Random.Range(-1f, 1f);
            direction2 = Random.Range(-1f, 1f);
            vector = new Vector3(direction1, 0, direction2);
            Physics.Raycast(safePlace.transform.position, vector, out hitInfo1, 1000, LayerMask.GetMask("Walls"));
        }

        Vector3 hitLocation = new Vector3(hitInfo1.point.x, hitInfo1.point.y, hitInfo1.point.z);
        Vector3 finalLocation = (hitLocation - startPlace) * (randomized / 100) + startPlace;
        return finalLocation;
    }

    public void placeTrees()
    {
        foreach (var tree in trees)
        {
            Destroy(tree);
        }
        trees.Clear();

        var generatedLocations = PoissonDiscSampling.GeneratePoints(15, new Vector2(280, 280), 160);
        foreach (var point in generatedLocations)
        {
            var oneTree = Instantiate(tree1, new Vector3(point.x - 140, 0, point.y - 140), Quaternion.identity);
            oneTree.transform.parent = treeFolder.transform;
            trees.Add(oneTree);
        }
    }
}



public static class PoissonDiscSampling
{

    public static List<Vector2> GeneratePoints(float radius, Vector2 sampleRegionSize, int numSamplesBeforeRejection = 30)
    {
        float cellSize = radius / Mathf.Sqrt(2);

        int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellSize), Mathf.CeilToInt(sampleRegionSize.y / cellSize)];
        List<Vector2> points = new List<Vector2>();
        List<Vector2> spawnPoints = new List<Vector2>();

        spawnPoints.Add(sampleRegionSize / 2);
        while (spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector2 spawnCentre = spawnPoints[spawnIndex];
            bool candidateAccepted = false;

            for (int i = 0; i < numSamplesBeforeRejection; i++)
            {
                float angle = Random.value * Mathf.PI * 2;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                Vector2 candidate = spawnCentre + dir * Random.Range(radius, 2 * radius);
                if (IsValid(candidate, sampleRegionSize, cellSize, radius, points, grid))
                {
                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = points.Count;
                    candidateAccepted = true;
                    break;
                }
            }
            if (!candidateAccepted)
            {
                spawnPoints.RemoveAt(spawnIndex);
            }

        }

        return points;
    }

    static bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius, List<Vector2> points, int[,] grid)
    {
        if (candidate.x >= 0 && candidate.x < sampleRegionSize.x && candidate.y >= 0 && candidate.y < sampleRegionSize.y)
        {
            int cellX = (int)(candidate.x / cellSize);
            int cellY = (int)(candidate.y / cellSize);
            int searchStartX = Mathf.Max(0, cellX - 2);
            int searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
            int searchStartY = Mathf.Max(0, cellY - 2);
            int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

            for (int x = searchStartX; x <= searchEndX; x++)
            {
                for (int y = searchStartY; y <= searchEndY; y++)
                {
                    int pointIndex = grid[x, y] - 1;
                    if (pointIndex != -1)
                    {
                        float sqrDst = (candidate - points[pointIndex]).sqrMagnitude;
                        if (sqrDst < radius * radius)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }
}
