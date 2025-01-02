using System.Collections.Generic;
using Snap.Core;
using UnityEngine;

namespace Snap.Card
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private Attributes.CardID[] _cardIDs = new Attributes.CardID[12];
        private Queue<Attributes.CardID> _deckQueue = new();

        private void Awake()
        {
            Debug.Assert(_cardIDs is { Length: 12 }, "Decks must have 12 cards.", gameObject);
        }

        private void Start()
        {
            Reset();
        }

        private void Reset()
        {
            foreach (Attributes.CardID id in _cardIDs) { _deckQueue.Enqueue(id); }
            _deckQueue = (Queue<Attributes.CardID>)_deckQueue.Shuffle();
        }
    }
}