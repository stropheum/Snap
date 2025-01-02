using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Snap.Core
{
    public static class EnumerableExtensions
    {
        public static IList<T> Shuffle<T>(this IList<T> list, int swapCount = 100)
        {
            if (list == null)
            {
                Debug.LogWarning("Attempting to shuffle null list");
                return null;
            }

            switch (list.Count)
            {
                case 0:
                    Debug.LogWarning("Attempting to shuffle empty list");
                    break;
                case 1:
                    Debug.LogWarning("Attempting to shuffle one element");
                    break;
                default:
                    for (int i = 0; i < swapCount; i++)
                    {
                        int first = Random.Range(0, list.Count);
                        int second;
                        do
                        {
                            second = Random.Range(0, list.Count);
                        } while (second == first);
                        (list[first], list[second]) = (list[second], list[first]);
                    }

                    break;
            }

            return list;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list, int swapCount = 100)
        {
            return Shuffle(list as List<T>, swapCount);            
        }
    }
}