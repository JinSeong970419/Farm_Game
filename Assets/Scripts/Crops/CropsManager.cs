using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsManager : MonoBehaviour
{
    // [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap cropTilemap;

    // 농작물 정보
    public Dictionary<Vector3Int, Crop> crops;

    public Dictionary<Vector3Int, Crop> corns;
    public Crop corn;

    // 타일 정보
    [SerializeField] private Tile wetground;
    [SerializeField] private Tile dryground;
    private void Start()
    {
        crops = new Dictionary<Vector3Int, Crop>();
        corns = new Dictionary<Vector3Int, Crop>();
    }

    private void Update()
    {
        // 모든 종류 작물 서치 후 성장
        foreach(var crop in crops.Values)
            Grow(crop);
    }

    // 작물 심기
    // 아이템 매니저 사용하기
    public void SeedCrop(Vector3Int position, Cropseed name)
    {
        Crop cropSeeded;
        // Test 당근
        if (name == Cropseed.corn)
        {
            cropSeeded = Instantiate(corn); //옥수수를 복제하다
            cropSeeded.position = position; //위치 설정
            cropSeeded.stateNow = cropSeeded.state[0]; // 상태 초기화
            cropSeeded.stateIndex = 0; // 상태 초기화
            cropSeeded.timeRemaining = 2; //성장시간 할당 - 추후 변경
            
            // 작물 위치 및 정보 추가
            crops.Add(position, cropSeeded);
            corns.Add(position, cropSeeded);
            cropTilemap.SetTile(cropSeeded.position, cropSeeded.stateNow); // 타일을 농작물로 변경
        }
    }

    // 작물 물주기
    public void Water(Vector3Int position)
    {
        if (crops[position].stateIndex > crops[position].state.Length - 2) { return; } // 작물이 다 자랐다면 성장하지 않음

        crops[position].timerIsRunning = true; // 식물을 자라게
        GameManager.instance.tileManager.SetInteracted(position, wetground); // 타일을 젖은 지면으로 바꾸기
        //Debug.Log(crops[position].timeRemaining);
    }

    // 작물 성장
    void Grow(Crop crop)
    {
        if (crop.timerIsRunning) //물이 공급된 경우 - 타이머가 실행 중입니다.
        {
            if (crop.timeRemaining > 0)
            {
                crop.timeRemaining -= Time.deltaTime; //counting down
                //Debug.Log(DisplayTime(crop.timeRemaining));
            }
            else
            {
                // 농작물 성장 종료
                crop.timerIsRunning = false;
                crop.timeRemaining = 2;
                GameManager.instance.tileManager.SetInteracted(crop.position, dryground);

                crop.stateIndex = (crop.stateIndex + 1) % crop.state.Length;
                crop.stateNow = crop.state[crop.stateIndex];
                //Debug.Log($"성장 완료 : {crop.stateNow}");

                cropTilemap.SetTile(crop.position, crop.stateNow);
            }
        }
    }

    // 식물 타이머_Debug용
    string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
