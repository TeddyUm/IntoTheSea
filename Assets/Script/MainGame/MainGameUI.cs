using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainGameUI : MonoBehaviour
{
    public Text timeText;
    public Text killText;
    public GameObject dieText;
    public GameObject StartText;
    public Image HPGuage;
    public Image BPGuage;
    public Image bullet;

    private bool start = false;
    void Start()
    {
        SoundManager.instance.Play("Bubble");
    }
    void Update()
    {
        if(DiagManager.Instance.endDiag == true)
        {
            DiagManager.Instance.endDiag = false;
            Invoke("SetStart", 2);
            Player.Instance.transform.DOMove(new Vector3(0, 3.0f, 0), 2.0f, false);
        }

        timeText.text = "Time : " + Mathf.Ceil(GameManager.Instance.timeLimit).ToString() + " Sec";
        killText.text = "Kill   : " + GameManager.Instance.score;
        HPGuage.fillAmount = GameManager.Instance.playerHp / 100.0f;
        BPGuage.fillAmount = GameManager.Instance.boostEnerge / 100.0f;

        if (Player.Instance.canFire == true)
            bullet.enabled = true;
        else
            bullet.enabled = false;

        if(ControlFreak2.CF2Input.GetKeyDown(KeyCode.Space) && start)
        {
            start = false;
            Player.Instance.pState = Player.PlayerState.Alive;
            StartText.SetActive(false);
            SoundManager.instance.Play("Button");
        }

        if(GameManager.Instance.playerHp <= 0 || GameManager.Instance.timeLimit <= 0)
        {
            dieText.SetActive(true);
            dieText.transform.DOMove(new Vector3(Player.Instance.transform.position.x, 
                Player.Instance.transform.position.y + 1.0f, Player.Instance.transform.position.z), 2.0f, false);
        }
    }

    void SetStart()
    {
        StartText.SetActive(true);
        start = true;
    }
}
