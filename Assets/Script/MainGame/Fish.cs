using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public GameObject explosion;
    public float speed = 0.2f;
    public SpriteRenderer sprite;
    public Collider2D col;
    private bool goLeft = false;
    private bool live = true;

    void Start()
    {
        sprite.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (live)
            MovingFish();
    }

    void MovingFish()
    {
        if (goLeft)
        {
            transform.position = new Vector2(transform.position.x - (Time.deltaTime * speed), transform.position.y);
            sprite.flipX = true;
        }
        else
        {
            transform.position = new Vector2(transform.position.x + (Time.deltaTime * speed), transform.position.y);
            sprite.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.playerHp -= 20.0f;
            live = false;
            
            explosion.SetActive(true);
            Player.Instance.transform.DOMove(new Vector2(Player.Instance.transform.position.x,
                             Player.Instance.transform.position.y + 0.5f), 0.5f, false);
            Player.Instance.SpeedZero();
            col.enabled = false;
            SoundManager.instance.Play("PlayerHit");
            Destroy(gameObject, 1.0f);
        }
        else if (collision.CompareTag("Bullet"))
        {
            GameManager.Instance.score += 10;
            live = false;
            explosion.SetActive(true);
            SoundManager.instance.Play("EnemyDie");
            col.enabled = false;
            Destroy(gameObject, 1.0f);
        }

        if (collision.CompareTag("WallL") || collision.CompareTag("WallR"))
        {
            if (goLeft == true)
                goLeft = false;
            else
                goLeft = true;
        }
    }
}
