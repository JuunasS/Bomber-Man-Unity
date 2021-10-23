using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile wallTile;
    public Tile wallTileTop;
    public Tile destructibleTile;

    public GameObject explosionPrefab;

    // Creates an explsion on the given world position.
    public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);

        ExplodeCell(originCell);

        // Checks if explosions can continue forward.
        if (ExplodeCell(originCell + new Vector3Int(1, 0, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(2, 0, 0));
        }
        if (ExplodeCell(originCell + new Vector3Int(0, 1, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(0, 2, 0));
        }
        if (ExplodeCell(originCell + new Vector3Int(-1, 0, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(-2, 0, 0));
        }
        if (ExplodeCell(originCell + new Vector3Int(0, -1, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(0, -2, 0));
        }
    }

    /*
     * Finds tile with given cell, checks if tile should be removed in the explosion.
     * Instansiates explosion prefab.
    */
   private bool ExplodeCell(Vector3Int cell)
    {
        Tile tile =  tilemap.GetTile<Tile>(cell);
        
        // Checks is explosion hits a wall.
        if (tile == wallTile || tile == wallTileTop)
        {
            // Explosion cannot happen.
            return false;
        } 

        // Checks if explosion hit a destructible tile.
        if (tile == destructibleTile)
        {
            // Remove tile
            tilemap.SetTile(cell, null);
        }

        // Create an explosion. (Animation)
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);

        // Explosion can continue
        return true;
    }

}
