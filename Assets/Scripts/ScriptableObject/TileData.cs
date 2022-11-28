using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/TileData")]
// Ÿ�� ������
public class TileData : ScriptableObject
{
    public List<TileBase> tiles; // Ÿ�� Base
    //public TileBase tiles; // Ÿ�� Base

    public bool plowable; // ���۰��� Ÿ��

    public bool ableToMow; // ���� ���� Ÿ��

    public bool ableToSeed; // ���ѽɱ� ���� Ÿ��

    public bool waterable; // ���� �� �� �ִ� Ÿ��

    //public bool collectible; // ����ǰ ���� Ÿ��
}