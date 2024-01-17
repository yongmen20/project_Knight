using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    life,
}

public class ItemData : MonoBehaviour
{
    public ItemType type;
    public int count = 1;

    public int arrangeId = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(type == ItemType.life)
            {
                if(PlayerController.hp<3)
                {
                    PlayerController.hp++;
                    //HP �����ϱ�
                    PlayerPrefs.SetInt("PlayerHP", PlayerController.hp);
                }
            }

            //������ ȹ�� ����
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Rigidbody2D itemBody = GetComponent<Rigidbody2D>();
            itemBody.gravityScale = 2.5f;    //Ƣ������� ����
            itemBody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            Destroy(gameObject, 0.5f);
        }
    }
}
