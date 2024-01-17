using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f; //ȭ��ǥ �Է°�
    public float speed = 3.0f;  //�̵� �ӵ�

    public float jump = 9.0f;   //������
    public LayerMask groundLayer;   //������ �� �ִ� ���̾�
    bool goJump = false;    //���� ���� �÷���
    bool onGround = false;  //���鿡 ���ִ� �÷���

    Animator animator; //�ִϸ�����
    public string idleAnime = "PlayerIdle";
    public string walkAnime = "PlayerWalk";
    public string jumpAnime = "PlayerJump";
    public string runAnime = "PlayerRun";
    public string deadAnime = "PlayerDead";
    public string attack1Anime = "PlayerAttack1";
    public string attack2Anime = "PlayerAttack2";
    public string hitAnime = "PlayerHit";
    string nowAnime = "";   //���� �ִϸ��̼�
    string oldAnime = "";   //���� ����� �ִϸ��̼�

    //�÷��̾� ü��
    public static int hp = 3;
    public static string gameState;
    bool inDamage = false;

    //���� ������
    private float curTime;
    public float coolTime;
    public Transform pos;
    public Vector2 boxSize;

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        //Animator ��������
        animator = GetComponent<Animator>();
        nowAnime = idleAnime;
        oldAnime = idleAnime;
        gameState = "playing";

        //HP �ҷ�����
        hp = PlayerPrefs.GetInt("PlayerHP");    //PlayerPrefs : �����͸� �����ϰų� �ҷ�����
    }

    // Update is called once per frame
    void Update()
    {
        //Ű���� �¿� ȭ��ǥ �Է� Ȯ��
        axisH = Input.GetAxisRaw("Horizontal");
        //���� ����
        if(axisH > 0.0f)
        {
            Debug.Log("������ �̵�");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            Debug.Log("���� �̵�");
            transform.localScale = new Vector2(-1, 1);
        }

        //����
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //������ or ������ ������ �ൿX
        if (gameState != "playing" || inDamage)
        {
            return;
        }

        //'Z'��ư���� ����
        if(curTime <= 0)
        {
            if(Input.GetKey(KeyCode.Z))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if(collider.tag == "Enemy")
                    {
                        collider.GetComponent<EnemyController>().TakeDamage(1);
                    }
                }
                animator.SetTrigger("atk");
                curTime = coolTime;
            }        
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    //���� ������Ʈ
    private void FixedUpdate()
    {
        //��������
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if(onGround || axisH != 0)
        {
            //���� �� �Ǵ� �ӵ��� 0�� �ƴҶ�
            //�̵��ӵ�
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        if(onGround && goJump)
        {
            //���� ������ �����۵�
            Debug.Log("����");
            Vector2 jumpPw = new Vector2(0, jump);  //������ ���� ���� ����
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //�������� �� ���ϱ�
            goJump = false; //�����Ϸ�
        }
        if(onGround)    //���� ��
        {
            if (axisH == 0) nowAnime = idleAnime;
            else nowAnime = walkAnime; 
        }
        else    //����
        {
            nowAnime = jumpAnime;
        }

        // ������X or �������� �Ծ�����
        if (gameState != "playing")
        {
            return;
        }
        if (inDamage)   //�������� �޴� �� �÷��̾� ����
        {
            float val = Mathf.Sin(Time.time * 100);
            Debug.Log(val);
            if(val > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            animator.Play(hitAnime);
            return; //�������� �޴� ���� ���ۺҰ�
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;    //�ִϸ��̼� ��ȯ
            animator.Play(nowAnime);    //�ִϸ��̼� ���
        }
    }
    //����
    public void Jump()
    {
        goJump = true;
        Debug.Log("����");
    }

    //���� �̺�Ʈ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            GetDamage(collision.gameObject);
        }
    }

    //������ 
    void GetDamage(GameObject enemy)
    {
        if(gameState == "playing")
        {
            hp--;
            //HP �����ϱ�
            PlayerPrefs.SetInt("PlayerHP", hp);
            if(hp > 0)
            {
                rbody.velocity = new Vector2(0, 0);
                //Ƣ������� ����
                rbody.AddForce(new Vector2(0, 5.0f), ForceMode2D.Impulse);
                //������ �޴� ����
                inDamage = true;
                Invoke("DamageEnd", 0.5f);  //Invoke : �ش� �Լ� ����
            }
            else
            {
                GameOver();
            }
        }
    }
    //������ ����
    void DamageEnd()
    {
        inDamage = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        animator.Play(idleAnime);
    }

    public void GameOver()
    {
        Debug.Log("���ӿ���");
        gameState = "gameover";
        animator.Play(deadAnime);
        Destroy(gameObject, 1.0f);
    }
}
