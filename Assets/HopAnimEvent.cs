using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopAnimEvent : MonoBehaviour
{
    Movement player;
    private void Awake()
    {
        player = GetComponentInParent<Movement>();
    }

    public void HopExitEvent()
    {
        GameManager.instance.tileManager.selctable = false;
        player.AnimTime = false;
    }
}
