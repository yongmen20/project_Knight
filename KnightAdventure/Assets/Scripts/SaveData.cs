using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   //JSON 형태(텍스트)의 데이터 저장
public class SaveData 
{
    public int arrangeID = 0;   //배치 ID
    public string objTag = "";  //배치된 오브젝트의 태그
}

[System.Serializable] 
public class SaveDataList
{
    public SaveData[] saveDatas;    //데이터를 SaveData배열에 저장
}