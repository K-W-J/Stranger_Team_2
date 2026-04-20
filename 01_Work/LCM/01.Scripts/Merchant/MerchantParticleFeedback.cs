using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _01_Work.LCM._01.Scripts.Merchant
{
    public class MerchantParticleFeedback : Feedback
    {
        [SerializeField] private Transform _merchantTransform;
        [SerializeField] private Transform _merchantVisual;
        [SerializeField] private ParticleSystem _dustParticle;
        [SerializeField] private Image _merchantImage;
        public override void PlayFeedback()
        {
            StartCoroutine(DeleteMerchant());
            Instantiate(_dustParticle, _merchantTransform.position, Quaternion.identity);
            _merchantImage.gameObject.SetActive(false);
            _merchantVisual.gameObject.SetActive(false);
        }

        private IEnumerator DeleteMerchant()
        {
            yield return new WaitForSeconds(2f);
            Destroy(_merchantTransform.gameObject);
        }
    }
}
