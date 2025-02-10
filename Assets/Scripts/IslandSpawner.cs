using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    [SerializeField] GameObject Island;
    [SerializeField] float MinSpawnDistance;
    [SerializeField] float MaxSpawnDistance;
    Vector3 farthestBuilding;

    private void Start()
    {
        SpawnIsland();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
            Invoke(nameof(SpawnIsland), 0.2f);
    }

    [ContextMenu("Spawn Island")]
    void SpawnIsland()
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        float mostDistance = 0;
        for (int i = 0; i < buildings.Length; i++)
        {
            float buildingDistance = Vector2.Distance(transform.position, buildings[i].transform.position);
            if (buildingDistance > mostDistance)
            {
                farthestBuilding = buildings[i].transform.position;
                mostDistance = buildingDistance;
            }
        }
        Vector2 dir = farthestBuilding - transform.position;
        Transform island = Instantiate(Island, dir.normalized * Random.Range(MinSpawnDistance, MaxSpawnDistance), Quaternion.identity).transform;
        island.SetParent(transform);
    }
}
