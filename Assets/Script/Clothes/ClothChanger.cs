using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public List<SkinnedMeshRenderer> meshes;

        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        private List<Texture2D> _defaultTextures = new List<Texture2D>();

        private void Awake()
        {
            _defaultTextures.Clear();
            foreach (var mesh in meshes)
            {
                if (mesh != null && mesh.sharedMaterials.Length > 0)
                {
                    _defaultTextures.Add((Texture2D)mesh.sharedMaterials[0].GetTexture(shaderIdName));
                }
                else
                {
                    _defaultTextures.Add(null);
                }
            }
        }

        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            foreach (var mesh in meshes)
            {
                if (mesh != null && mesh.materials.Length > 0)
                {
                    mesh.materials[0].SetTexture(shaderIdName, texture);
                }
            }
        }

        public ClothSetup CurrentClothSetup { get; private set; }

        public void ChangeTexture(ClothSetup setup)
        {
            CurrentClothSetup = setup;
            foreach (var mesh in meshes)
            {
                if (mesh != null && mesh.materials.Length > 0)
                {
                    mesh.materials[0].SetTexture(shaderIdName, setup.texture);
                }
            }
        }

        public void ResetTexture()
        {
            CurrentClothSetup = null;
            for (int i = 0; i < meshes.Count; i++)
            {
                var mesh = meshes[i];
                if (mesh != null && mesh.materials.Length > 0 && i < _defaultTextures.Count)
                {
                    mesh.materials[0].SetTexture(shaderIdName, _defaultTextures[i]);
                }
            }
        }
    }
}
