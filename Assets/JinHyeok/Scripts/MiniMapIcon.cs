using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class MiniMapIcon : MonoBehaviour
{
    Transform _target;
    RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void SetTarget(Transform target, Color color)
    {
        _target = target;
        GetComponent<Image>().color = color;
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector3 pos = Camera.allCameras[Camera.allCameras.Length - 1].WorldToViewportPoint(_target.position) * 400f;
            _rect.anchoredPosition = pos - new Vector3(200f, 200f, 0f);
        }
    }
}
