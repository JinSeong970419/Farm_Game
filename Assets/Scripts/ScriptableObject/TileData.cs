using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/TileData")]
// 타일 데이터
public class TileData : ScriptableObject
{
    public List<TileBase> tiles; // 타일 Base
    //public TileBase tiles; // 타일 Base

    public bool plowable; // 경작가능 타일

    public bool ableToMow; // 베기 가능 타일

    public bool ableToSeed; // 씨앗심기 가능 타일

    public bool waterable; // 물을 줄 수 있는 타일

    //public bool collectible; // 수집품 관련 타일
}