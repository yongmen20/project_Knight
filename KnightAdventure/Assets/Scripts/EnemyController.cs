using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rbody;
    public int hp = 3;
    public float speed = 3.0f;  //�̵� �ӵ�
    public string direction = "left";   //�̵� ����
    public float range = 0.0f;  //�̵� �Ÿ�
    Vector3 defPos; //���� ��ġ

    Animator animator;  //�ִϸ�����
    public string moveAnime = "EnemyMove";
    public string hitAnime = "EnemyHit";
    public string deadAnime = "EnemyDead";

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();

        if (direction == "right")
        {
            transform.localScale = new Vector2(-1, 1);  //���� ����
        }
        defPos = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(range > 0.0f)
        {
            if(transform.position.x < defPos.x - (range / 2))
            {
                direction = "right";
                transform.localScale = new Vector2(-1, 1);
            }
            if (transform.position.x > defPos.x + (range / 2))
            {
                direction = "left";
                transform.localScale = new Vector2(1, 1);
            }
        }
    }

    private void FixedUpdate()
    {
        //Rigidbody2D �Ӽ� ��������(�̵� �ӵ�)
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        if(direction == "right")
        {
            rbody.velocity = new Vector2(speed, rbody.velocity.y);
        }
        else
        {
            rbody.velocity = new Vector2(-speed, rbody.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //����
    {
        if(direction == "right")
        {
            direction = "left";
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            direction = "right";
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void TakeDamage(int damage)
    {
        hp = hp - damage;

        if(hp <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            rbody.velocity = new Vector2(0, 0);
            rbody.AddForce(new Vector2(0, 3.0f), ForceMode2D.Impulse);
            Animator animator = GetComponent<Animator>();
            animator.Play(deadAnime);
            Destroy(gameObject, 0.5f);
        }
    }
}
