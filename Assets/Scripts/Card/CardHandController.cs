using System.Collections;
using System.Collections.Generic;
using Snap.Core;
using UnityEngine;

namespace Snap.Card
{
    [RequireComponent(typeof(Deck))]
    [RequireComponent(typeof(CircularLayout))]
    public class CardHandController : MonoBehaviour
    {
        private Deck _deck;
        private List<Card> _cardsInHand = new();
        private CircularLayout _circularLayout;

        private void Awake()
        {
            _deck = GetComponent<Deck>();
            _circularLayout = GetComponent<CircularLayout>();
        }
        
        private void Start()
        {
            DrawStartingHand();
        }

        private void DrawStartingHand()
        {
            for (int i = 0; i < 7; i++)
            {
                var cardID = _deck.Draw();
                if (!cardID.HasValue) { continue; }

                Card card = CardFactory.Instance.GenerateCard(cardID.Value, transform);
                Debug.Log("Instantiated card: " + cardID.Value + ", " + card.name);
                _cardsInHand.Add(card);
            }
            _circularLayout.UpdateChildPositions();
            foreach (Card card in _cardsInHand)
            {
                card.SetOrigin(transform.position);
            }
        }
    }
}
