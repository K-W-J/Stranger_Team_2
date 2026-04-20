using UnityEngine;

namespace _01_Work.LCM._01.Scripts.BuildResources.Resource.Feedback
{
    public class ResourceHitFeedback : global::Feedback
    {
        [SerializeField] private ParticleSystem hitParticle;


        public override void PlayFeedback()
        {
            Instantiate(hitParticle, transform.position + new Vector3(0,0.2f,0), hitParticle.transform.rotation);
        }
    }
}
