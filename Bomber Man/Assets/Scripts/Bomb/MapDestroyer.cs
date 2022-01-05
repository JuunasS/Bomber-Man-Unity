using Photon.Pun;
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

    // Creates an explosion on the given world position.
    public void Explode(Vector2 worldPos, GameObject player)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);

        ExplodeCell(originCell);

        // Checks if explosions can continue forward.
        if(GameManager.manager.isSingleplayer)
        {
            ExplosionCheck(player.GetComponent<PlayerController>().explosionDistance, originCell);
        } else
        {
            ExplosionCheck(player.GetComponent<PlayerControllerMultiplayer>().explosionDistance, originCell);

        }
    }

    private void ExplosionCheck(int distance, Vector3Int originCell)
    {
        for(int i = 1; i <= distance; i++)
        {
            if(ExplodeCell(originCell + new Vector3Int(i, 0, 0))) { } 
            else { break; }
        }

        for (int i = 1; i <= distance; i++)
        {
            if (ExplodeCell(originCell + new Vector3Int(0, i, 0))) { }
            else { break; }
        }

        for (int i = 1; i <= distance; i++)
        {
            if (ExplodeCell(originCell + new Vector3Int(-i, 0, 0))) { }
            else { break; }
        }

        for (int i = 1; i <= distance; i++)
        {
            if (ExplodeCell(originCell + new Vector3Int(0, -i, 0))) { }
            else { break; }
        }
    }


    [PunRPC]
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
