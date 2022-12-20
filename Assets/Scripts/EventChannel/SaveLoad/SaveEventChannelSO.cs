using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/saveEventChannel")]
public class SaveEventChannelSO : ScriptableObject
{
    public UnityAction<int, int> OnEventRaised;

    public void RaiseEvent(int number, int i = -1)
    {
        OnEventRaised?.Invoke(number, i);
    }
}
