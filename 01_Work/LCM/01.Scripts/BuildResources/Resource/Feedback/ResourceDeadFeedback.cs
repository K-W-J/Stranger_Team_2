using UnityEngine;

namespace _01_Work.LCM._01.Scripts.BuildResources.Resource.Feedback
{
    public class ResourceDeadFeedback : global::Feedback
    {
        [SerializeField] private ParticleSystem deadParticle;
        public override void PlayFeedback()
        {
            Instantiate(deadParticle, transform.position + new Vector3(0, 0.2f, 0), deadParticle.transform.rotation);
        }
    }
}
