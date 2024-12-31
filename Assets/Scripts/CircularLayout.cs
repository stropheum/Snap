using UnityEngine;
using Math = Snap.Core.Math;

namespace Snap
{
    public class CircularLayout : MonoBehaviour
    {
        [SerializeField] [Range(0f, 1024f)] private float _radius;
        [SerializeField] [Range(0f, 360f)] private float _groupingAngle;
        [SerializeField] [Range(0f, 360f)] private float _offsetRotationFromOrigin;
        [SerializeField] private float _zOffsetPerIndex = 0.2f;
        private Vector3 _directionTowardsReferenceCircle;

        private void OnDrawGizmosSelected()
        {
            Vector3 midVector = GetDirectionByPercent(0.5f);
            Gizmos.color = Color.green;
            GizmoDrawDirectionFromReferenceCircle(midVector);
            
            Vector3 minVector = GetDirectionByPercent(0f);
            Gizmos.color = Color.cyan;
            GizmoDrawDirectionFromReferenceCircle(minVector);
            
            Vector3 maxVector = GetDirectionByPercent(1f);
            Gizmos.color = Color.red;
            GizmoDrawDirectionFromReferenceCircle(maxVector);

            GizmoDrawChildReferences();
        }

        private void OnValidate()
        {
            UpdateChildPositions();
        }

        private void UpdateChildPositions()
        {
            int childCount = transform.childCount;
            for (int i = 0; i < transform.childCount; i++)
            {
                float percent = i / (float)(childCount - 1); 
                Transform child = transform.GetChild(i);
                float radians = MapToRadByGroupingAngle(percent) + (_offsetRotationFromOrigin * Mathf.Deg2Rad);
                var unitVector = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);
                child.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -90f + radians * Mathf.Rad2Deg));
                float mid = MapToRadByGroupingAngle(0.5f) + (_offsetRotationFromOrigin * Mathf.Deg2Rad);
                _directionTowardsReferenceCircle = new Vector3(Mathf.Cos(mid), Mathf.Sin(mid), 0).normalized;

                Vector3 newPosition = (unitVector * _radius) - (_directionTowardsReferenceCircle * _radius) + new Vector3(0f, 0f, _zOffsetPerIndex * i);
                child.localPosition = newPosition;
            }
        }

        private float MapToRadByGroupingAngle(float percent)
        {
            float maxRad = _groupingAngle * (Mathf.Deg2Rad * 0.5f);
            float minRad = -_groupingAngle * (Mathf.Deg2Rad * 0.5f);
            return Math.Map(1f - percent, 0f, 1f, minRad, maxRad);
        }

        private Vector3 GetDirectionByPercent(float percent)
        {
            float mid = MapToRadByGroupingAngle(percent) + (_offsetRotationFromOrigin * Mathf.Deg2Rad);
            return new Vector3(Mathf.Cos(mid), Mathf.Sin(mid), 0);
        }

        private void GizmoDrawDirectionFromReferenceCircle(Vector3 direction)
        {
            Gizmos.DrawLine(transform.localPosition - (_directionTowardsReferenceCircle * _radius),  
                transform.position + (direction * _radius) - (_directionTowardsReferenceCircle * _radius));
        }

        private void GizmoDrawChildReferences()
        {
            var gradient = new Gradient
            {
                colorKeys = new GradientColorKey[2]
                {
                    new(new Color32(0, 255, 255, 255), 0f),
                    new(new Color32(255, 0, 0, 255), 1f),
                },
            };

            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = transform.GetChild(i);
                Gizmos.matrix = Matrix4x4.TRS(child.position, child.rotation, Vector3.one);
                Gizmos.color = gradient.Evaluate(i / (float)childCount);
                Gizmos.DrawSphere(Vector3.zero, 0.25f);
                Gizmos.DrawWireCube(Vector3.zero, new Vector3(5.25f, 7.25f, 0.1f));
            }
        }
    }
}
