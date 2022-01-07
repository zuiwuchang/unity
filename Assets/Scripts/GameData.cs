using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.king011;
public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
