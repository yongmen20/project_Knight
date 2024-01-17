using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;          
    public float moveY = 0.0f;          
    public float times = 0.0f;          
    public float weight = 0.0f;        
    public bool isMoveWhenOn = false;   //�÷��̾� ���˽� ������

    public bool isCanMove = true;       //������
    float perDX;                        //�������� �� X�̵� ��
    float perDY;                        //�������� �� Y�̵� ��
    Vector3 defPos;                     //�ʱ� ��ġ
    bool isReverse = false;             //���� ����

    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;
        float timestep = Time.fixedDeltaTime;
        perDX = moveX / (1.0f / timestep * times);  //�� �����Ӵ� X �̵� �� 
        perDY = moveY / (1.0f / timestep * times);  //�� �����Ӵ� Y �̵� ��

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
                //�ݴ� ���� �̵�
                if ((perDX >= 0.0f && x <= defPos.x) || (perDX < 0.0f && x >= defPos.x))
                {
                    endX = true;    
                }
                if ((perDY >= 0.0f && y <= defPos.y) || (perDY < 0.0f && y >= defPos.y))
                {
                    endY = true;    
                }
                //��� �̵�
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z));
            }
            else
            {
                //������ �̵�
                if ((perDX >= 0.0f && x >= defPos.x + moveX) || (perDX < 0.0f && x <= defPos.x + moveX))
                {
                    endX = true;    
                }
                if ((perDY >= 0.0f && y >= defPos.y + moveY) || (perDY < 0.0f && y <= defPos.y + moveY))
                {
                    endY = true;    
                }
                //��� �̵�
                transform.Translate(new Vector3(perDX, perDY, defPos.z));
            }

            if (endX && endY)
            {
                //�̵� ����
                if (isReverse)
                {
                    //��ġ�� ��߳��°��� �����ϱ� ���� ���ư��� ���� �ʱ� ��ġ�� ����
                    transform.position = defPos;
                }
                isReverse = !isReverse; //���� ����
                isCanMove = false;      //�̵� ���� ���� false
                if (isMoveWhenOn == false)  //�÷��̾ ������ ������� �������� ����
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

    //���� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //�����Ѱ��� �÷��̾��� �̵� ����� �ڽ����� �����
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                //�÷��̾ �ö����� �� ������
                isCanMove = true;   
            }
        }
    }
    //���� ����
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //�����Ѱ��� �÷��̾��� �̵� ����� �ڽĿ��� ���ܽ�Ŵ
            collision.transform.SetParent(null);
        }
    }
}
