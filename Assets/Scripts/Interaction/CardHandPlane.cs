using Snap.Core;
using UnityEngine;

namespace Snap.Interaction
{
    [RequireComponent(typeof(BoxCollider))]
    public class CardHandPlane : Singleton<CardHandPlane>
    {
        public Vector3 CardPlanePosition => transform.position + _collider.center;
        private BoxCollider _collider;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<BoxCollider>();
        }
    }
}
