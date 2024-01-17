using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //�� ��� ���
using UnityEngine.UI;   //UI��� ���

public class UIManager : MonoBehaviour
{
    int hp = 0;
    public GameObject hpImage;
    public Sprite life3Image;
    public Sprite life2Image;
    public Sprite life1Image;
    public Sprite life0Image;
    public GameObject mainImage;
    public GameObject resetButton;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;

    public string retrySceneName = "";  //����� �ϴ� ���� �̸�

    // Start is called before the first frame update
    void Start()
    {
        UpdateHP();
        //�̹��� �����
        Invoke("InactiveImage", 1.5f);
        resetButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHP();
    }

    //HP���� ��������
    void UpdateHP()
    {
        if(PlayerController.gameState != "gameend")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                if (PlayerController.hp != hp)
                {
                    hp = PlayerController.hp;
                    if (hp <= 0) //�÷��̾� �����
                    {
                        hpImage.GetComponent<Image>().sprite = life0Image;
                        resetButton.SetActive(true);
                        mainImage.SetActive(true);
                        mainImage.GetComponent<Image>().sprite = gameOverSpr;
                        PlayerController.gameState = "gameend";
                    }
                    else if (hp == 1) hpImage.GetComponent<Image>().sprite = life1Image;
                    else if (hp == 2) hpImage.GetComponent<Image>().sprite = life2Image;
                    else hpImage.GetComponent<Image>().sprite = life3Image;
                }
            }
        }
    }

    //����� ��ư
    public void Retry()
    {
        PlayerPrefs.SetInt("PlayerHP", 3);
        SceneManager.LoadScene(retrySceneName);
    }

    //�̹��� �����
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
