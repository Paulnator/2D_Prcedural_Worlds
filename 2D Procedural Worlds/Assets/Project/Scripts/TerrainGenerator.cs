using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Map Size and Scale")]
    public int width = 256;
    public int height = 256;
    public float scale = 20f;

    [Header("Terrains To Generate")]
    public Sprite dirtSprite;
    public Sprite grassSprite;
    public Sprite sandSprite;
    public Sprite stoneSprite;

    [Header("Objects to Spawn")]
    public List<Sprite> stoneSprites;
    public List<Sprite> bushSprites;
    public List<Sprite> sandBushesObjects;

    [Header("Objects chances")]
    public float stoneChance = 0.5f; 
    public float bushChance = 0.3f;
    public float sandBushesChance = 0.3f;

    [Header("Seed Settings")]
    [SerializeField] bool RandomGeneration = true;
    [SerializeField] string customSeed;

    private int seed;

    private void Start()
    {
        if(RandomGeneration == true)
        {
        GenerateSeed();
        }
        else
        {
            seed = int.Parse(customSeed);
        }


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

    private void PlaceSprite(Sprite terrainSprite, int x, int y)
    {       
        GameObject terrainObject = new GameObject("TerrainSprite");
        SpriteRenderer terrainRenderer = terrainObject.AddComponent<SpriteRenderer>();
        terrainRenderer.sprite = terrainSprite;
        terrainObject.transform.position = new Vector3(x, y, 0);

        float randomChance = Random.Range(0f, 1f);

        if (terrainSprite == dirtSprite || terrainSprite == stoneSprite)
        {
            if (randomChance < stoneChance)
            {
                Sprite randomStone = stoneSprites[Random.Range(0, stoneSprites.Count)];

                GameObject stoneObject = new GameObject("StoneSprite");
                SpriteRenderer stoneRenderer = stoneObject.AddComponent<SpriteRenderer>();
                stoneRenderer.sprite = randomStone;
                stoneObject.transform.position = new Vector3(x, y, 0);
                SpriteRenderer rock = stoneObject.GetComponent<SpriteRenderer>();
                stoneObject.AddComponent<CircleCollider2D>();
                rock.sortingLayerName = "Props";
            }
        }
        else if (terrainSprite == grassSprite)
        {
            if (randomChance < bushChance)
            {
                Sprite randomBush = bushSprites[Random.Range(0, bushSprites.Count)];

                GameObject bushObject = new GameObject("BushSprite");
                SpriteRenderer bushRenderer = bushObject.AddComponent<SpriteRenderer>();
                bushRenderer.sprite = randomBush;
                bushObject.transform.position = new Vector3(x, y, 0);
                SpriteRenderer bush = bushObject.GetComponent<SpriteRenderer>();
                bush.sortingLayerName = "Props";
                
            }
        }
        else if (terrainSprite == sandSprite)
        {
            if(randomChance < sandBushesChance)
            {
                Sprite randomBush = sandBushesObjects[Random.Range(0, sandBushesObjects.Count)];

                GameObject sandBush = new GameObject("BushSprite");
                SpriteRenderer bushRenderer = sandBush.AddComponent<SpriteRenderer>();
                bushRenderer.sprite = randomBush;
                sandBush.transform.position = new Vector3(x, y, 0);
                SpriteRenderer bush = sandBush.GetComponent<SpriteRenderer>();
                bush.sortingLayerName = "Props";
            }
        }

    }
}   
