using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Crop")]
public class Crop : ScriptableObject
{
    public string Name;
    // ���۹� ���� ����
    public TileBase[] state = new TileBase[5];

    //public Tilemap cropTilemap;

    public TileBase stateNow; // ���۹� ����
    public int stateIndex; // ���۹� ����
    public Vector3Int position;

    public float timeRemaining = 10;  // ���۹� ���� �ð�
    public bool timerIsRunning = false; // ���� ����
    public Text timeText;
}
