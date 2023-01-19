using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region singleton
    private static Player instance = null;

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

    public static Player Instance
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
    public enum PlayerState 
    {
        Invinsible, Alive, Dead, End, Event
    }

    public float horizontalSpeed = 1.0f;
    public float bulletTimer = 2.0f;
    public Camera cam;
    public bool canFire = true;
    public GameObject bullet;
    public GameObject particle;
    public GameObject hitParticle;
    public GameObject deathParticle;
    public BoxCollider2D cCol;
    public PlayerState pState = PlayerState.Alive;
    public GameObject winText;
    public float thrustPower = 0f;

    private Rigidbody2D rigidBody;
    private SpriteRenderer pImage;
    private Vector2 thVec;
    private bool engineSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponentInChildren<Rigidbody2D>();
        pImage = GetComponentInChildren<SpriteRenderer>();
        cCol.enabled = false;
        pState = PlayerState.Invinsible;
    }

    // Update is called once per frame
    void Update()   
    {
        if (pState == PlayerState.Alive)
        {
            if (GameManager.Instance.timeLimit > 0)
                GameManager.Instance.timeLimit -= Time.deltaTime;
            else
                GameManager.Instance.timeLimit = 0;

            if (GameManager.Instance.playerHp <= 0.0f)
            {
                pState = PlayerState.Dead;
                deathParticle.transform.position = transform.position;
                deathParticle.SetActive(true);
            }

            if (pState == PlayerState.Alive)
            {
                RespondToThrustInput();
                if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Space) && canFire)
                    StartCoroutine(BulletFire());
            }

            cam.transform.position = new Vector3(0, rigidBody.transform.position.y, -10);
            if (rigidBody.velocity.y > 0)
            {
                rigidBody.velocity = new Vector3(0, 0, 0);
            }
            else if (rigidBody.velocity.y < -1.2f)
            {
                rigidBody.velocity = new Vector3(0, -1.2f, 0);
            }

            Vector2 downVec = new Vector2(0, -0.03f);
            rigidBody.AddForce(downVec);
        }
        else if(pState == PlayerState.Dead)
        {
            deathParticle.SetActive(true);
            deathParticle.transform.position = transform.position;
            rigidBody.velocity = new Vector3(0, 0, 0);
            SoundManager.instance.Play("game_over_16");
            Invoke("Die", 3.0f);
        }

        if (GameManager.Instance.timeLimit <= 0)
        {
            pState = PlayerState.Dead;
        }
    }

    IEnumerator BulletFire()
    {
        canFire = false;
        Instantiate(bullet, transform.position, quaternion.identity);
        yield return new WaitForSeconds(bulletTimer);
        canFire = true;
    }

    void RespondToThrustInput()
    {
        float x = - ControlFreak2.CF2Input.GetAxis("Horizontal");
        if (GameManager.Instance.boostEnerge > 0.0f)
        {
            Vector2 vec = new Vector2(x * horizontalSpeed * Time.deltaTime, 0);
            transform.Translate(vec);
        }
        else
        {
            Vector2 vec = new Vector2(0, 0);
            transform.Translate(vec);
        }

        if (!engineSound)
        {
            engineSound = true;
            SoundManager.instance.Play("Engine");
        }

        thVec = new Vector2(0, thrustPower);
        rigidBody.AddForce(thVec);

        if (ControlFreak2.CF2Input.GetAxis("Vertical") < 0 && GameManager.Instance.boostEnerge > 0)
        {
            if (thrustPower < 0.3f)
                thrustPower += 0.3f * Time.deltaTime;
            particle.transform.eulerAngles = new Vector3(0, 0, 0);
            particle.transform.position = transform.position;
            particle.SetActive(true);
            GameManager.Instance.boostEnerge -= Time.deltaTime * 30;
            if (!engineSound)
            {
                engineSound = true;
                SoundManager.instance.Play("Engine");
            }
        }
        else if (ControlFreak2.CF2Input.GetAxis("Vertical") > 0 && GameManager.Instance.boostEnerge > 0)
        {
            if (thrustPower > -0.3f)
                thrustPower -= 0.3f * Time.deltaTime;
            particle.transform.eulerAngles = new Vector3(0, 0, 180);
            particle.transform.position = transform.position;
            particle.SetActive(true);
            GameManager.Instance.boostEnerge -= Time.deltaTime * 15;
            if (!engineSound)
            {
                engineSound = true;
                SoundManager.instance.Play("Engine");
            }
        }
        else if (ControlFreak2.CF2Input.GetAxis("Horizontal") > 0 && GameManager.Instance.boostEnerge > 0)
        {
            particle.transform.eulerAngles = new Vector3(0, 0, 90);
            particle.transform.position = transform.position;
            particle.SetActive(true);
            GameManager.Instance.boostEnerge -= Time.deltaTime * 15;
            if (!engineSound)
            {
                engineSound = true;
                SoundManager.instance.Play("Engine");
            }
        }
        else if (ControlFreak2.CF2Input.GetAxis("Horizontal") < 0 && GameManager.Instance.boostEnerge > 0)
        {
            particle.transform.eulerAngles = new Vector3(0, 0, 270);
            particle.transform.position = transform.position;
            particle.SetActive(true);
            GameManager.Instance.boostEnerge -= Time.deltaTime * 15;
            if (!engineSound)
            {
                engineSound = true;
                SoundManager.instance.Play("Engine");
            }
        }
        else
        {
            thrustPower = 0.0f;
            particle.SetActive(false);
            if (GameManager.Instance.boostEnerge < 100.0f)
                GameManager.Instance.boostEnerge += Time.deltaTime * 20;
            else
                GameManager.Instance.boostEnerge = 100.0f;

            engineSound = false;
            SoundManager.instance.Stop("Engine");
        }

        if (x < 0)
        {
            pImage.flipX = true;
        }
        else if (x > 0)
        {
            pImage.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WallR"))
        {
            GameManager.Instance.playerHp -= 10;
            transform.DOMoveX(transform.position.x - 1f, 1f, false);
        }
        else if (collision.CompareTag("WallL"))
        {  
            GameManager.Instance.playerHp -= 10;
            transform.DOMoveX(transform.position.x + 1f, 1f, false);
        }

        if (collision.CompareTag("TentacleR") || collision.CompareTag("TentacleL"))
        {
            GameManager.Instance.playerHp -= 30;
            transform.DOMoveY(transform.position.y + 2f, 2f, false);
            hitParticle.transform.position = transform.position;
            hitParticle.SetActive(true);
            SoundManager.instance.Play("PlayerHit");
            SpeedZero();
            Invoke("CloseParticle", 1.0f);
        }

        if (collision.CompareTag("End"))
        {
            pState = PlayerState.End;
            rigidBody.velocity = new Vector3(0, 0, 0);

            winText.SetActive(true);
            Invoke("Win", 2.0f);
        }
    }

    public void SpeedZero()
    {
        rigidBody.velocity *= 0.5f;
    }
    
    void Win()
    {
        SoundManager.instance.Stop("MainBGM");
        SoundManager.instance.Stop("Bubble");
        SoundManager.instance.Stop("Win");
        switch(GameManager.Instance.curStage)
        {
            case 1:
                GameManager.Instance.SceneChange("GameScene2");
                GameManager.Instance.curStage++;
                break;
            case 2:
                GameManager.Instance.SceneChange("GameScene3");
                GameManager.Instance.curStage++;
                break;
            case 3:
                GameManager.Instance.SceneChange("Ending");
                break;
        }
    }

    void Die()
    {
        SoundManager.instance.Stop("MainBGM");
        SoundManager.instance.Stop("Bubble");
        GameManager.Instance.SceneChange("GameOver");
    }

    void CloseParticle()
    {
        hitParticle.SetActive(false);
    }
}
