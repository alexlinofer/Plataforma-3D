using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public float duration = 5f;

        public string compareTag = "Player";


        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            Debug.Log("cloth collected");


            var setup = ClothManager.Instance.GetSetupByType(clothType);

            Player.Instance.ChangeTexture(setup, duration);

            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}
