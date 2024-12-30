using UnityEngine;

namespace Snap
{
    public class PingpongCameraAngle : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _maxAngle = 45f;
        private void FixedUpdate()
        {
            float angle = Mathf.PingPong(Time.time * _speed, _maxAngle);
            transform.rotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));
        }
    }
}
