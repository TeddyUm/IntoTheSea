using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class Tentacle : MonoBehaviour
{
    public bool rightTentacle = true;
    public bool isMoving = false;
    public float speed = 1.0f;
    public float range = 10;
    public GameObject collisionObject;
    void Update()
    {
        if (Vector3.Distance(Player.Instance.transform.position, collisionObject.transform.position) < range)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (rightTentacle == false && isMoving)
        {
            LeftTMoving();
        }
        else if (rightTentacle == true && isMoving)
        {
            RightTMoving();
        }
    }
    private void LeftTMoving()
    {
        transform.position = new Vector2(transform.position.x + (Time.deltaTime * speed), transform.position.y);
    }

    private void RightTMoving()
    {
        transform.position = new Vector2(transform.position.x - (Time.deltaTime * speed), transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("WallL") && rightTentacle)
        {
            speed *= -1;
            Invoke("Stop", 7);
        }
        if (collision.CompareTag("WallR") && !rightTentacle)
        {
            speed *= -1;
            Invoke("Stop", 7);
        }
    }

    void Stop()
    {
        isMoving = false;
    }
}
