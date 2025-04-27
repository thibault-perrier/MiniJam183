using UnityEngine;

namespace Levels
{
    public interface ITileMapObject
    {
        public void OnTileEntered(Collider2D _other);
    }
}