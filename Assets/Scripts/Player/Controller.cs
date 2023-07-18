using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rig;
    private bool isAlive;
    private PlayerShoot shooter;
    private EnemyController enemyController;
    private SpriteRenderer playerSprite;
    public float xMin, xMax, yMin, yMax;

    [SerializeField] private int playerLife;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        shooter = GetComponentInChildren<PlayerShoot>();
        enemyController = FindObjectOfType<EnemyController>();
        playerSprite = GetComponent<SpriteRenderer>();

    }
    
    void Start()
    {
        isAlive = true;
    }

    void Update()
    {
        float x = Mathf.Clamp(transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(transform.position.y, yMin, yMax);

        transform.position = new Vector3(x, y, 0);
        Shooting();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (isAlive)
        {
            float movX = Input.GetAxis("Horizontal");
            float movY = Input.GetAxis("Vertical");
        
            rig.velocity = new Vector2(movX, movY) * speed;
        }
    }

    private void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shooter.Shooting();
        }
    }

    public void Hurt(int hurt)
    {
        playerLife -= hurt;

        if (playerLife <= 0)
        {
            isAlive = false;
            UIController.instance.EndGameText("Game Over!");
            enemyController.PlayerIsdead();
            playerSprite.enabled = false;
        }
    }
}
