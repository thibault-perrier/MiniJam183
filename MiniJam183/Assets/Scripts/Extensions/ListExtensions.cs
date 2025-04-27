using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class ListExtensions
    {
        public static T PickRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new System.InvalidOperationException("The list is empty or null.");
            }

            int _index = Random.Range(0, list.Count);
            return list[_index];
        }
    }
}