using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "NewLocation", menuName = "Scene Data/Location")]
public class GameSceneSO : DescriptionBaseSO
{
    public GameSceneType sceneType;
    // 런타임에 AssetBundle에서 씬(scene)을 로드하는 데 사용
    public AssetReference sceneReference;
}

public enum GameSceneType
{
    Location,
    Menu
}