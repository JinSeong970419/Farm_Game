using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsManager : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap cropTilemap;

    // 농작물 정보
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
        // 모든 종류 작물 서치 후 성장
        foreach(var crop in crops.Values)
            Grow(crop);
    }

    // 작물 심기
    public void SeedCrop(Vector3Int position, string name)
    {
        Crop cropSeeded;
        // Test 당근
        if (name == "corn")
        {
            cropSeeded = Instantiate(corn); //옥수수를 복제하다
            cropSeeded.position = position; //위치 설정
            cropSeeded.state = cropSeeded.state1; // 상태 초기화
            cropSeeded.timeRemaining = 10; //성장시간 할당
            
            // 작물 위치 및 정보 추가
            crops.Add(position, cropSeeded);
            corns.Add(position, cropSeeded);
            cropTilemap.SetTile(cropSeeded.position, cropSeeded.state); // 타일을 농작물로 변경
        }
    }

    // 작물 물주기
    public void Water(Vector3Int position)
    {
        crops[position].timerIsRunning = true; // 식물 자라게
        //groundTilemap.SetTile(position, watered); // 타일 지도의 타일을 지면에 바꾸기
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
                Debug.Log("성장 완료");
                // 노가다.... 추후 변경
                if (crop.state == crop.state0)
                    crop.state = crop.state3;
                else if (crop.state == crop.state1)
                    crop.state = crop.state2;
                else if (crop.state == crop.state2)
                    crop.state = crop.state3;
                else if (crop.state == crop.state3)
                    crop.state = crop.state4;

                // 농작물 상태 변경
                cropTilemap.SetTile(crop.position, crop.state);

                // 농작물 성장 종료
                crop.timerIsRunning = false;

                // 농작물이 모두 성장하지 않았다면 성장 타임 초기화
                if (crop.state != crop.state4)
                {
                    crop.timeRemaining = 2; // 테스트
                }
                else
                {
                    Debug.Log("성장 종료 - 수확");
                }
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
