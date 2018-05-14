using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class WallTile : Tile {

    [SerializeField]
    private Sprite[] wallSprites;
    [SerializeField]
    private Sprite preview;


    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);

                if (HasWall(tilemap, nPos))
                {
                    tilemap.RefreshTile(nPos);
                }
            }
        }
    }
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        string composition = string.Empty;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 || y != 0)
                {
                    if (HasWall(tilemap, new Vector3Int(position.x + x, position.y + y, position.z)))
                    {
                        composition += 'W';
                    }
                    else
                    {
                        composition += 'E';
                    }
                }
            }
        }
        tileData.sprite = wallSprites[0];

        if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'W' ||
            composition[1] == 'W' && composition[3] == 'E' && composition[4] == 'E' && composition[6] == 'W' ||
            composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'W' ||
            composition[1] == 'W' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[0];
        }
        if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[1];
        }
        if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[2];
        }
        if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'E' )
        {
            tileData.sprite = wallSprites[3];
        }
        if (composition[1] == 'E' && composition[3] == 'E' && composition[4] == 'E' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[5];
        }
        if (composition[1] == 'W' && composition[3] == 'E' && composition[4] == 'E' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[6];
        }
        if (composition[1] == 'E' && composition[3] == 'E' && composition[4] == 'E' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[7];
        }


    }

    private bool HasWall(ITilemap tilemap ,Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/WallTile")]
    public static void CreateWallTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Walltile", "New Walltile", "asset", "Save walltile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WallTile>(), path);
    }
#endif 
}
