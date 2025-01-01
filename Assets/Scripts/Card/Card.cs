using TMPro;
using UnityEngine;

namespace Snap.Card
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Attributes _attributes;
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

        public void SetAttributes(Attributes attributes)
        {
            _attributes = attributes;
            ApplyAttributes();
        }

        private void ApplyAttributes()
        {
            if (_attributes == null)
            {
                Debug.LogWarning("Attributes not set", gameObject);
                return;
            }
            _bgRenderer.sprite = _attributes.BgSprite;
            _mainRenderer.sprite = _attributes.MainSprite;
            _nameTxt.text = _attributes.Name;
            _abilityTxt.text = _attributes.AbilityText;
            _energyTxt.text = _attributes.Energy.ToString();
            _powerTxt.text = _attributes.Power.ToString();
        }
    }
}
