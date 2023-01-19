using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;
    public float speed = 0.2f;
    public Collider2D col;
    private bool live = true;

    void Start()
    {
    }

    void Update()
    {
        if (live)
            Moving();
    }

    private void Moving()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + (Time.deltaTime * speed));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.playerHp -= 10.0f;
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
            GameManager.Instance.score += 1;
            live = false;
            explosion.SetActive(true);
            SoundManager.instance.Play("EnemyDie");
            col.enabled = false;
            Destroy(gameObject, 1.0f);
        }
        else if (collision.tag == "Upper")
        {
            live = false;
            Destroy(gameObject);
        }
    }
}
