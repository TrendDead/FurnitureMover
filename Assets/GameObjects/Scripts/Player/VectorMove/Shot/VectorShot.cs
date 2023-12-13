using UnityEngine;

namespace FM.CoreGameplay
{
    /// <summary>
    /// Базовый класс выстрела "пули" в направлении вектора
    /// </summary>
    public abstract class VectorShot : MonoBehaviour
    {
        public GameObject LastTarget => _target;

        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _speedShot;
        [SerializeField] private float _bulletLifeTime = 0.5f;
        [SerializeField] private Bullet _bulletPrefab;
        
        [SerializeField] protected GetVectorMove _getVectorMove;
        [SerializeField] protected GetClickOnTheScreen _getClickOnTheScreen;

        protected Bullet _bullet;
        protected GameObject _target;

        private void Awake()
        {
            _getVectorMove.CreateVector += Shot;
            _getClickOnTheScreen.EventClick += Action;
        }

        private void OnDestroy()
        {
            _getVectorMove.CreateVector -= Shot;
            _getClickOnTheScreen.EventClick -= Action;
        }

        public abstract void Action();

        protected virtual void Shot(Vector2 vectorShot)
        {
            _bullet = Instantiate(_bulletPrefab, new Vector3(_shootPoint.position.x, _shootPoint.position.y, _shootPoint.position.z), Quaternion.identity);
            _bullet.GetComponent<Rigidbody>().AddForce(vectorShot.normalized * _speedShot, ForceMode.VelocityChange);
            _bullet.LifeTime = _bulletLifeTime;
        }
    }
}
