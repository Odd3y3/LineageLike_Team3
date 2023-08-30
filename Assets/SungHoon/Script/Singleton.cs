using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _inst = null;
    public static T Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindObjectOfType<T>();
                if (_inst == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).ToString();
                    _inst = obj.AddComponent<T>();
                }
            }
            return _inst;
        }
    }

    protected void Initialize()
    {
        //새로 생긴 inst가 삭제 되도록 변경
        if (Inst != this) //참조하는 인스턴스가 자기자신이 아니면 새로 만들어진 거다 
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}