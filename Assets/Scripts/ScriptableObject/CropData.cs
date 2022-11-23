using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/CropData")]
public class CropData : ScriptableObject
{
    public List<TileBase> tiles;

    public bool noPlant; // 식물

    /*public bool withCorn;

    public bool withParsley;

    public bool withPotato;

    public bool withStrawberry;

    public bool withTomato;*/

    public bool planted; // 심은 작물

    public bool collectible; // 수집

    public bool collectibleCorn;       // 옥수수
    public bool collectibleParsley;    // 파슬리
    public bool collectiblePotato;     // 감자
    public bool collectibleStrawberry; // 딸기
    public bool collectibleTomato;     // 토마토
}
