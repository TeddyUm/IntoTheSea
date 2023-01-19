using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    #endregion

    public bool gameStart = false;
    public int score = 0;
    public float timeLimit = 60.0f;
    public float playerHp = 100.0f;
    public float boostEnerge = 100.0f;
    public int curStage = 1;

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
