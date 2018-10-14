using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
        if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[1];
        }
        if (composition[1] == 'E' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[2];
        }
        if (composition[1] == 'W' && composition[3] == 'E' && composition[4] == 'E' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[3];
        }
        if (composition[1] == 'E' && composition[3] == 'E' && composition[4] == 'E' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[4];
        }
        if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[5];
        }
        if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[6];
        }
        if (composition[1] == 'W' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[7];
        }
        if (composition[1] == 'E' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[8];
        }
        if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[9];
        }
        if (composition[1] == 'W' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'W')
        {
            int randomVal = Random.Range(0, 100);
            if (randomVal <= 30)
            {
                tileData.sprite = wallSprites[10];
            }
            else if (randomVal > 30 && randomVal <= 60)
            {
                tileData.sprite = wallSprites[13];
            }
            else if (randomVal > 60 && randomVal <= 80)
            {
                tileData.sprite = wallSprites[11];
            }
            else
            {
                tileData.sprite = wallSprites[12];
            }
        }
        if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'W')
        {
            int randomVal = Random.Range(0, 100);
            if (randomVal <= 30)
            {
                tileData.sprite = wallSprites[14];
            }
            else if (randomVal > 30 && randomVal <= 60)
            {
                tileData.sprite = wallSprites[17];
            }
            else if (randomVal > 60 && randomVal <= 80)
            {
                tileData.sprite = wallSprites[15];
            }
            else
            {
                tileData.sprite = wallSprites[16];
            }
        }
        if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'E')
        {
            int randomVal = Random.Range(0, 100);
            if (randomVal <= 30)
            {
                tileData.sprite = wallSprites[18];
            }
            else if (randomVal > 30 && randomVal <= 60)
            {
                tileData.sprite = wallSprites[21];
            }
            else if (randomVal > 60 && randomVal <= 80)
            {
                tileData.sprite = wallSprites[19];
            }
            else
            {
                tileData.sprite = wallSprites[20];
            }
        }
        if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'W')
        {
            int randomVal = Random.Range(0, 100);
            if (randomVal <= 30)
            {
                tileData.sprite = wallSprites[22];
            }
            else if (randomVal > 30 && randomVal <= 60)
            {
                tileData.sprite = wallSprites[25];
            }
            else if (randomVal > 60 && randomVal <= 80)
            {
                tileData.sprite = wallSprites[23];
            }
            else
            {
                tileData.sprite = wallSprites[24];
            }
        }
        if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[26];
        }
        if (composition[1] == 'W' && composition[3] == 'E' && composition[4] == 'E' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[27];
        }
        if (composition[1] == 'W' && composition[2] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[28];
        }
        if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'W' && composition[7] == 'E')
        {
            tileData.sprite = wallSprites[29];
        }
        if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'E' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[30];
        }
        if (composition[0] == 'E' && composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[31];
        }
        if (composition[1] == 'W'&& composition[2] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'W' && composition[7] == 'E')
        {
            tileData.sprite = wallSprites[32];
        }
        if (composition[0] == 'E'&& composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'E' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[33];
        }
        if (composition[0] == 'E'&& composition[1] == 'W' && composition[2] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[34];
        }
        if (composition[1] == 'W'&& composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'E' && composition[6] == 'W' && composition[7] == 'E')
        {
            tileData.sprite = wallSprites[35];
        }
        if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'W' && composition[6] == 'W' && composition[7] == 'E')
        {
            tileData.sprite = wallSprites[36];
        }
        if (composition[0] == 'W' && composition[1] == 'W'&& composition[3] == 'W' && composition[4] == 'E' && composition[5] == 'E' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[37];
        }
        if (composition[0] == 'E' && composition[1] == 'W'&& composition[2] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[38];
        }
        if (composition[1] == 'W'&& composition[2] == 'E' && composition[3] == 'E' && composition[4] == 'W'&& composition[6] == 'W' && composition[7] == 'W')
        {
            tileData.sprite = wallSprites[39];
        }
        if (composition[1] == 'E'&& composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'E' && composition[6] == 'W' && composition[7] == 'W')
        {
            tileData.sprite = wallSprites[40];
        }
        if (composition[0] == 'E' && composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'E' && composition[5] == 'W' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[41];
        }
        if (composition[0] == 'W' && composition[1] == 'W' && composition[2] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[42];
        }
        if (composition[1] == 'W' && composition[2] == 'W' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'W' && composition[7] == 'E')
        {
            tileData.sprite = wallSprites[43];
        }
        if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'E' && composition[6] == 'W' && composition[7] == 'E')
        {
            tileData.sprite = wallSprites[44];
        }
        if (composition[0] == 'E' && composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'E' && composition[5] == 'E' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[45];
        }
        if (composition[0] == 'E' && composition[1] == 'W' && composition[2] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[46];
        }
        if (composition[1] == 'W' && composition[2] == 'E' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'W' && composition[7] == 'E')
        {
            tileData.sprite = wallSprites[47];
        }
        if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'E' && composition[5] == 'E' && composition[6] == 'W')
        {
            tileData.sprite = wallSprites[48];
        }
        if (composition[0] == 'E' && composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[49];
        }
        if (composition[1] == 'W' && composition[2] == 'E' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'E')
        {
            tileData.sprite = wallSprites[50];
        }
        if (composition[1] == 'E' && composition[3] == 'E' && composition[4] == 'W' && composition[6] == 'W' && composition[7] == 'E')
        {
            tileData.sprite = wallSprites[51];
        }
        if (composition[0] == 'E' &&  composition[1] == 'W' && composition[2] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'W' && composition[6] == 'W' && composition[7] == 'E')
        {
            tileData.sprite = wallSprites[52];
        }
        if (composition[0] == 'W' && composition[1] == 'W' && composition[2] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'E' && composition[6] == 'W' && composition[7] == 'E')
        {
            tileData.sprite = wallSprites[53];
        }
        if (composition[0] == 'E' && composition[1] == 'W' && composition[2] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'E' && composition[6] == 'W' && composition[7] == 'E')
        {
            tileData.sprite = wallSprites[54];
        }
        if (composition[0] == 'E' && composition[1] == 'W' && composition[2] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'E' && composition[6] == 'W' && composition[7] == 'W')
        {
            tileData.sprite = wallSprites[55];
        }
        if (composition[0] == 'E' && composition[1] == 'W' && composition[2] == 'E' && composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'E' && composition[6] == 'W' && composition[7] == 'E')
        {
            tileData.sprite = wallSprites[56];
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
