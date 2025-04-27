using Extensions;
using Levels;
using UnityEngine;

public class Spike : MonoBehaviour, ITileMapObject
{
    public void OnTileEntered(Collider2D _other)
    {
        if (_other.TryGetComponentInParent(out RobotController _robotController))
        {
            Destroy(_robotController.gameObject);
        }
    }
}
