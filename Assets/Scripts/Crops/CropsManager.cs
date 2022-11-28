using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsManager : MonoBehaviour
{
    // [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap cropTilemap;

    // ���۹� ����
    public Dictionary<Vector3Int, Crop> crops;

    public Dictionary<Vector3Int, Crop> corns;
    public Crop corn;

    // Ÿ�� ����
    [SerializeField] private Tile wetground;
    [SerializeField] private Tile dryground;
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
    // ������ �Ŵ��� ����ϱ�
    public void SeedCrop(Vector3Int position, Cropseed name)
    {
        Crop cropSeeded;
        // Test ���
        if (name == Cropseed.corn)
        {
            cropSeeded = Instantiate(corn); //�������� �����ϴ�
            cropSeeded.position = position; //��ġ ����
            cropSeeded.stateNow = cropSeeded.state[0]; // ���� �ʱ�ȭ
            cropSeeded.stateIndex = 0; // ���� �ʱ�ȭ
            cropSeeded.timeRemaining = 2; //����ð� �Ҵ� - ���� ����
            
            // �۹� ��ġ �� ���� �߰�
            crops.Add(position, cropSeeded);
            corns.Add(position, cropSeeded);
            cropTilemap.SetTile(cropSeeded.position, cropSeeded.stateNow); // Ÿ���� ���۹��� ����
        }
    }

    // �۹� ���ֱ�
    public void Water(Vector3Int position)
    {
        if (crops[position].stateIndex > crops[position].state.Length - 2) { return; } // �۹��� �� �ڶ��ٸ� �������� ����

        crops[position].timerIsRunning = true; // �Ĺ��� �ڶ��
        GameManager.instance.tileManager.SetInteracted(position, wetground); // Ÿ���� ���� �������� �ٲٱ�
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
                // ���۹� ���� ����
                crop.timerIsRunning = false;
                crop.timeRemaining = 2;
                GameManager.instance.tileManager.SetInteracted(crop.position, dryground);

                crop.stateIndex = (crop.stateIndex + 1) % crop.state.Length;
                crop.stateNow = crop.state[crop.stateIndex];
                //Debug.Log($"���� �Ϸ� : {crop.stateNow}");

                cropTilemap.SetTile(crop.position, crop.stateNow);
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
