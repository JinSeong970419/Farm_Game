using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float hp;
    [SerializeField] private float maxHp = 100f;

    public float HP
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
            onHealthChange?.Invoke();
            if(hp == 0)
            {
                Debug.Log("사망 이벤트");
            }
        }
    }

    public float MaxHP { get => maxHp; }

    public HealthDelegate onHealthChange { get; set; }

    private void Awake()
    {
        hp = maxHp;
    }
}
