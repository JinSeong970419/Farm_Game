using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Events/LoadSceneSystem")]
public class LoadScene : ScriptableObject
{
    [SerializeField] private GameSceneSO _LoadTolocation;

    // Load
    [SerializeField] private LoadEventChannelSO _loadLocation;
    private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;

    private void OnEnable()
    {
        _loadLocation.OnLoadRequest += LoadLocation;
    }

    void OnDisable()
    {
        _loadLocation.OnLoadRequest -= LoadLocation;
    }

    private void LoadLocation(GameSceneSO LoadTolocation, bool showLoadingScreen, bool fadeScreen)
    {
        _loadingOperationHandle = LoadTolocation.sceneReference.LoadSceneAsync(LoadSceneMode.Single, true, 0);
        _loadingOperationHandle.Completed += OnNewSceneLoad;

    }

    void OnNewSceneLoad(AsyncOperationHandle<SceneInstance> obj)
    {
        Scene s = obj.Result.Scene;
        SceneManager.SetActiveScene(s);
    }
}
