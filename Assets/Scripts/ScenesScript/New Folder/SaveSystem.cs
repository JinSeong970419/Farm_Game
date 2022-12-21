using Newtonsoft.Json;
using System.IO;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Events/SaveSystem")]
public class SaveSystem : ScriptableObject
{
    [SerializeField] private SaveEventChannelSO _saveLoad;

    public PlayerData nowPlayer = new PlayerData();
    [SerializeField] private InventoryObject _InventoryObj;
    public string path;
    public int nowSlot;

    private void OnEnable()
    {
        _saveLoad.OnEventRaised += SaveTest;
    }

    void OnDisable()
    {
        _saveLoad.OnEventRaised -= SaveTest;
    }

    public void SaveTest(int number, int i = -1)
    {
        switch (number)
        {
            case 0:
                LoadData(i);
                break;
            case 1:
                SaveData(i);
                break;
            case 2:
                DataClear();
                break;
        }
    }

    private void SaveData(int i)
    {
        nowPlayer.inventory = _InventoryObj.Container;
        FileStream stream = new FileStream(path + "/slot" + i.ToString() + ".json", FileMode.Create);
        string jsonData = JsonConvert.SerializeObject(nowPlayer);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        stream.Write(data, 0, data.Length);
        stream.Close();
    }

    private void LoadData(int i)
    {
        _InventoryObj.Container.Clear();
        FileStream stream = new FileStream(path + "/slot" + i.ToString() + ".json", FileMode.Open);
        byte[] data = new byte[stream.Length];
        stream.Read(data, 0, data.Length);
        stream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        nowPlayer = JsonConvert.DeserializeObject<PlayerData>(jsonData);
        _InventoryObj.Container = nowPlayer.inventory;
    }

    private void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
