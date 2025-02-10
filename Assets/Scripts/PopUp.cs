using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public enum PowerSelection
    {
        Speed,
        Shield,
        Health
    }

    [Serializable]
    public class PowerType
    {
        public Sprite PowerSprite;
        public PowerSelection Power;
        public float IncreaseValue = 1f;
    }

    [SerializeField] PowerType[] Powers;

    float moveSpeed;
    bool shrink;

    private void Start()
    {
        transform.localScale = Vector3.zero;

        int randomPower = UnityEngine.Random.Range(0, Powers.Length);

        GetComponent<SpriteRenderer>().sprite = Powers[randomPower].PowerSprite;

        if (Powers[randomPower].Power == PowerSelection.Speed)
        {
            Movement.Instance.Speed += Powers[randomPower].IncreaseValue;
        }
        else if (Powers[randomPower].Power == PowerSelection.Shield)
        {
            Movement.Instance.GetComponent<Health>().Shield();
        }
        else if (Powers[randomPower].Power == PowerSelection.Health)
        {
            Movement.Instance.GetComponent<Health>().Heal(Powers[randomPower].IncreaseValue);
        }
    }

    private void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;

        if (shrink)
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 10f * Time.deltaTime);
        else
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 10f * Time.deltaTime);
    }

    void ShrinkObject()
    {
        shrink = true;
        Destroy(gameObject, 1f);
    }

    public void PopUpPower(float Duration = 2, float speed = 5)
    {
        Invoke(nameof(ShrinkObject), Duration);
        moveSpeed = speed;
    }
}
