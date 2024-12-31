using UnityEngine;

namespace Snap
{
    public class PerAxisRotation : MonoBehaviour
    {
        [SerializeField] private Vector3 _axisRotationSpeed;
       
        private void FixedUpdate()
        {
            Vector3 newRotation = transform.rotation.eulerAngles + (_axisRotationSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(newRotation);
        }
    }
}
