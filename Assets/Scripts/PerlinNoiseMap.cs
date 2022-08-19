using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMap : MonoBehaviour
{

    //PerlinNoiseMap을 이용해 타일을 미리 만들어두는 스크립트
    Dictionary<int, GameObject> tileset;
    Dictionary<int, GameObject> tile_groups;

    public GameObject prefab_0;
    public GameObject prefab_1;
    public GameObject prefab_2;
    public GameObject prefab_3;

    //맵의크기
    int map_width = 16000;  
    int map_height = 9000;  

    List<List<int>> noise_grid = new List<List<int>>();
    List<List<GameObject>> tile_grid=new List<List<GameObject>>();

    float magification = 7.0f;

    int x_offset = -20;
    int y_offset = -20;

    const int Range_X = 20;
    const int Range_Y = 12;

    private void Start()
    {
        CreateTileSet();
        CreateTileGroups();
        GenerateMap();
        PlayerBackGround();
    }

    private void Update()
    {
        
    }

    
    //플레이어가 근처로 가면 타일을 생성한다.
    public void PlayerBackGround()
    {
        int X = (int)GameManager.INSTANCE.PLAYER.transform.position.x + (int)(map_width * 0.5f);
        int Y = (int)GameManager.INSTANCE.PLAYER.transform.position.y + (int)(map_height * 0.5f);
        for (int i = X - Range_X; i < X + Range_X; i++)
        {
            for (int j = Y - Range_Y; j < Y + Range_Y; j++)
            {
                if (noise_grid[i][j] == -1)
                {
                    int tile_id = GetIdUsingPerlin(i, j);
                    noise_grid[i][j] = tile_id;
                    CreateTile(tile_id, i, j);
                }

            }
        }
    }

    void CreateTileSet()
    {
        tileset = new Dictionary<int, GameObject>();
        tileset.Add(0, prefab_0);
        tileset.Add(1, prefab_1);
        tileset.Add(2, prefab_2);
        tileset.Add(3, prefab_3);
    }

    void CreateTileGroups()
    {
        tile_groups = new Dictionary<int, GameObject>();
        foreach(var prefab_pair in tileset)
        {
            GameObject tile_group = new GameObject(prefab_pair.Value.name);
            tile_group.transform.parent = gameObject.transform;
            tile_group.transform.localPosition = new Vector3(-(map_width*0.5f), -(map_height*0.5f), 0);
            tile_groups.Add(prefab_pair.Key, tile_group);
        }
    }

    void GenerateMap()
    {
        for(int i=0; i<map_width; i++)
        {
            noise_grid.Add(new List<int>());
            tile_grid.Add(new List<GameObject>());

            for (int j = 0; j < map_height; j++)
            {

                //int tile_id = GetIdUsingPerlin(i, j);
                noise_grid[i].Add(-1);
                //CreateTile(tile_id, i, j);

            }
        }
    }

    int GetIdUsingPerlin(int x,int y)
    {
        float raw_perlin=Mathf.PerlinNoise((x-x_offset)/magification, (y-y_offset)/magification);

        float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);
        float scaled_perlin = clamp_perlin * tileset.Count;
        if(scaled_perlin==4)
        {
            scaled_perlin = 3;
        }

        return Mathf.FloorToInt(scaled_perlin);
    }

    void CreateTile(int tile_id,int x,int y)
    {
        GameObject tile_prefab = tileset[tile_id];
        GameObject tile_group = tile_groups[tile_id];
        GameObject tile = Instantiate(tile_prefab, tile_group.transform);

        tile.name = string.Format($"tile_x{x}_y{y}");
        tile.transform.localPosition = new Vector3(x, y, 0);

        tile_grid[x].Add(tile);

    }
}
