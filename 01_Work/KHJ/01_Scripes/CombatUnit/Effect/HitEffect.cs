using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _01_Work.KHJ.CombatUnit
{

    public class HitEffect : MonoBehaviour
    {
        private List<Material> _materials = new List<Material>();

        private bool _isPlayingEffect = false;
        private void Awake()
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>(true);

            foreach (Renderer renderer in renderers)
            {
                foreach (Material mat in renderer.materials)
                {
                    if (mat != null && !_materials.Contains(mat))
                    {
                        _materials.Add(mat);
                    }
                }
            }
        }

        public void DeathEffectPlay()
        {
            foreach (var mat in _materials)
            {
                mat.DOColor(Color.black, 1f);
            }
        }


        public void EffectPlay()
        {
            if (gameObject.activeInHierarchy)
                //StartCoroutine(EffectCoroutine());
                PlayEffect();
        }

        private void PlayEffect()
        {
            if (_isPlayingEffect == false)
                StartCoroutine(PlayEffectCoroutine());
        }

        private IEnumerator PlayEffectCoroutine()
        {
            _isPlayingEffect = true;
            foreach (var mat in _materials)
                mat.DOColor(Color.red, 0.3f);

            yield return new WaitForSeconds(0.3f);

            foreach (var mat in _materials)
                mat.DOColor(Color.white, 0.4f);

            yield return new WaitForSeconds(0.4f);
            _isPlayingEffect = false;

        }
    }
}
