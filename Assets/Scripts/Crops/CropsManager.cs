using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsManager : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap cropTilemap;

    // 농작물 정보
    public Crop corn;

    // 씨앗별 작물심기
    public void SeedCrop(Vector3Int position, string name)
    {
        Crop cropSeeded;
        if (name == "corn")
        {
            cropSeeded = Instantiate(corn); //옥수수를 복제하다
            cropSeeded.position = position; //위치 설정
            cropSeeded.state = cropSeeded.state5; // 상태 초기화
            cropSeeded.timeRemaining = 120; //성장시간 할당

            cropTilemap.SetTile(cropSeeded.position, cropSeeded.state); // 타일을 농작물로 변경
        }
    }
}
