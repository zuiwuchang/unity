using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.king011;
public class GameData : MonoBehaviour
{
    public delegate void OnLanguageChange();
    [Label("視圖")]
    public string view;
    OnLanguageChange a;
    public class Cat
    {
        public string name = "cat";
        public void OnLanguageChange1()
        {
            Debug.Log($"{name} changed");
        }
    }
    private void Awake()
    {
        var c0 = new Cat();
        var c1 = new Cat();
        add(c0.OnLanguageChange1);
        c1.name = "dog";
        add(c1.OnLanguageChange1);
        a();
        a -= c0.OnLanguageChange1;
        a -= c1.OnLanguageChange1;
        a -= c1.OnLanguageChange1;
        Debug.Log($"{a == null}");
    }
    private void add(OnLanguageChange b)
    {
        a += b;
    }
}
