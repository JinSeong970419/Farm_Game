using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Crop")]
public class Crop : ScriptableObject
{
    // 농작물 성장 순서
    public TileBase state0;

    public TileBase state1;

    public TileBase state2;

    public TileBase state3;

    public TileBase state4;

    //public Tilemap cropTilemap;

    public TileBase state; // 농작물 상태
    public Vector3Int position;

    public float timeRemaining = 10;  // 농작물 성장 시간
    public bool timerIsRunning = false; // 성장 여부
    public Text timeText;
    public string Name;
}
