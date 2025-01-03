using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Snap.Card
{
    [RequireComponent(typeof(CardDragHandler))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private AttributeObject _attributeObject;
        [SerializeField] private SpriteRenderer _bgRenderer;
        [SerializeField] private SpriteRenderer _mainRenderer;
        [SerializeField] private TextMeshPro _nameTxt;
        [SerializeField] private TextMeshPro _abilityTxt;
        [SerializeField] private TextMeshPro _energyTxt;
        [SerializeField] private TextMeshPro _powerTxt;
        private CardDragHandler _cardDragHandler;

        private void Awake()
        {
            _cardDragHandler = GetComponent<CardDragHandler>();
        }
        
        private void Start()
        {
            ApplyAttributes();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                return;
            }
            EditorApplication.delayCall += ApplyAttributes;
        }
#endif

        public void SetAttributes(AttributeObject attributeObject)
        {
            _attributeObject = attributeObject;
            ApplyAttributes();
        }

        /// <summary>
        /// Setter for the drag handler's drag origin
        /// </summary>
        /// <param name="newOrigin">The new drag origin, in world space</param>
        public void SetOrigin(Vector3 newOrigin)
        {
            _cardDragHandler.SetOrigin(newOrigin);
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