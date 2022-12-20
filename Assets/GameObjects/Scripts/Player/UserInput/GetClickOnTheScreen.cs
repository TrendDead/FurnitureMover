using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FM.CoreGameplay
{
    /// <summary>
    /// Получить пользовательское нажатие по экрану
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
