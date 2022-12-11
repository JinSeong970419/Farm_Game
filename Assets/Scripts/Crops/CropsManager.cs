using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsManager : MonoBehaviour
{
    [SerializeField] private Tilemap cropTilemap;
    [SerializeField] private GameObject Complet;

    // 농작물 정보
    public Dictionary<Vector3Int, Crop> crops;

    private void Start()
    {
        crops = new Dictionary<Vector3Int, Crop>();
    }

    // 작물 심기
    public void SeedCrop(Vector3Int position, Crop crop)
    {
        Crop cropSeeded;
        cropSeeded = Instantiate(crop); // 씨앗 정보를 복제
        cropSeeded.position = position; // 위치 설정
        cropSeeded.stateNow = cropSeeded.state[0]; // 상태 초기화
        cropSeeded.stateIndex = 0; // 상태 초기화
            
        // 작물 위치 및 정보 추가
        crops.Add(position, cropSeeded);
        cropTilemap.SetTile(cropSeeded.position, cropSeeded.stateNow); // 타일을 농작물로 변경
    }

    // 작물 물주기
    public void Water(Vector3Int position)
    {
        crops[position].timerIsRunning = true; // 식물을 자라게 쓸모없는 작업
        GameManager.instance.tileManager.SetInteracted(position, TileName.Wetground); // 타일을 젖은 지면으로 바꾸기
        StartCoroutine(WaitForIt(crops[position]));
    }

    // 작물 성장
    IEnumerator WaitForIt(Crop crop)
    {
        yield return new WaitForSeconds(crop.timeRemaining);

        // 농작물 성장 종료
        crop.timerIsRunning = false;
        crop.timeRemaining = 2;
        GameManager.instance.tileManager.SetInteracted(crop.position, TileName.Summer_Plowed);

        crop.stateIndex = (crop.stateIndex + 1) % crop.state.Length;
        crop.stateNow = crop.state[crop.stateIndex];

        // 작물 변경
        cropTilemap.SetTile(crop.position, crop.stateNow);

        // 완료 표시 테스트 중
        //if(crop.stateIndex == crop.state.Length - 1)
        //{
        //    Vector3 positions = crop.position;
        //    positions += Vector3.one;
        //    Instantiate(Complet, positions, Quaternion.identity);
        //}
    }

    // 작물 수확
    public void Harvest(Vector3Int position)
    {
        // 아이템 인벤토리 추가 넣기
        GameObject obj = Instantiate(Complet, position, Quaternion.identity);
        obj.AddComponent<BaesItem>();
        BaesItem test = obj.GetComponent<BaesItem>();
        test.item = crops[position].itemData;
        //crops[position].itemData.CreateItem();

        GameManager.instance.tileManager.SetCropsTile(crops[position].position, null);
        crops.Remove(position);
    }
}
