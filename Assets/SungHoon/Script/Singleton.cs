using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
<<<<<<< HEAD
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
	protected void Initialized()
	{
		if (_inst != null && _inst !=this)
		{
			Destroy(_inst.gameObject);
			_inst = this as T;
		}
		DontDestroyOnLoad(transform.root.gameObject);
	}
=======
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
        if (Inst != null && Inst != this) //�����ϴ� �ν��Ͻ��� �ڱ��ڽ��� �ƴϸ� ���� ������� �Ŵ� 
        {
            Destroy(_inst.gameObject);
            _inst = this as T;
        }
        DontDestroyOnLoad(gameObject);
    }
>>>>>>> a1bf179d72bdacba5fcc1e7841ae693c76374262
}