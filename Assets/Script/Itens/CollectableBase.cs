using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Itens
{
    public class CollectableBase : MonoBehaviour
    {
        public ItemType itemType;

        public string compareTag = "Player";
        public ParticleSystem particleSystem;
        public float timeToHide = 3f;
        public GameObject graphicItem;

        public Collider collider;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            //if (particleSystem != null) particleSystem.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            if (collider != null) collider.enabled = false;
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideObject", timeToHide);
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (particleSystem != null) particleSystem.Play();
            if (audioSource != null) audioSource.Play();

            CollectableManager.Instance.AddByType(itemType);
        }
    }
}


