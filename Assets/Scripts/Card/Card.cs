using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Snap.Card
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private AttributeObject _attributeObject;
        [SerializeField] private SpriteRenderer _bgRenderer;
        [SerializeField] private SpriteRenderer _mainRenderer;
        [SerializeField] private TextMeshPro _nameTxt;
        [SerializeField] private TextMeshPro _abilityTxt;
        [SerializeField] private TextMeshPro _energyTxt;
        [SerializeField] private TextMeshPro _powerTxt;
        private bool _isDirty;

        private void Start()
        {
            ApplyAttributes();
        }

        private void OnValidate()
        {
            #if UNITY_EDITOR
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                return;
            }
            EditorApplication.delayCall += ApplyAttributes;
            #endif
        }

        private void Update()
        {
            CheckIsDirty();
        }

        public void SetAttributes(AttributeObject attributeObject)
        {
            _attributeObject = attributeObject;
            ApplyAttributes();
        }

        private void CheckIsDirty()
        {
            if (!_isDirty) { return; }

            ApplyAttributes();
            _isDirty = false;
        }

        private void ApplyAttributes()
        {
            if (gameObject == null) { return; }
            bool attributesExist = _attributeObject != null;
            SetAttributesEnabled(attributesExist);
            if (!attributesExist)
            {
                return;
            }

            _bgRenderer.sprite = _attributeObject.BgSprite;
            _mainRenderer.sprite = _attributeObject.MainSprite;
            _nameTxt.text = _attributeObject.Name;
            _abilityTxt.text = _attributeObject.AbilityText;
            _energyTxt.text = _attributeObject.Energy.ToString();
            _powerTxt.text = _attributeObject.Power.ToString();
        }

        private void SetAttributesEnabled(bool isEnabled)
        {
            _bgRenderer.enabled = isEnabled;
            _mainRenderer.enabled = isEnabled;
            _nameTxt.enabled = isEnabled;
            _abilityTxt.enabled = isEnabled;
            _energyTxt.enabled = isEnabled;
            _powerTxt.enabled = isEnabled;
        }
    }
}