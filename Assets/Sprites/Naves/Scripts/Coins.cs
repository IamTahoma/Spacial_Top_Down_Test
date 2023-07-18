using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private EnemyController enemyController;

    private Transform firstPoint;
    private Transform mediumPoint;
    private Transform finalPoint;
    private SpriteRenderer spriteShip;
    private Collider2D colShip;
    private bool isDestroyed;
    private AudioSource enemySound;

    [SerializeField] float speed;
    [SerializeField] int pointsammount;
    [SerializeField] bool isSpecialShip;

    private void Awake()
    {
        colShip = GetComponent<Collider2D>();
        enemySound = GetComponent<AudioSource>();
        spriteShip = GetComponent<SpriteRenderer>();
        enemyController = GameObject.FindObjectOfType<EnemyController>();
    }

    private void OnEnable()
    {
        colShip.enabled = true;
        spriteShip.enabled = true;
        isDestroyed = false;
        SetRoute();
        transform.position = firstPoint.position;   
    }


    private void Update()
    {
        if (isSpecialShip == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, mediumPoint.position, speed * Time.deltaTime);
            if (transform.position == mediumPoint.position)
            {
                isSpecialShip = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, finalPoint.position, speed * Time.deltaTime);
        }

        if (transform.position == finalPoint.position)
        {
            DisableShip();
        }
    }

    private void SetRoute()
    {
        for (int i = -1; i < pointsammount; i++)
        {
            switch (i)
            {
                case 0:
                    firstPoint = enemyController.SetWayPoint(i);
                    break;
                case 1:
                    finalPoint = enemyController.SetWayPoint(i);
                    break;
                case 2:
                    mediumPoint = enemyController.SetWayPoint(i);
                    break;
            }
        }
    }

    public void Exploit()
    {
        enemySound.Play();
        UIController.instance.AddPoint(+5);
        colShip.enabled = false;
        spriteShip.enabled = false;
        isDestroyed = true;
        Invoke(nameof(DisableShip), 1.5f);
    }

    private void DisableShip()
    {
        this.gameObject.SetActive(false);
    }
}
