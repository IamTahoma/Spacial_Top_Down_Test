using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public Transform bossPosition;
    private bool startAttack;
    private Animator anim;
    private SpriteRenderer bossSprite;
    private BoxCollider2D col;
    private bool isDestroyed;
    private AudioSource bossSound;

    [SerializeField] int bossLives;
    [SerializeField] float bossSpeed;
    [SerializeField] ParticleSystem [] arrayDestroyBoss;
    [SerializeField] GameObject shooter;

    private void Awake()
    {
        bossSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        bossSprite = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        bossPosition = GameObject.Find("BossWayPoint").transform;
    }

    private void Start()
    {
        anim.enabled = false;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, bossPosition.position, bossSpeed * Time.deltaTime);
      
        if (transform.position == bossPosition.position && startAttack == false)
        {
            startAttack = true;
            AttackPosition();
        }
    }

    private void AttackPosition()
    {
        anim.enabled = true;
        anim.SetTrigger("StartAttack");
        StartCoroutine(BossShooting());
    }


    public void SubtracLives()
    {
        bossLives--;
        if (bossLives <= 0)
        {
            DisableBoss();
        }
    }

    private void DisableBoss()
    {
        isDestroyed = true;
        bossSprite.enabled = false;
        col.enabled = false;
        anim.enabled = false;
        StartCoroutine(DestroyBoss());
    }

    IEnumerator DestroyBoss()
    {
        for (int i = 0; i < arrayDestroyBoss.Length; i++)
        {
            arrayDestroyBoss[i].Play();
            bossSound.Play();
            yield return new WaitForSeconds(0.1f);
        }

        Invoke(nameof(Destruction), 2f);
    }

    IEnumerator BossShooting()
    {
        for (int i = 0; i < 10; i++)
        {
            i = 0;
            float tiempo = Random.Range(0.3f, 0.8f);
            yield return new WaitForSeconds(tiempo);
            if (isDestroyed == false)
            {
                Instantiate(Resources.Load("EnemyLaser"), shooter.transform.position, Quaternion.identity);
            }
        }
    }

    private void Destruction()
    {
        UIController.instance.AddPoint(+20);
        UIController.instance.EndGameText("You Win!");
        Destroy(this.gameObject);
    }
}
