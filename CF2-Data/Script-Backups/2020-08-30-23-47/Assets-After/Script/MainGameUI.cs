using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameUI : MonoBehaviour
{
    public Text timeText;
    public Text killText;
    public GameObject StartText;
    public Image HPGuage;
    public Image BPGuage;
    public Image bullet;

    private bool start = false;
    void Start()
    {
        Invoke("SetStart", 2);
    }
    void Update()
    {
        timeText.text = "Time : " + Mathf.Ceil(Player.Instance.timer).ToString() + " Sec";
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
        }
    }

    void SetStart()
    {
        StartText.SetActive(true);
        start = true;
    }
}
