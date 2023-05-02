using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace FM.CoreGameplay
{ 
    /// <summary>
    /// Получить вектор направление через движение пальца по экрану
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class GetVectorMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<Vector2> CreateVector = delegate { };

        [SerializeField]
        private Camera _mainCamera;

        private Vector2 _startPosition;
        private Vector2 _dragPosition;
        private Vector2 _endPosition;

        private LineRenderer _lr;

        private void OnEnable()
        {
            //TODO: Перенести lineRender в отдельный компонент
            //Временное решение
            _lr = GetComponent<LineRenderer>();
        }

        private void ResetLineRender()
        {
            _lr.positionCount = 0;
            for(int i = 0; i < _lr.positionCount; i++)
            {
                _lr.SetPosition(0, Vector3.zero);
            }
        }
        private Vector3 GetPointToWorldPoint(Vector2 pontVector)
        {
            Debug.Log(_mainCamera.ScreenToWorldPoint(new Vector3(pontVector.x, pontVector.y, _mainCamera.nearClipPlane)));
            return _mainCamera.ScreenToWorldPoint(new Vector3(pontVector.x, pontVector.y, _mainCamera.nearClipPlane));
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = eventData.position;

            _lr.positionCount = 0;
            _lr.positionCount = 2;
            _lr.SetPosition(0, GetPointToWorldPoint(_startPosition));
        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragPosition = (_startPosition - eventData.position) + _startPosition;

            _lr.SetPosition(1, GetPointToWorldPoint(_dragPosition));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _endPosition = (_startPosition - eventData.position) + _startPosition;
            _lr.SetPosition(1, GetPointToWorldPoint(_endPosition));
            CreateVector?.Invoke(GetPointToWorldPoint(_endPosition) - GetPointToWorldPoint(_startPosition));
            _lr.positionCount = 0;
        }

    }
}
