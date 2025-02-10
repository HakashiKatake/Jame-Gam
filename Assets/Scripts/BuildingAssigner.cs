using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAssigner : MonoBehaviour
{
    int docs;
    int fuel;

    private void Start()
    {
        Invoke(nameof(AssignBuilds), 0.3f);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
            Invoke(nameof(AssignBuilds), 0.3f);
    }

    [ContextMenu("Assign")]
    void AssignBuilds()
    {
        docs = GameManager.Instance.DocumentsBuildings;
        fuel = GameManager.Instance.FuelBuildings;

        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        for (int i = 0; i < docs; i++)
        {
            int ran = Random.Range(0, buildings.Length);

            buildings[ran].GetComponent<Building>().DocumentBuilding = true;

            buildings[ran].GetComponent<SpriteRenderer>().color = Color.green;
            buildings[ran].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
        }

        for (int i = 0; i < fuel; i++)
        {
            int ran = Random.Range(0, buildings.Length);

            if (buildings[ran].GetComponent<Building>().DocumentBuilding)
                return;

            buildings[ran].GetComponent<Building>().FuelBuilding = true;

            buildings[ran].GetComponent<SpriteRenderer>().color = Color.yellow;
            buildings[ran].transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
