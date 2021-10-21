using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject bombPrefab;

    // Update is called once per frame
    void Update()
    {
        /* 
         * Finds mouse positon on screen and translates it to tilemap grid position.
         * Places bomb on the found tilemap grid position.
        */
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cell = tilemap.WorldToCell(worldPos);
            Vector3 cellCenterpos = tilemap.GetCellCenterWorld(cell);

            Instantiate(bombPrefab, cellCenterpos, Quaternion.identity);
        }
    }
}
