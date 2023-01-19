using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiagManager : MonoBehaviour
{
    #region singleton
    private static DiagManager instance = null;

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

    public static DiagManager Instance
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

    public Diag[] diags;
    public GameObject diagBox;
    public Text diagText;
    public bool endDiag = false;
    private int count = 0;
    private bool isDiag = false;

    void Start()
    {
        ShowDiag();
    }

    public void ShowDiag()
    {
        diagBox.SetActive(true);
        count = 0;
        isDiag = true;
        diagText.text = diags[count].diag;
    }

    private void NextDiag()
    {
        count++;
        diagText.text = diags[count].diag;
    }

    private void EndDiag()
    {
        diagBox.SetActive(false);
        count = 0;
        isDiag = false;
    }

    void Update()
    {
        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            if(count < diags.Length - 1)
            {
                NextDiag();
                SoundManager.instance.Play("Button");
            }
            else
            {
                EndDiag();
                endDiag = true;
            }
        }
    }

}
