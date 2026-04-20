using Unity.Cinemachine;
using UnityEngine;

namespace _01_Work.LCM._01.Scripts.Merchant
{
    public class MerchantCameraFeedback : Feedback
    {
        [SerializeField] private CinemachineCamera _camera;


        public override void PlayFeedback()
        {
            _camera.Priority = 0;
        }
    }
}
