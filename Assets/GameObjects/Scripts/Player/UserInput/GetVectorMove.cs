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
        [SerializeField]
        private VectorMoveView _vectorMoveView;

        private bool _isDrag = false;
        private Vector2 _startPosition;
        private Vector2 _dragPosition;
        private Vector2 _endPosition;

        private Vector3 GetPointToWorldPoint(Vector2 pontVector)
        {
            //Debug.Log(_mainCamera.ScreenToWorldPoint(new Vector3(pontVector.x, pontVector.y, _mainCamera.nearClipPlane)));
            return _mainCamera.ScreenToWorldPoint(new Vector3(pontVector.x, pontVector.y, _mainCamera.nearClipPlane));
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = eventData.position;

            //_vectorMoveView.LinePoints(0);
            //_vectorMoveView.ResetPos();
            _vectorMoveView.LinePoints(2);
            
            _vectorMoveView.AddPoint(0, GetPointToWorldPoint(_startPosition - new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y)));
            _isDrag = true;
        }
        
        /// <summary>
        /// Временное решение, для тестоа
        /// </summary>
        private void LateUpdate()
        {
            if (_isDrag)
            {
                _vectorMoveView.AddPoint(0, GetPointToWorldPoint(_startPosition - new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y)/* + new Vector2(transform.position.x, transform.position.y)*/));
            }
        }


        public void OnDrag(PointerEventData eventData)
        {
            _dragPosition = -eventData.position + _startPosition * 2;
            //_dragPosition = eventData.position * -1 + _startPosition * 2;

            //_vectorMoveView.AddPoint(0, GetPointToWorldPoint(_startPosition));
            //Debug.Log(GetPointToWorldPoint(eventData.position) - GetPointToWorldPoint(_startPosition));
            _vectorMoveView.AddPoint(1, GetPointToWorldPoint(_dragPosition/* + new Vector2(transform.position.x, transform.position.y)*/));
            
            //_vectorMoveView.AddPoint(1, (GetPointToWorldPoint(_dragPosition) - GetPointToWorldPoint(_startPosition)).normalized * 2);
            //_lr.SetPosition(1, GetPointToWorldPoint(_dragPosition));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _endPosition = -eventData.position + _startPosition * 2;

            //_vectorMoveView.AddPoint(1, GetPointToWorldPoint(_endPosition));
            //_lr.SetPosition(1, GetPointToWorldPoint(_endPosition));
            CreateVector?.Invoke(GetPointToWorldPoint(_endPosition) - GetPointToWorldPoint(_startPosition));
            // _vectorMoveView.LinePoints(1);
            _isDrag = false;
            _vectorMoveView.LinePoints(0);
        }

    }
}
