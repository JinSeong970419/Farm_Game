using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerHealth health;
    public PlayerHealth Health
    {
        get { return health; }
    }

    public TileManager tileManager;
    public Toolbar_UI UI_Manager;
    public CropsManager cropManager;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        tileManager = GetComponent<TileManager>();
        cropManager = GetComponent<CropsManager>();

        // 임시
        health = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }
}
