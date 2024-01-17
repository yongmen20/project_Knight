using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//출입구 위치/방향
public enum ExitDirection
{
    right,
    left,
    down,
    up,
}

public class Exit : MonoBehaviour
{
    public string sceneName = "";
    public int doorNumber = 0;
    public ExitDirection direction = ExitDirection.right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
        }
    }
}
