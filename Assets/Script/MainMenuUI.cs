using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject creditPanel;

    void Start()
    {
        SoundManager.instance.Play("MainBGM");
    }

    public void StartButton()
    {
        SoundManager.instance.Play("Button");
        SoundManager.instance.Stop("MainBGM");
        GameManager.Instance.SceneChange("GameScene");
    }

    public void CreditButtonOpen()
    {
        SoundManager.instance.Play("Button");
        creditPanel.SetActive(true);
    }
    public void CreditButtonClose()
    {
        SoundManager.instance.Play("Button");
        creditPanel.SetActive(false);
    }

    public void QuitButton()
    {
        SoundManager.instance.Play("Button");
        Application.Quit();
    }
}
