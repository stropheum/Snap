using System.Collections.Generic;
using Snap.Core;
using UnityEngine;

namespace Snap.Card
{
    public class CardFactory : Singleton<CardFactory>
    {
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private AttributeObject[] _attributeObjects;
        private Dictionary<Attributes.CardID, AttributeObject> _attributeObjectMap;

        protected override void Awake()
        {
            base.Awake();
            Debug.Assert(_cardPrefab != null, nameof(_cardPrefab) + " != null", gameObject);
        }

        private void Start()
        {
            MapAttributes();
        }

        private void MapAttributes()
        {
            _attributeObjectMap = new Dictionary<Attributes.CardID, AttributeObject>();
            foreach (AttributeObject attributeObject in _attributeObjects)
                _attributeObjectMap.Add(attributeObject.ID, attributeObject);
        }

        public Card GenerateCard(Attributes.CardID id, Transform parent)
        {
            if (!_attributeObjectMap.ContainsKey(id))
            {
                Debug.LogWarning("Key not found in card attributes: " + id, gameObject);
                return null;
            }

            Card result = Instantiate(_cardPrefab, parent);
            result.SetAttributes(_attributeObjectMap[id]);
            return result;
        }
    }
}