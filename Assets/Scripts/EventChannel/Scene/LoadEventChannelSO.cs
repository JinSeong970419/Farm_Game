using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Events/Load Event Channel")]
public class LoadEventChannelSO : DescriptionBaseSO
{
    public UnityAction<GameSceneSO, bool, bool> OnLoadRequest;

    // 이동 씬
    public void RaiseEvent(GameSceneSO LoadTolocation, bool showLoadingScreen = false, bool fadeScreen = false)
    {
        if(OnLoadRequest != null)
        {
            OnLoadRequest?.Invoke(LoadTolocation, showLoadingScreen, fadeScreen);
        }
        else
        {
            Debug.LogError("Event 수신 확인");
        }
    }
}
