using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndUI : MonoBehaviour
{
    public Text timeScoreText;
    public Text killScoreText;
    public Text totlaScoreText;

    public GameObject time;
    public GameObject kill;
    public GameObject total;

    void Start()
    {
        int time = 0;

        if (GameManager.Instance.timeLimit > 0 && GameManager.Instance.timeLimit < 5)
        {
            time = 10;
            timeScoreText.text = "Time Score : " + time + " C grade";
        }
        else if (GameManager.Instance.timeLimit >= 5 && GameManager.Instance.timeLimit < 10)
        {
            time = 20;
            timeScoreText.text = "Time Score : " + time + " B grade!";
        }
        else
        {
            time = 50;
            timeScoreText.text = "Time Score : " + time + " A grade!!";
        }

        killScoreText.text = "Kill Score : " + GameManager.Instance.score;
        totlaScoreText.text = "Total Score : " + (time + GameManager.Instance.score);

        Invoke("TimeScore", 1.0f);
        Invoke("KillScore", 2.0f);
        Invoke("TotalScore", 3.0f);

        GameManager.Instance.gameStart = false;
        GameManager.Instance.score = 0;
        GameManager.Instance.timeLimit = 60.0f;
        GameManager.Instance.playerHp = 100.0f;
        GameManager.Instance.boostEnerge = 100.0f;
        GameManager.Instance.curStage = 1;
}

    public void RetryButton()
    {
        SoundManager.instance.Play("Button");
        GameManager.Instance.playerHp = 100.0f;
        GameManager.Instance.SceneChange("GameScene");
    }
    public void TimeScore()
    {
        time.transform.DOMove(new Vector2(0, 2), 0.5f, false);
    }
    public void KillScore()
    {
        kill.transform.DOMove(new Vector2(0, 1), 0.5f, false);
    }
    public void TotalScore()
    {
        total.transform.DOMove(new Vector2(0, 0), 0.5f, false);
    }

    public void QuitButton()
    {
        SoundManager.instance.Play("Button");
        Application.Quit();
    }
}
