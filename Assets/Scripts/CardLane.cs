using UnityEngine;

namespace Snap
{
    [RequireComponent(typeof(ShowOnHover))]
    public class CardLane : MonoBehaviour
    {
        private const int CardSlotCount = 4;
        
        [SerializeField] private CardSlot[] _cardSlots;
        private ShowOnHover _showOnHover;

        private void Awake()
        {
            Debug.Assert(_cardSlots is { Length: CardSlotCount }, 
                "Card lanes must have" + CardSlotCount + " child card slots");
            _showOnHover = GetComponent<ShowOnHover>();
        }

        private void Start()
        {
            _showOnHover.HoverStateChanged += ShowOnHoverOnHoverStateChanged;
        }

        private void OnDestroy()
        {
            _showOnHover.HoverStateChanged -= ShowOnHoverOnHoverStateChanged;
        }

        private void ShowOnHoverOnHoverStateChanged(bool isHovering)
        {
            
            for (int i = 0; i < _cardSlots.Length; i++)
            {
                CardSlot frontCard = _cardSlots[i];
                CardSlot backCard = _cardSlots[_cardSlots.Length - 1 - i];
                if (isHovering && !frontCard.IsOccupied)
                {
                    frontCard.IsOccupied = true;
                    return;
                }
                else if (!isHovering & backCard.IsOccupied)
                {
                    backCard.IsOccupied = false;
                    return;
                }
            }
        }
    }
}
