using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemMenuUI : MonoBehaviour
{
    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rayList;

    private ItemMenuUI _beginDragSlot;
    private Transform _beginDragIconTransform;

    private Vector3 _beginDragIconPoint;
    private Vector3 _beginDragCursorPoint;
    private int _beginDragSlotSiblingIndex;

    void Start()
    {

    }


    private void Update()
    {
        _ped.position = Input.mousePosition;

    }
    private T RaycastAndGetFirstComponent<T>() where T : Component
    {
        _rayList.Clear();
        _gr.Raycast(_ped, _rayList);

        if (_rayList.Count == 0)
            return null;

        return _rayList[0].gameObject.GetComponent<T>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            _beginDragSlot = RaycastAndGetFirstComponent<ItemMenuUI>();

            if (_beginDragSlot != null && _beginDragSlot.gameObject)
            {

            }
        }
    }

    public void OnPointerDrag(PointerEventData eventData)
    {
        if (_beginDragSlot == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            _beginDragIconTransform.position = _beginDragIconPoint + (Input.mousePosition - _beginDragCursorPoint);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_beginDragSlot != null)
            {
                _beginDragIconTransform.position = _beginDragIconPoint;
                _beginDragSlot.transform.SetSiblingIndex(_beginDragSlotSiblingIndex);

                _beginDragSlot = null;
                _beginDragIconTransform = null;
            }
        }
    }
}
