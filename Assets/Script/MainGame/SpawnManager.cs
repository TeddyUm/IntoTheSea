using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SpawnManager : MonoBehaviour
{
    public GameObject bomb;
    public GameObject fish;
    public GameObject Pratform;
    private float bombSpawnTimer;
    private float fishSpawnTimer;
    private bool canBombSpawn = true;

    void Update()
    {
        if(canBombSpawn && GameManager.Instance.gameStart)
            StartCoroutine(SpawnBomb());
    }

    IEnumerator SpawnBomb()
    {
        Vector2 bombVec = new Vector2(Random.Range(Screen.width / 2 - 0.2f, -Screen.width / 2 + 0.2f), Screen.height);
        canBombSpawn = false;
        Instantiate(bomb, bombVec, Quaternion.identity);
        yield return new WaitForSeconds(2.0f);
        canBombSpawn = true;
    }
}
