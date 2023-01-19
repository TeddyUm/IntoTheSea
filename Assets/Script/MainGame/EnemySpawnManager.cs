using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject bomb;
    public GameObject Pratform;
    public float bombSpawnTimer;
    private bool canBombSpawn = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (canBombSpawn && Player.Instance.pState == Player.PlayerState.Alive)
            StartCoroutine(SpawnBomb());
    }

    IEnumerator SpawnBomb()
    {
        Vector2 bombVec = new Vector2(Random.Range(-2.2f, 2.2f), Player.Instance.transform.position.y -10);
        canBombSpawn = false;
        Instantiate(bomb, bombVec, Quaternion.identity);
        yield return new WaitForSeconds(bombSpawnTimer);
        canBombSpawn = true;
    }
}
