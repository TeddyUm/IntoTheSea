using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] item;
    public float itemSpawnTimer;

    private int itemNum;
    private bool canItemSpawn = true;

    void Start()
    {

    }

    void Update()
    {
        if (canItemSpawn && Player.Instance.pState == Player.PlayerState.Alive)
            StartCoroutine(SpawnItem());
    }

    IEnumerator SpawnItem()
    {
        Vector2 itemVec = new Vector2(Random.Range(-2f, 2f), Player.Instance.transform.position.y - 10);
        canItemSpawn = false;
        itemNum = Random.Range(0, 3);
        switch (itemNum)
        {
            case 0:
                Instantiate(item[0], itemVec, Quaternion.identity);
                break;
            case 1:
                Instantiate(item[1], itemVec, Quaternion.identity);
                break;
            case 2:
                Instantiate(item[2], itemVec, Quaternion.identity);
                break;
        }
        yield return new WaitForSeconds(itemSpawnTimer);
        canItemSpawn = true;
    }
}
