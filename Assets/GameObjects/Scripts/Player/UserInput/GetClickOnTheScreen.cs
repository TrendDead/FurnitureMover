using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FM.CoreGameplay
{
    /// <summary>
    /// �������� ���������������� ������� �� ������
    /// </summary>
    public class GetClickOnTheScreen : MonoBehaviour, IPointerClickHandler
    {
        public event Action EventClick = delegate { };

        public void OnPointerClick(PointerEventData eventData)
        {
            EventClick?.Invoke();
        }
    }
}
