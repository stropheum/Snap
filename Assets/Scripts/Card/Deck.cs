using System.Collections.Generic;
using Snap.Core;
using UnityEngine;

namespace Snap.Card
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private Attributes.CardID[] _cardIDs = new Attributes.CardID[12];
        private IList<Attributes.CardID> _deckList = new List<Attributes.CardID>();

        private void Awake()
        {
            Debug.Assert(_cardIDs is { Length: 12 }, "Decks must have 12 cards.", gameObject);
        }

        private void Start()
        {
            ResetDeck();
        }

        private void ResetDeck()
        {
            foreach (Attributes.CardID id in _cardIDs) _deckList.Add(id);
            _deckList = _deckList.Shuffle();
        }
    }
}