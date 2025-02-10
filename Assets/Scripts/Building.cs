using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool DocumentBuilding;
    public bool FuelBuilding;
    [Space]
    [SerializeField] GameObject PowerPopUp;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            if (DocumentBuilding)
            {
                DocumentBuilding = false;
                GetComponent<SpriteRenderer>().color = Color.white;
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                GameManager.Instance.CollectDoc();
                EnemySpawner.Instance.EndWave();
                GameManager.Instance.Score += 200;
                AudioPlayer.Instance.PlayPaper();
            }
            else if (FuelBuilding)
            {
                FuelBuilding = false;
                GetComponent<SpriteRenderer>().color = Color.white;
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                Instantiate(PowerPopUp, transform.position, Quaternion.identity).TryGetComponent<PopUp>(out PopUp pop);
                pop.PopUpPower();
                GameManager.Instance.Score += 400;
                AudioPlayer.Instance.PlayPowerUp();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Road" || collision.tag == "Building")
            Destroy(gameObject);
    }
}
