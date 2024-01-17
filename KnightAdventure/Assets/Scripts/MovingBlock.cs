using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;          
    public float moveY = 0.0f;          
    public float times = 0.0f;          
    public float weight = 0.0f;        
    public bool isMoveWhenOn = false;   //플레이어 접촉시 움직임

    public bool isCanMove = true;       //움직임
    float perDX;                        //１프레임 당 X이동 값
    float perDY;                        //１프레임 당 Y이동 값
    Vector3 defPos;                     //초기 위치
    bool isReverse = false;             //반전 여부

    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;
        float timestep = Time.fixedDeltaTime;
        perDX = moveX / (1.0f / timestep * times);  //１ 프레임당 X 이동 값 
        perDY = moveY / (1.0f / timestep * times);  //１ 프레임당 Y 이동 값

        if (isMoveWhenOn)
        {
            isCanMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (isCanMove)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;
            if (isReverse)
            {
                //반대 방향 이동
                if ((perDX >= 0.0f && x <= defPos.x) || (perDX < 0.0f && x >= defPos.x))
                {
                    endX = true;    
                }
                if ((perDY >= 0.0f && y <= defPos.y) || (perDY < 0.0f && y >= defPos.y))
                {
                    endY = true;    
                }
                //블록 이동
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z));
            }
            else
            {
                //정방향 이동
                if ((perDX >= 0.0f && x >= defPos.x + moveX) || (perDX < 0.0f && x <= defPos.x + moveX))
                {
                    endX = true;    
                }
                if ((perDY >= 0.0f && y >= defPos.y + moveY) || (perDY < 0.0f && y <= defPos.y + moveY))
                {
                    endY = true;    
                }
                //블록 이동
                transform.Translate(new Vector3(perDX, perDY, defPos.z));
            }

            if (endX && endY)
            {
                //이동 종료
                if (isReverse)
                {
                    //위치가 어긋나는것을 방지하기 위해 돌아가기 전에 초기 위치로 돌림
                    transform.position = defPos;
                }
                isReverse = !isReverse; //값을 반전
                isCanMove = false;      //이동 가능 값을 false
                if (isMoveWhenOn == false)  //플레이어가 발판을 밟았을때 움직임이 없음
                {
                    
                    Invoke("Move", weight);
                }
            }
        }
    }

    public void Move()
    {
        isCanMove = true;
    }

    public void Stop()
    {
        isCanMove = false;
    }

    //접촉 시작
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //접촉한것이 플레이어라면 이동 블록의 자식으로 만들기
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                //플레이어가 올라탔을 때 움직임
                isCanMove = true;   
            }
        }
    }
    //접촉 종료
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //접촉한것이 플레이어라면 이동 블록의 자식에서 제외시킴
            collision.transform.SetParent(null);
        }
    }
}
