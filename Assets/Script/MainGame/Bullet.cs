using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2;
    public float lifespan = 3.0f;
    private bool live = true;

    void Start()
    {
        SoundManager.instance.Play("Attack");
        Destroy(gameObject, lifespan);
    }

    void Update()
    {
        if (live)
            Moving();
    }

    private void Moving()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - (Time.deltaTime * speed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TentacleR") || collision.CompareTag("TentacleL") || collision.CompareTag("Bomb") || collision.CompareTag("Fish"))
            Destroy(gameObject);
    }
}
