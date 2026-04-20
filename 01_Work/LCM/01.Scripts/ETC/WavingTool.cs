using UnityEngine;

namespace _01_Work.LCM._01.Scripts.ETC
{
    public class WavingTool : MonoBehaviour
    {
        private float _currentZAngle;
        [SerializeField] private float rotationSpeed = 50f;
    
        private void Update()
        {
            _currentZAngle = Mathf.PingPong(Time.time * rotationSpeed, 60f) - 30f;
            transform.rotation = Quaternion.Euler(0, 0, _currentZAngle);
        }
    }
}
