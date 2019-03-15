using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(ScrollRect))]
public class ScrollSnap : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    private ScrollRect _scrollRect;
    private bool _isEndDrag;
    private float _targetHorPos;
    private List<float> posList;
    private void Awake()
    {
        _isEndDrag = false;
        _scrollRect = GetComponent<ScrollRect>();
        posList = new List<float>();
    }

    private void Update()
    {
        if (_isEndDrag)
        {
            _scrollRect.horizontalNormalizedPosition = Mathf.Lerp(_scrollRect.horizontalNormalizedPosition,
                _targetHorPos, Time.deltaTime * 4);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _isEndDrag = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _isEndDrag = true;
        // 判断与哪一个点最靠近
        int index = 0;
        float offMin = Mathf.Abs(posList[index] - _scrollRect.horizontalNormalizedPosition);
        for (int i = 1; i < posList.Count; i++)
        {
            float tempoff = Mathf.Abs(posList[i] - _scrollRect.horizontalNormalizedPosition);
            if (tempoff < offMin)
            {
                offMin = tempoff;
                index = i;
            }
        }
        _targetHorPos = posList[index];
    }

    public void SetPos(List<float> list)
    {
        _isEndDrag = false;
        posList = list;
    }

}
