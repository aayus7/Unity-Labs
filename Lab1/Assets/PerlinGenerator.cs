using UnityEngine;

public class PerlinGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int width = 20;
    public int depth = 20;
    public float scale = 5f; // Higher = more hills, Lower = flatter
    public float heightMultiplier = 5f;

    void Start()
    {
        GeneratePerlinMap();
    }

    void GeneratePerlinMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                // Calculate Perlin value based on coordinates
                float xCoord = (float)x / width * scale;
                float zCoord = (float)z / depth * scale;
                float perlinValue = Mathf.PerlinNoise(xCoord, zCoord);

                float finalHeight = perlinValue * heightMultiplier;
                Vector3 pos = new Vector3(x, finalHeight / 2, z);

                GameObject newTile = Instantiate(tilePrefab, pos, Quaternion.identity);
                newTile.transform.localScale = new Vector3(0.9f, finalHeight, 0.9f);
                newTile.transform.parent = this.transform;
            }
        }
    }
}