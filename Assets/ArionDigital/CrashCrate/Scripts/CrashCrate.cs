namespace ArionDigital
{
    using FM.CoreGameplay;
    using UnityEngine;

    public class CrashCrate : MonoBehaviour
    {
        [Header("Whole Create")]
        public MeshRenderer wholeCrate;
        public BoxCollider boxCollider;
        [Header("Fractured Create")]
        public GameObject fracturedCrate;
        [Header("Audio")]
        public AudioSource crashAudioClip;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                wholeCrate.enabled = false;
                boxCollider.enabled = false;
                fracturedCrate.SetActive(true);
                crashAudioClip.Play();
                var playerShoot = collision.gameObject.GetComponent<VectorShot>();
                if (playerShoot.LastTarget == this.gameObject)
                {
                    playerShoot.Action();
                }
            }
        }

        [ContextMenu("Test")]
        public void Test()
        {
            wholeCrate.enabled = false;
            boxCollider.enabled = false;
            fracturedCrate.SetActive(true);
        }
    }
}