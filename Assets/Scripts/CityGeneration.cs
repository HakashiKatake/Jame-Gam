using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CityGeneration : MonoBehaviour
{
    [SerializeField] GameObject RoadSpawner;
    [SerializeField] int RoadSpawnerCount;
    [Space]
    [SerializeField] int RoadCount;
    [Space]
    [SerializeField] int MinRoadLength;
    [SerializeField] int MaxRoadLength;
    [Space]
    [SerializeField] GameObject Road;
    [Space]
    [SerializeField] int BuildInterval = 3;
    [SerializeField] GameObject[] Buildings;

    private void Start()
    {
        GenerateCity();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
            GenerateCity();
    }

    [ContextMenu("Generate")]
    void GenerateCity()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        //generate
        for (int r = 0; r < RoadSpawnerCount; r++)
        {
            Transform roadSpawner = Instantiate(RoadSpawner, Vector2.zero, Quaternion.identity).transform;
            for (int i = 0; i < RoadCount; i++)
            {
                int rotateDirection = Random.Range(0, 3);
                if (rotateDirection == 0)
                    roadSpawner.Rotate(0, 0, 90);
                else if (rotateDirection == 1)
                    roadSpawner.Rotate(0, 0, -90);

                bool canSpawnBuilding = false;

                int interval = 0;
                int length = Random.Range(MinRoadLength, MaxRoadLength);
                for (int l = 0; l < length; l++)
                {
                    roadSpawner.position += roadSpawner.up * 2;
                    Transform road = Instantiate(Road, roadSpawner.position, roadSpawner.rotation).transform;
                    road.SetParent(transform);

                    interval++;
                    if (canSpawnBuilding && interval == BuildInterval)
                    {
                        int RanPos = Random.Range(0, 2);
                        Vector2 pos = Vector2.zero;

                        if (RanPos == 0)
                            pos = road.position + (road.right * Random.Range(5f, 7f));
                        else if (RanPos == 1)
                            pos = road.position + (road.right * Random.Range(-5f, -7f));

                        Transform building = Instantiate(Buildings[Random.Range(0, Buildings.Length)], pos, road.rotation).transform;
                        building.SetParent(transform);

                        interval = 0;
                    }

                    canSpawnBuilding = true;
                }
            }
            Destroy(roadSpawner.gameObject);
        }
    }
}
