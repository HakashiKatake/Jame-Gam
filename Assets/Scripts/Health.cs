using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] int ScoreAdd;
    [Header("References")]
    [SerializeField] Slider HpSlider;
    [SerializeField] SpriteRenderer[] ObjectSprite;

    [Header("Values")]
    [SerializeField] float MaxHp = 10f;
    [SerializeField] float CurrentHp = 10f;
    [Space]
    [SerializeField] float DammageFlashDuration = 0.05f;
    [SerializeField] float HealFlashDuration = 0.05f;
    Color[] OriginColor;
    [SerializeField] enum DeathMode { RestartGame, DestroyObject, Nothing };
    public bool Dead;
    [Space]
    [SerializeField] DeathMode AfterDeath = DeathMode.DestroyObject;
    public bool Shielded = false;

    [Header("Sounds")]
    [SerializeField] AudioSource _Audio;
    [SerializeField] AudioClip Hit;

    [Header("Particles")]
    [SerializeField] GameObject DeathParticle;

    [Header("Knockback")]
    public float knockbackForce;
    private Rigidbody2D rb;

    private void Start()
    {
        CurrentHp = MaxHp;
        rb = GetComponent<Rigidbody2D>();

        OriginColor = new Color[ObjectSprite.Length];
        if (ObjectSprite != null)
        {
            for (int i = 0; i < ObjectSprite.Length; i++)
            {
                OriginColor[i] = ObjectSprite[i].color;
            }
        }
    }

    private void Update()
    {
        if (Dead)
            return;

        //limit the current hp from going over the max hp and update the values
        if (CurrentHp > MaxHp)
            CurrentHp = MaxHp;

        if (HpSlider != null)
        {
            HpSlider.maxValue = MaxHp;
            HpSlider.value = CurrentHp;
        }

        //death code
        if (CurrentHp <= 0)
        {
            if (AfterDeath == DeathMode.DestroyObject)
            {
                if (DeathParticle != null)
                {
                    Instantiate(DeathParticle, transform.position, DeathParticle.transform.rotation);
                }

                GameManager.Instance.Score += ScoreAdd * (int)transform.localScale.x;

                Destroy(gameObject);
            }
            else if (AfterDeath == DeathMode.RestartGame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (AfterDeath == DeathMode.Nothing)
            {
                Dead = true;
            }
        }
    }

    public void ChangeMaxHealth(float number)
    {
        if (Dead)
            return;

        MaxHp = number;
    }

    public void Dammage(float number = 1)
    {
        if (Dead)
            return;

        if (Shielded)
        {
            if (ObjectSprite != null)
                ResetColor();
            Shielded = false;
            return;
        }

        CurrentHp -= number;

        if (ObjectSprite != null)
            DammageFlash();

        if (_Audio != null)
            _Audio.PlayOneShot(Hit);
    }

    public void Shield()
    {
        if (ObjectSprite != null)
        {
            for (int i = 0; i < ObjectSprite.Length; i++)
            {
                ObjectSprite[i].color = Color.blue;
            }
        }
        Shielded = true;
    }

    public void Heal(float number = 1)
    {
        if (Dead)
            return;

        CurrentHp += number;

        if (ObjectSprite != null)
            HealFlash();
    }

    [ContextMenu("Dammage by one")]
    void DammageByOne()
    {
        Dammage(1);
    }

    [ContextMenu("Heal by one")]
    void HealByOne()
    {
        Heal(1);
    }

    void DammageFlash()
    {
        for (int i = 0; i < ObjectSprite.Length; i++)
        {
            ObjectSprite[i].color = Color.red;
        }
        Invoke(nameof(ResetColor), DammageFlashDuration);
    }

    void HealFlash()
    {
        for (int i = 0; i < ObjectSprite.Length; i++)
        {
            ObjectSprite[i].color = Color.green;
        }
        Invoke(nameof(ResetColor), HealFlashDuration);
    }

    void ResetColor()
    {
        for (int i = 0; i < ObjectSprite.Length; i++)
        {
            ObjectSprite[i].color = OriginColor[i];
        }
    }

    public void ApplyKnockback(Vector2 direction)
    {
        if (rb != null)
        {
            rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
