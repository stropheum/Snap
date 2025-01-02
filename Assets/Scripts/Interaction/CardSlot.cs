using UnityEngine;

namespace Snap.Interaction
{
    public class CardSlot : MonoBehaviour
    {
        public bool IsOccupied { get; set; }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.color = IsOccupied ? Color.red : Color.cyan;
            Gizmos.DrawSphere(Vector3.zero, 0.25f);
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(5.25f, 7.25f, 0.1f));
        }
    }
}