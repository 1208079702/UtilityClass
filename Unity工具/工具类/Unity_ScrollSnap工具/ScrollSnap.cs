using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(ScrollRect))]
public class ScrollSnap : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    public enum Direction
    {
        Horizontal,
        Vertical,
        Hor_Ver
    }
    [Flags]
    public enum DragDirection
    {
        None = 0,
        Left = 1,
        Center = 2,
        Right = 4,
        Up = 8,
        Middle = 16,
        Down = 32,
        LeftUp = Left | Up,
        LeftMiddle = Left | Middle,
        LeftDown = Left | Down,
        CenterUp = Center | Up,
        CenterMiddle = Center | Middle,
        CenterDown = Center | Down,
        RightUp = Right | Up,
        RightMiddle = Right | Middle,
        RightDown = Right | Down,
    }

    /* 前提设置 */
    private Direction _direction;   // ScrollRect 的拖动方向
    private DragDirection _dragDirection;// 手指的拖动方向
    private ScrollRect _scrollRect;

    /* 功能1 拖拽对齐*/  // todo 目前只完成了横向拖拽
    private bool _isCanMove;
    private float _targetHorPos;
    private List<float> _posList;
    private Vector2 _vecDrag;
    private float _horThresholdValue;
    private float _verThresholdValue;
    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _direction = Direction.Horizontal;
        _dragDirection = DragDirection.None;
        _isCanMove = false;
        _vecDrag = Vector2.zero;
    }

    private void Update()
    {
        if (_isCanMove)
        {
            if (_posList == null)
            {
                throw new Exception("请先使用 Init进行设置");
            }

            switch (_direction)
            {
                case Direction.Horizontal:
                    //_scrollRect.DOHorizontalNormalizedPos(_targetHorPos, 0.3f);
                    _scrollRect.horizontalNormalizedPosition = Mathf.Lerp(_scrollRect.horizontalNormalizedPosition, _targetHorPos, Time.deltaTime * 4);
                    if (Mathf.Approximately(_targetHorPos,_scrollRect.horizontalNormalizedPosition))
                    {
                        _isCanMove = false;
                    }
                    break;
                case Direction.Vertical:
                    break;
                case Direction.Hor_Ver:
                    break;
            }
        }
    }


    public void Init(Direction dir, float thresholdValue, List<float> list)
    {
        _direction = dir;
        switch (_direction)
        {
            case Direction.Horizontal:
                _horThresholdValue = thresholdValue;
                break;
            case Direction.Vertical:
                _verThresholdValue = thresholdValue;
                break;
        }
        _posList = list;
    }

    public void Init(float horthresholdValue, float verthresholdValue, List<float> list)
    {
        _direction = Direction.Hor_Ver;
        _horThresholdValue = horthresholdValue;
        _verThresholdValue = verthresholdValue;
        _posList = list;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isCanMove = false;
        _dragDirection = DragDirection.None;
        _vecDrag = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 判断方向
        _vecDrag += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 拖拽方向
        _dragDirection = JudgeDragDirection(_vecDrag);
        // 目标下标
        int finalIndex = JudgeTargetIndex(_posList, _dragDirection);
        if (finalIndex != -1)
        {
            _isCanMove = true;
            _targetHorPos = _posList[finalIndex];
        }
        else
        {
            _isCanMove = false;
        }
    }
    // 判断拖拽方向
    private DragDirection JudgeDragDirection(Vector2 dragDir)
    {
        DragDirection result = DragDirection.None;
        switch (_direction)
        {
            // 横向判断
            case Direction.Horizontal:
                if (Mathf.Abs(dragDir.x) >= _horThresholdValue)
                {
                    if (dragDir.x >= 0)
                        result |= DragDirection.Right;
                    else
                        result |= DragDirection.Left;
                }
                else
                    result |= DragDirection.Center;
                break;
            // 纵向判断
            case Direction.Vertical:
                if (Mathf.Abs(dragDir.y) >= _verThresholdValue)
                {
                    if (dragDir.y >= 0)
                        result |= DragDirection.Up;
                    else
                        result |= DragDirection.Down;
                }
                else
                    result |= DragDirection.Middle;
                break;
            case Direction.Hor_Ver:
                if (Mathf.Abs(dragDir.x) >= _horThresholdValue)
                {
                    if (dragDir.x >= 0)
                        result |= DragDirection.Right;
                    else
                        result |= DragDirection.Left;
                }
                else
                    result |= DragDirection.Center;
                if (Mathf.Abs(dragDir.y) >= _verThresholdValue)
                {
                    if (dragDir.y >= 0)
                        result |= DragDirection.Up;
                    else
                        result |= DragDirection.Down;
                }
                else
                    result |= DragDirection.Middle;
                break;
        }
        return result;
    }
    // 判断目标点
    private int JudgeTargetIndex(List<float> list, DragDirection currentDir)
    {
        int finalIndex = -1;
        if (list.Count == 0)
        {
            finalIndex = -1;
            return finalIndex;
        }
        else if (list.Count == 1)
        {
            finalIndex = 0;
            return finalIndex;
        }
        else
        {
            // 判断最近的点
            int nestedIndex = 0;
            float offMin = Mathf.Abs(list[nestedIndex] - _scrollRect.horizontalNormalizedPosition);
            for (int i = 0; i < list.Count; i++)
            {
                float tempoff = Mathf.Abs(list[i] - _scrollRect.horizontalNormalizedPosition);
                if (tempoff < offMin)
                {
                    offMin = tempoff;
                    nestedIndex = i;
                }
            }
            // 判断第二个近点
            int scendNestIndex = (int)Mathf.Repeat(nestedIndex + 1, list.Count); // 选择不同于第一个点的点
            offMin = Mathf.Abs(list[scendNestIndex] - _scrollRect.horizontalNormalizedPosition);
            for (int i = 0; i < list.Count; i++)
            {
                if (i == nestedIndex)
                    continue;
                float tempoff = Mathf.Abs(list[i] - _scrollRect.horizontalNormalizedPosition);
                if (tempoff < offMin)
                {
                    offMin = tempoff;
                    scendNestIndex = i;
                }
            }
            switch (currentDir) //以后的其他方向可以增加
            {
                case DragDirection.Left:
                    finalIndex = scendNestIndex > nestedIndex ? scendNestIndex : nestedIndex; //向左选择大下标
                    break;
                case DragDirection.Center:
                    finalIndex = nestedIndex;//中间选择最近的下标
                    break;
                case DragDirection.Right:
                    finalIndex = scendNestIndex < nestedIndex ? scendNestIndex : nestedIndex;//向右选择小下标
                    break;
            }
            return finalIndex;
        }
    }

}
