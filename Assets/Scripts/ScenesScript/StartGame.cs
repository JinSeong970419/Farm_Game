using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameSceneSO _LoadTolocation;
    //[SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private bool _showLoadScreen;

    [SerializeField] private LoadEventChannelSO _loadLocation;

    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _onNewGameButton;

    //private bool _hasSaveData;

    private void Start()
    {
        _onNewGameButton.OnEventRaised += StartNewGame;
    }

    private void OnDestroy()
    {
        _onNewGameButton.OnEventRaised -= StartNewGame;
    }

    private void StartNewGame()
    {
        _loadLocation.RaiseEvent(_LoadTolocation, _showLoadScreen);
    }
}
