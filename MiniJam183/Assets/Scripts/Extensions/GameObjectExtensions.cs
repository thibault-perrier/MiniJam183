using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtensions
    {
        public static bool CompareFirstTagInParent(this GameObject _gameObject, string _tag)
        {
            GameObject _currentCheck = _gameObject;
            while (true)
            {
                if (!_currentCheck.CompareTag("Untagged"))
                {
                    return _currentCheck.CompareTag(_tag);
                }
                
                if (_currentCheck.transform.root == _currentCheck.transform)
                {
                    return false;
                }
            }
        }
    }
}