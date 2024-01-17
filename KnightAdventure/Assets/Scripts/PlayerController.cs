using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f; //화살표 입력값
    public float speed = 3.0f;  //이동 속도

    public float jump = 9.0f;   //점프력
    public LayerMask groundLayer;   //착지할 수 있는 레이어
    bool goJump = false;    //점프 개시 플래그
    bool onGround = false;  //지면에 서있는 플래그

    Animator animator; //애니메이터
    public string idleAnime = "PlayerIdle";
    public string walkAnime = "PlayerWalk";
    public string jumpAnime = "PlayerJump";
    public string runAnime = "PlayerRun";
    public string deadAnime = "PlayerDead";
    public string attack1Anime = "PlayerAttack1";
    public string attack2Anime = "PlayerAttack2";
    public string hitAnime = "PlayerHit";
    string nowAnime = "";   //현재 애니메이션
    string oldAnime = "";   //이전 실행된 애니메이션

    //플레이어 체력
    public static int hp = 3;
    public static string gameState;
    bool inDamage = false;

    //공격 딜레이
    private float curTime;
    public float coolTime;
    public Transform pos;
    public Vector2 boxSize;

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        //Animator 가져오기
        animator = GetComponent<Animator>();
        nowAnime = idleAnime;
        oldAnime = idleAnime;
        gameState = "playing";

        //HP 불러오기
        hp = PlayerPrefs.GetInt("PlayerHP");    //PlayerPrefs : 데이터를 저장하거나 불러오기
    }

    // Update is called once per frame
    void Update()
    {
        //키보드 좌우 화살표 입력 확인
        axisH = Input.GetAxisRaw("Horizontal");
        //방향 조절
        if(axisH > 0.0f)
        {
            Debug.Log("오른쪽 이동");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            Debug.Log("왼쪽 이동");
            transform.localScale = new Vector2(-1, 1);
        }

        //점프
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //게임중 or 데미지 받으면 행동X
        if (gameState != "playing" || inDamage)
        {
            return;
        }

        //'Z'버튼으로 공격
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

    //동작 업데이트
    private void FixedUpdate()
    {
        //착지판정
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if(onGround || axisH != 0)
        {
            //지면 위 또는 속도가 0이 아닐때
            //이동속도
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        if(onGround && goJump)
        {
            //지면 위에서 점프작동
            Debug.Log("점프");
            Vector2 jumpPw = new Vector2(0, jump);  //점프를 위한 벡터 생성
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);    //순간적인 힘 가하기
            goJump = false; //점프완료
        }
        if(onGround)    //지면 위
        {
            if (axisH == 0) nowAnime = idleAnime;
            else nowAnime = walkAnime; 
        }
        else    //공중
        {
            nowAnime = jumpAnime;
        }

        // 게임중X or 데미지를 입었을때
        if (gameState != "playing")
        {
            return;
        }
        if (inDamage)   //데미지를 받는 중 플레이어 점멸
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
            return; //데미지를 받는 동안 조작불가
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;    //애니메이션 변환
            animator.Play(nowAnime);    //애니메이션 재생
        }
    }
    //점프
    public void Jump()
    {
        goJump = true;
        Debug.Log("점프");
    }

    //접촉 이벤트
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            GetDamage(collision.gameObject);
        }
    }

    //데미지 
    void GetDamage(GameObject enemy)
    {
        if(gameState == "playing")
        {
            hp--;
            //HP 갱신하기
            PlayerPrefs.SetInt("PlayerHP", hp);
            if(hp > 0)
            {
                rbody.velocity = new Vector2(0, 0);
                //튀어오르는 연출
                rbody.AddForce(new Vector2(0, 5.0f), ForceMode2D.Impulse);
                //데미지 받는 동안
                inDamage = true;
                Invoke("DamageEnd", 0.5f);  //Invoke : 해당 함수 지연
            }
            else
            {
                GameOver();
            }
        }
    }
    //데미지 받음
    void DamageEnd()
    {
        inDamage = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        animator.Play(idleAnime);
    }

    public void GameOver()
    {
        Debug.Log("게임오버");
        gameState = "gameover";
        animator.Play(deadAnime);
        Destroy(gameObject, 1.0f);
    }
}
