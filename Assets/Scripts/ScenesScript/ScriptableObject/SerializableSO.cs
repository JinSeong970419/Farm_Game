using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializableSO : ScriptableObject
{
    [SerializeField, HideInInspector] private string _guid;
    public string Guid => _guid;
}
