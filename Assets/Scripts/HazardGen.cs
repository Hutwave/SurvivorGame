using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardGen : MonoBehaviour
{
    public GameObject startPlaceObject;
    public GameObject goalPlaceObject;
    public GameObject meteorFlying;
    public GameObject playerObject;
    public GameObject tree1;
    float rngCd = 1.1f;
    private float minCd, maxCd;
    private GameObject playerInstance;
    private GameObject goal;
    public GameObject uiObject;
    private List<GameObject> trees = new List<GameObject>();
    private int levelNumber;
    private bool levelComplete;

    private List<GameObject> hazards = new List<GameObject>();

    float levelUpDash;
    float levelUpDashCd;
    float levelUpSpeed;
    float levelUpRng;
    float cameraFov;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fogStartDistance = -100f;
        RenderSettings.fogEndDistance = 180f;
        levelUpDash = 1f;
        levelUpDashCd = 1f;
        levelUpSpeed = 1f;
        levelUpRng = 1f;
        levelNumber = 0;
        cameraFov = 22f;
        minCd = 1f;
        maxCd = 2f;
        newLevel();
    }

    public GameObject getPlayer()
    {
        return playerInstance;
    }

    public void placeTrees()
    {
        var asdd = PoissonDiscSampling.GeneratePoints(15, new Vector2(280, 280), 160);
        foreach (var point in asdd)
        {
            var oneTree = Instantiate(tree1, new Vector3(point.x - 140, 0, point.y - 140), Quaternion.identity);
            trees.Add(oneTree);
        }
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
            playerComp.cantDie();
            levelComplete = true;
        }
    }

    private void levelUpPlayer()
    {
        levelUpDash *= Random.Range(1.00f, 1.02f);
        levelUpDashCd *= Random.Range(0.975f, 1.01f);
        levelUpSpeed *= Random.Range(1.00f, 1.03f);
        levelUpRng *= Random.Range(0.96f, 1.02f);
    }

    public void newLevel()
    {
        levelUpPlayer();
        RenderSettings.fogStartDistance -= ((10f+(1.5f*levelNumber)) * levelUpRng);
        
        if(RenderSettings.fogEndDistance > 133f)
        {
            RenderSettings.fogEndDistance -= (levelNumber/1.5f * levelUpRng);
            if(RenderSettings.fogEndDistance < 130f)
            {
                RenderSettings.fogEndDistance = 132f;
            }
        }
        cameraFov -= (levelNumber/10);
        uiObject.SetActive(false);
        levelNumber++;
        Destroy(playerInstance);
        Destroy(goal);
        foreach(var tree in trees)
        {
            Destroy(tree);
        }
        foreach(var haz in hazards)
        {
            Destroy(haz);
        }
        trees.Clear();
        hazards.Clear();
        placeTrees();
        Vector3 newPlace = RandomizeLocation(20, 70, 20, out _);
        startPlaceObject.transform.position = new Vector3(newPlace.x, 0.5f, newPlace.z);
        Vector3 newGoal = RandomizeLocation(40, 90, 150, out _);
        goal = Instantiate(goalPlaceObject, new Vector3(newGoal.x, 0.5f, newGoal.z), Quaternion.identity);
        
        playerInstance = Instantiate(playerObject, new Vector3(startPlaceObject.transform.position.x, startPlaceObject.transform.position.y + 0.35f, startPlaceObject.transform.position.z), Quaternion.identity);
        playerInstance.GetComponent<PlayerMove>().setStats(levelUpDash, levelUpDashCd, levelUpSpeed);
        //goal.transform.position = playerInstance.transform.position;
        if (RenderSettings.fogEndDistance < 150f)
        {
            playerInstance.GetComponent<PlayerMove>().lightsOn();
        }
        levelComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        rngCd -= (1f * Time.deltaTime);
        if (rngCd < 1f)
        {
            float randomNumber = Random.Range(0.02f, 2f) * levelUpRng;
            rngCd = (float)(minCd + (maxCd - minCd) * System.Math.Pow(randomNumber, 0.4f));
            int randomHappening = Random.Range(1, 100);

            int value = 0;

            //playerInstance.GetComponent<PlayerMove>().camerafov(1f);
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
                    Vector3 rngLocation = RandomizeLocation(5, 95, 60, out float randomized);
                    var meteorfall = Instantiate(meteorFlying, new Vector3(rngLocation.x + randomized, rngLocation.y + 55f, rngLocation.z - (Mathf.Abs(randomized))), Quaternion.identity);
                    meteorfall.GetComponent<MeteorFall>().xzChange = randomized;
                    hazards.Add(meteorfall);
                    break;
            }


            //MeteorFall met = gameObject.AddComponent<MeteorFall>() as MeteorFall;
            
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
