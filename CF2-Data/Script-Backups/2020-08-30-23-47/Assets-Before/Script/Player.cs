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
        Invinsible, Alive, Dead, End
    }

    public float horizontalSpeed = 1.0f;
    public float bulletTimer = 2.0f;
    public Camera cam;
    public bool canFire = true;
    public GameObject bullet;
    public GameObject particle;
    public float timer;
    public BoxCollider2D cCol;
    public PlayerState pState = PlayerState.Alive;
    public GameObject winText;

    public Button Up;
    public Button Down;
    public Button Left;
    public Button Right;

    private Rigidbody2D rigidBody;
    private SpriteRenderer pImage;
    private float thrustPower = 0f;
    private Vector2 thVec;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponentInChildren<Rigidbody2D>();
        pImage = GetComponentInChildren<SpriteRenderer>();
        timer = 0;
        cCol.enabled = false;
        pState = PlayerState.Invinsible;
        transform.DOMove(new Vector3(0, 3.0f, 0), 2.0f, false);
    }

    // Update is called once per frame
    void Update()   
    {
        if (pState == PlayerState.Alive)
        {
            timer += Time.deltaTime;

            if (GameManager.Instance.playerHp <= 0.0f)
                pState = PlayerState.Dead;

            if (pState == PlayerState.Alive)
            {
                RespondToThrustInput();
                if (Input.GetKeyDown(KeyCode.Space) && canFire)
                    StartCoroutine(BulletFire());
            }

            cam.transform.position = new Vector3(0, rigidBody.transform.position.y, -10);
            if (rigidBody.velocity.y > 1f)
            {
                rigidBody.velocity = new Vector3(0, 1f, 0);
            }
            else if (rigidBody.velocity.y < -1f)
            {
                rigidBody.velocity = new Vector3(0, -1f, 0);
            }

            Vector2 downVec = new Vector2(0, -0.05f);
            rigidBody.AddForce(downVec);
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
        float x = Input.GetAxis("Horizontal");
        Vector2 vec = new Vector2(x * horizontalSpeed * Time.deltaTime, 0);
        transform.Translate(vec);

        thVec = new Vector2(0, thrustPower);
        rigidBody.AddForce(thVec);

        if (Input.GetKey(KeyCode.W) && GameManager.Instance.boostEnerge > 0)
        {
            if (thrustPower < 0.2f)
                thrustPower += 0.2f * Time.deltaTime;
            particle.transform.eulerAngles = new Vector3(0, 0, 0);
            particle.transform.position = transform.position;
            particle.SetActive(true);
            GameManager.Instance.boostEnerge -= Time.deltaTime * 30;
        }
        else if (Input.GetKey(KeyCode.S) && GameManager.Instance.boostEnerge > 0)
        {
            if (thrustPower > -0.3f)
                thrustPower -= 0.3f * Time.deltaTime;
            particle.transform.eulerAngles = new Vector3(0, 0, 180);
            particle.transform.position = transform.position;
            particle.SetActive(true);
            GameManager.Instance.boostEnerge -= Time.deltaTime * 10;
        }
        else if (Input.GetKey(KeyCode.A) && GameManager.Instance.boostEnerge > 0)
        {
            particle.transform.eulerAngles = new Vector3(0, 0, 90);
            particle.transform.position = transform.position;
            particle.SetActive(true);
            GameManager.Instance.boostEnerge -= Time.deltaTime * 10;
        }
        else if (Input.GetKey(KeyCode.D) && GameManager.Instance.boostEnerge > 0)
        {
            particle.transform.eulerAngles = new Vector3(0, 0, 270);
            particle.transform.position = transform.position;
            particle.SetActive(true);
            GameManager.Instance.boostEnerge -= Time.deltaTime * 10;
        }
        else
        {
            thrustPower = 0.0f;
            particle.SetActive(false);
            if (GameManager.Instance.boostEnerge < 100.0f)
                GameManager.Instance.boostEnerge += Time.deltaTime * 20;
            else
                GameManager.Instance.boostEnerge = 100.0f;
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
        if (collision.CompareTag("Wall"))
        {
            if (transform.position.x > 0)
            {
                GameManager.Instance.playerHp -= 10;
                transform.DOMoveX(1.5f, 1f, false);
            }
            else
            {
                GameManager.Instance.playerHp -= 10;
                transform.DOMoveX(-1.5f, 1f, false);
            }
        }

        if (collision.CompareTag("End"))
        {
            pState = PlayerState.End;
            rigidBody.velocity = new Vector3(0, 0, 0);

            winText.SetActive(true);
            Invoke("Win", 2.0f);
        }
    }

    void Win()
    {
        GameManager.Instance.SceneChange("Ending");
    }
}
