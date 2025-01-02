using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Snap.Core
{
    public static class ListExtensions
    {
        public static IList<T> Shuffle<T>(this IList<T> list, int swapCount = 100)
        {
            if (list == null)
            {
                Debug.LogWarning("Attempting to shuffle null list");
                return null;
            }

            if (list.Count <= 1) { return list; }

            for (var i = 0; i < swapCount; i++)
            {
                int first = Random.Range(0, list.Count);
                int second;
                do { second = Random.Range(0, list.Count); } while (second == first);

                (list[first], list[second]) = (list[second], list[first]);
            }

            return list;
        }
    }
}