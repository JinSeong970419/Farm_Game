using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/LoadVoidEventChannel")]
public class LoadSaveChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised;
}
