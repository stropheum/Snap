using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Snap.Card
{
    public class Card : MonoBehaviour
    {
        [FormerlySerializedAs("_attributes")] [SerializeField] private AttributeObject _attributeObject;
        [SerializeField] private SpriteRenderer _bgRenderer;
        [SerializeField] private SpriteRenderer _mainRenderer;
        [SerializeField] private TextMeshPro _nameTxt;
        [SerializeField] private TextMeshPro _abilityTxt;
        [SerializeField] private TextMeshPro _energyTxt;
        [SerializeField] private TextMeshPro _powerTxt;

        private void Awake()
        {
            ApplyAttributes();
        }

        private void OnValidate()
        {
            ApplyAttributes();
        }

        public void SetAttributes(AttributeObject attributeObject)
        {
            _attributeObject = attributeObject;
            ApplyAttributes();
        }

        private void ApplyAttributes()
        {
            if (_attributeObject == null)
            {
                Debug.LogWarning("Attributes not set", gameObject);
                return;
            }
            _bgRenderer.sprite = _attributeObject.BgSprite;
            _mainRenderer.sprite = _attributeObject.MainSprite;
            _nameTxt.text = _attributeObject.Name;
            _abilityTxt.text = _attributeObject.AbilityText;
            _energyTxt.text = _attributeObject.Energy.ToString();
            _powerTxt.text = _attributeObject.Power.ToString();
        }
    }
}
