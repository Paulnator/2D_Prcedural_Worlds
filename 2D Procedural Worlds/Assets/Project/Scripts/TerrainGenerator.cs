using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;

    [Header("Terrains To Generate")]
    public Sprite dirtSprite;
    public Sprite grassSprite;
    public Sprite sandSprite;
    public Sprite stoneSprite;

    private int seed;

    private void Start()
    {
        GenerateSeed();
        Debug.Log("Current seed = " + seed);
        GenerateTerrain(); 
    }

    private void GenerateTerrain()
    {
        Random.InitState(seed);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = ((float)x / width * scale) + seed;
                float yCoord = ((float)y / height * scale) + seed;

                float heightValue = Mathf.PerlinNoise(xCoord, yCoord);

                Sprite sprite = ChooseSprite(heightValue);
                PlaceSprite(sprite, x, y);
            }
        }
    }

    private void GenerateSeed()
    {
        seed = Random.Range(0, 100000); 
    }

    private Sprite ChooseSprite(float heightValue)
    {
        if (heightValue < 0.3f)
        {
            return dirtSprite;
        }
        else if (heightValue < 0.6f)
        {
            return grassSprite;
        }
        else if (heightValue < 0.8f)
        {
            return sandSprite;
        }
        else
        {
            return stoneSprite;
        }
    }

    private void PlaceSprite(Sprite sprite, int x, int y)
    {
        GameObject spriteObject = new GameObject("TerrainSprite");
        SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteObject.transform.position = new Vector3(x, y, 0);
    }   
}