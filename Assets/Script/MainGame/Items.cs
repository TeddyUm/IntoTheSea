using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Items : MonoBehaviour
{
    public enum ItemType
    {
        Heal, Booster, Bomb
    }
    public GameObject explosion;
    public float speed = 0.2f;
    public ItemType itemType;

    private bool live = true;

    void Start()
    {
        
    }

    // Update is called once per frame
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
            SoundManager.instance.Play("Item");
            live = false;
            if (itemType == ItemType.Bomb)
            {
                Player.Instance.bulletTimer -= 0.2f;
            }
            else if (itemType == ItemType.Heal)
            {
                GameManager.Instance.playerHp += 20;
                if (GameManager.Instance.playerHp > 100)
                    GameManager.Instance.playerHp = 100;
            }
            else if (itemType == ItemType.Booster)
            {
                GameManager.Instance.boostEnerge += 20;
                if (GameManager.Instance.boostEnerge > 100)
                    GameManager.Instance.boostEnerge = 100;
            }
            explosion.SetActive(true);
            Destroy(gameObject, 1.0f);
        }
        else if (collision.tag == "Upper")
        {
            live = false;
            explosion.SetActive(true);
            Destroy(gameObject, 1.0f);
        }
    }
}
