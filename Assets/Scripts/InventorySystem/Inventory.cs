using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[60];

    // 인벤토리 저장
    public void Save(string Path)
    {
        FileStream stream = new FileStream(Application.dataPath + Path, FileMode.Create);
        string jsonData = JsonConvert.SerializeObject(Slots);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        stream.Write(data, 0, data.Length);
        stream.Close();
    }

    // 인벤토리 로드
    public void Load(string Path)
    {
        Clear();
        FileStream stream = new FileStream(Application.dataPath + Path, FileMode.Open);
        byte[] data = new byte[stream.Length];
        stream.Read(data, 0, data.Length);
        stream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        InventorySlot[] LoadSlots = JsonConvert.DeserializeObject<InventorySlot[]>(jsonData);
        // UI 초기화
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].UpdateSlot(LoadSlots[i].item, LoadSlots[i].amount);
        }
    }

    // 인베토리 비우기
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].item = new Item();
            Slots[i].amount = 0;
        }
    }
}