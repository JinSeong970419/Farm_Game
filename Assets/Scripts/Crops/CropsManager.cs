using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsManager : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap cropTilemap;

    // ���۹� ����
    public Crop corn;

    // ���Ѻ� �۹��ɱ�
    public void SeedCrop(Vector3Int position, string name)
    {
        Crop cropSeeded;
        if (name == "corn")
        {
            cropSeeded = Instantiate(corn); //�������� �����ϴ�
            cropSeeded.position = position; //��ġ ����
            cropSeeded.state = cropSeeded.state5; // ���� �ʱ�ȭ
            cropSeeded.timeRemaining = 120; //����ð� �Ҵ�

            cropTilemap.SetTile(cropSeeded.position, cropSeeded.state); // Ÿ���� ���۹��� ����
        }
    }
}
