using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public class EnemyInvisible : EnemyBase
    {
        public MeshRenderer meshRenderer;
        public BoxCollider boxCollider;

        public override void Start()
        {
            base.Start();
            SetAlpha(0f);
            boxCollider.enabled = false;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SetAlpha(1f); // Torna visível
                boxCollider.enabled = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SetAlpha(0f); // Torna invisível
                boxCollider.enabled = false;
            }
        }

        private void SetAlpha(float alpha)
        {
            Color newColor = meshRenderer.material.color; // Obtém a cor atual
            newColor.a = alpha; // Modifica apenas o Alpha
            meshRenderer.material.color = newColor; // Aplica a nova cor

            // Se o material usa transparência via shader
            meshRenderer.material.SetFloat("_Mode", 3); // Define como transparente no Standard Shader
        }
    }
}
