using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   //JSON ����(�ؽ�Ʈ)�� ������ ����
public class SaveData 
{
    public int arrangeID = 0;   //��ġ ID
    public string objTag = "";  //��ġ�� ������Ʈ�� �±�
}

[System.Serializable] 
public class SaveDataList
{
    public SaveData[] saveDatas;    //�����͸� SaveData�迭�� ����
}