using UnityEngine;
using UnityEngine.EventSystems;

namespace FM.CoreGameplay
{
    /// <summary>
    /// Выстрел гарпуном
    /// </summary>
    public class HarpoonShot : VectorShot
    {
        private SpringJoint _springJoint;
        private bool _isNowHit; // Можно сделать списком и добавить возможность выпуска нескольких гарпунов одновременно

        private void Start()
        {
            _getVectorMove.enabled = true;
            _getClickOnTheScreen.enabled = false;
        }

        protected override void Shot(Vector2 vectorShot)
        {
            base.Shot(vectorShot);
            _bullet.Hit += MakeHarpoon;
        }

        private void MakeHarpoon(GameObject targetObject, Vector3 collisionPosition)
        {
            gameObject.AddComponent(typeof(SpringJoint));
            _springJoint = gameObject.GetComponent<SpringJoint>();

            _springJoint.connectedBody = targetObject.GetComponent<Rigidbody>();
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = targetObject.transform.InverseTransformPoint(collisionPosition);
            _springJoint.enableCollision = true;

            _getVectorMove.enabled = false;
            _getClickOnTheScreen.enabled = true;
            _isNowHit = true;
        }

        public override void Action()
        {
            if (_isNowHit)
            {
                //Debug.Log(_springJoint);
                Destroy(_springJoint);
                _isNowHit = false;
                _getVectorMove.enabled = true;
                _getClickOnTheScreen.enabled = false;
            }
        }
    }
}
