using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int width = 10;
    public int depth = 10;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                // Random height between 1 and 5
                float randomHeight = Random.Range(1f, 5f);
                Vector3 pos = new Vector3(x, randomHeight / 2, z);
                
                GameObject newTile = Instantiate(tilePrefab, pos, Quaternion.identity);
                newTile.transform.localScale = new Vector3(0.9f, randomHeight, 0.9f);
                newTile.transform.parent = this.transform; // Keep hierarchy clean
            }
        }
    }
}