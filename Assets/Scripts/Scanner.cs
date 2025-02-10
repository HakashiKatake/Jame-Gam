using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public static Scanner instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject Arrow;
    [SerializeField] float ArrowLifetime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ScanForDocs();
        else if (Input.GetKeyDown(KeyCode.F))
            ScanForFuel();
    }

    void ScanForDocs()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].GetComponent<Building>().DocumentBuilding == true)
            {
                Transform arrow = Instantiate(Arrow, transform.position, Quaternion.identity).transform;
                arrow.GetComponent<Arrow>().Target = buildings[i].transform;
                arrow.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                arrow.SetParent(transform);
                Destroy(arrow.gameObject, ArrowLifetime);
            }
        }
    }

    void ScanForFuel()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].GetComponent<Building>().FuelBuilding == true)
            {
                Transform arrow = Instantiate(Arrow, transform.position, Quaternion.identity).transform;
                arrow.GetComponent<Arrow>().Target = buildings[i].transform;
                arrow.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
                arrow.SetParent(transform);
                Destroy(arrow.gameObject, ArrowLifetime);
            }
        }
    }

    public void SpawnPermArrow(Transform Target)
    {
        Transform arrow = Instantiate(Arrow, transform.position, Quaternion.identity).transform;
        arrow.GetComponent<Arrow>().Target = Target;
        arrow.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        arrow.SetParent(transform);
    }
}
