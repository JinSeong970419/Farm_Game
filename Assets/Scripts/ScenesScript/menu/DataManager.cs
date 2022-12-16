using UnityEngine;
using System.IO;

// 슬롯별로 다르게 저장

public class PlayerData
{
    // 이름, 레벨, 코인, 아이템
    public string name;
    public int level;
    public int coin;
    public int item;
}

public class DataManager : MonoBehaviour
{
    // 싱글톤
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.dataPath + "/slot" + nowSlot.ToString()+".json";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path, data);
        Debug.Log(path);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
