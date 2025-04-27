using System;
using Levels;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapTriggerDetection : MonoBehaviour
{
    private Tilemap tilemap;
    private TilemapCollider2D tilemapCollider2D;
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemapCollider2D = GetComponent<TilemapCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        var _worldToCell = tilemap.WorldToCell(tilemapCollider2D.ClosestPoint(_other.transform.position));
        GameObject _instantiatedObject = tilemap.GetInstantiatedObject(_worldToCell);
        if (_instantiatedObject && _instantiatedObject.TryGetComponent(out ITileMapObject _tileMapObject))
        {
            _tileMapObject.OnTileEntered(_other);
        }
    }
}
