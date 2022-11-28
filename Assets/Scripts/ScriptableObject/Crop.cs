using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Crop")]
public class Crop : ScriptableObject
{
    public string Name;
    // 농작물 성장 순서
    public TileBase[] state = new TileBase[5];

    //public Tilemap cropTilemap;

    public TileBase stateNow; // 농작물 상태
    public int stateIndex; // 농작물 상태
    public Vector3Int position;

    public float timeRemaining = 10;  // 농작물 성장 시간
    public bool timerIsRunning = false; // 성장 여부
    public Text timeText;
}
