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
            ResetDeck();
        }

        public Attributes.CardID? Draw()
        {
            if (_deckList.Count == 0) { return null; }
            Attributes.CardID result = _deckList[0];
            _deckList.RemoveAt(0);
            return result;
        }
        
        private void ResetDeck()
        {
            foreach (Attributes.CardID id in _cardIDs) _deckList.Add(id);
            _deckList = _deckList.Shuffle();
        }
    }
}