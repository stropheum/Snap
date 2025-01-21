using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Snap.Card
{
    public class CardPool : MonoBehaviour
    {
        [SerializeField] private Card _prefab;
        private ObjectPool<Card> _pool;
        private HashSet<Card> _poolMembers = new();

        private void Awake()
        {
            Debug.Assert(_prefab != null, nameof(_prefab) + " != null");
            _pool = new ObjectPool<Card>(
                createFunc: CreateFunc,
                actionOnGet: ActionOnGet,
                actionOnRelease: ActionOnRelease,
                actionOnDestroy: ActionOnDestroy,
                collectionCheck: true,
                defaultCapacity: 50);
        }

        private IEnumerator Start()
        {
            yield return PrewarmPool(50);
        }

        public void ReleaseAll()
        {
            foreach (Card card in _poolMembers)
            {
                _pool.Release(card);
            }
        }
        
        private Card CreateFunc()
        {
            Card card = Instantiate(_prefab);
            _poolMembers.Add(card);
            card.gameObject.SetActive(false);
            return card;
        }
        
        private void ActionOnGet(Card card)
        {
            card.transform.SetParent(null);
            card.gameObject.SetActive(true);
        }

        private void ActionOnRelease(Card card)
        {
            card.gameObject.SetActive(false);
            card.transform.SetParent(transform);
        }

        private void ActionOnDestroy(Card card)
        {
        }

        private IEnumerator PrewarmPool(int initialCapacity)
        {
            for (int i = 0; i < initialCapacity; i++)
            {
                Card card = _pool.Get();
                card.transform.SetParent(transform);
                card.gameObject.SetActive(false);
                yield return null;
            }

            ReleaseAll();
        }
    }
}