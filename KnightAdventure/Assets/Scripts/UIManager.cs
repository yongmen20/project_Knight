using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //씬 기능 사용
using UnityEngine.UI;   //UI기능 사용

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

    public string retrySceneName = "";  //재시작 하는 씬의 이름

    // Start is called before the first frame update
    void Start()
    {
        UpdateHP();
        //이미지 숨기기
        Invoke("InactiveImage", 1.5f);
        resetButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHP();
    }

    //HP정보 가져오기
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
                    if (hp <= 0) //플레이어 사망시
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

    //재시작 버튼
    public void Retry()
    {
        PlayerPrefs.SetInt("PlayerHP", 3);
        SceneManager.LoadScene(retrySceneName);
    }

    //이미지 숨기기
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
