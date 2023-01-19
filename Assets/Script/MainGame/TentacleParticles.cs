using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TentacleParticles : MonoBehaviour
{
    public SpriteRenderer image;
    public bool isLeft;
    private bool switchColor;
    private float count = 0;
    private bool isMoving;
    private void Update()
    {
        if (Vector3.Distance(Player.Instance.transform.position, transform.position) < 4 && Player.Instance.pState == Player.PlayerState.Alive)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, count);

            if(!switchColor)
            {
                count += Time.deltaTime;
                if(count > 0.5f)
                {
                    switchColor = true;
                }
            }
            else
            {
                count -= Time.deltaTime;
            }

            if(isLeft && !isMoving)
            {
                isMoving = true;
                transform.DOMove(new Vector3(3, transform.position.y, 0), 5.0f, false);
            }
            else if(!isLeft)
            {
                isMoving = true;
                transform.DOMove(new Vector3(-3, transform.position.y, 0), 5.0f, false);
            }
        }
    }
}