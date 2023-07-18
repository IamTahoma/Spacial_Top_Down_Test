using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float speed = 10;

    private void OnEnable()
    {
        transform.position = transform.parent.position;
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);      
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            this.gameObject.SetActive(false);
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            this.gameObject.SetActive(false);
            AlienShip alienShip = collision.GetComponent<AlienShip>();
            alienShip.Exploit();
        }
        else if (collision.gameObject.CompareTag("Coins"))
        {
            this.gameObject.SetActive(false);
            Coins coins = collision.GetComponent<Coins>();
            coins.Exploit();
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            this.gameObject.SetActive(false);
            FinalBoss finalBoss = collision.GetComponent<FinalBoss>();
            finalBoss.SubtracLives();
        }
    }
}