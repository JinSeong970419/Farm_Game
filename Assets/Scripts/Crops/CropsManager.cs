using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsManager : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap cropTilemap;

    // ���۹� ����
    public Dictionary<Vector3Int, Crop> crops;

    public Dictionary<Vector3Int, Crop> corns;
    public Crop corn;

    private void Start()
    {
        crops = new Dictionary<Vector3Int, Crop>();
        corns = new Dictionary<Vector3Int, Crop>();
    }

    private void Update()
    {
        // ��� ���� �۹� ��ġ �� ����
        foreach(var crop in crops.Values)
            Grow(crop);
    }

    // �۹� �ɱ�
    public void SeedCrop(Vector3Int position, string name)
    {
        Crop cropSeeded;
        // Test ���
        if (name == "corn")
        {
            cropSeeded = Instantiate(corn); //�������� �����ϴ�
            cropSeeded.position = position; //��ġ ����
            cropSeeded.state = cropSeeded.state1; // ���� �ʱ�ȭ
            cropSeeded.timeRemaining = 10; //����ð� �Ҵ�
            
            // �۹� ��ġ �� ���� �߰�
            crops.Add(position, cropSeeded);
            corns.Add(position, cropSeeded);
            cropTilemap.SetTile(cropSeeded.position, cropSeeded.state); // Ÿ���� ���۹��� ����
        }
    }

    // �۹� ���ֱ�
    public void Water(Vector3Int position)
    {
        crops[position].timerIsRunning = true; // �Ĺ� �ڶ��
        //groundTilemap.SetTile(position, watered); // Ÿ�� ������ Ÿ���� ���鿡 �ٲٱ�
        //Debug.Log(crops[position].timeRemaining);
    }

    // �۹� ����
    void Grow(Crop crop)
    {
        if (crop.timerIsRunning) //���� ���޵� ��� - Ÿ�̸Ӱ� ���� ���Դϴ�.
        {
            if (crop.timeRemaining > 0)
            {
                crop.timeRemaining -= Time.deltaTime; //counting down
                //Debug.Log(DisplayTime(crop.timeRemaining));
            }
            else
            {
                Debug.Log("���� �Ϸ�");
                // �밡��.... ���� ����
                if (crop.state == crop.state0)
                    crop.state = crop.state3;
                else if (crop.state == crop.state1)
                    crop.state = crop.state2;
                else if (crop.state == crop.state2)
                    crop.state = crop.state3;
                else if (crop.state == crop.state3)
                    crop.state = crop.state4;

                // ���۹� ���� ����
                cropTilemap.SetTile(crop.position, crop.state);

                // ���۹� ���� ����
                crop.timerIsRunning = false;

                // ���۹��� ��� �������� �ʾҴٸ� ���� Ÿ�� �ʱ�ȭ
                if (crop.state != crop.state4)
                {
                    crop.timeRemaining = 2; // �׽�Ʈ
                }
                else
                {
                    Debug.Log("���� ���� - ��Ȯ");
                }
            }
        }
    }

    // �Ĺ� Ÿ�̸�_Debug��
    string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
