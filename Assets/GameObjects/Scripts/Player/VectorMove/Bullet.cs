using System;
using System.Collections;
using UnityEngine;

namespace FM.CoreGameplay
{
    /// <summary>
    /// Компонент пули/наконечника гарпуна
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        public Action<GameObject, Vector3> Hit = delegate { };

        /// <summary>
        /// Время жизни гарпуна
        /// </summary>
        public float LifeTime 
        { 
            set 
            {
                StartCoroutine(DeletionOverTime(value));
            }
        }

        private void OnDestroy()
        {
            //Hit = delegate { }; //Вместо отписки
        }

        private IEnumerator DeletionOverTime(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log("Collision");

            if(collision.gameObject.tag == "HarpoonTarget")
            {
           // Debug.Log("Try Tag");
                Hit?.Invoke(collision.gameObject, collision.contacts[0].point);
                Destroy(gameObject);
            }
        }

        private void OnTriggerExit(Collider other) //если спавн происходит внутри колайдера
        {
            if(other.tag == "Player")
            {
                //Debug.Log("Player");
                GetComponent<Collider>().isTrigger = false;
            }
        }
    }
}
