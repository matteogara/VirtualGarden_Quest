using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerBush : MonoBehaviour
{
    public GameObject item;
    public int population = 5;
    public GameObject terrain;
    public float terrainMargin = 0.1f;
    public float threshold = 0.3f;
    public int maxIterations = 10000;
    public Material hm_material;

    private Texture2D terrainTex;
    private Vector3 terrainSize;
    private Color[] colors;


    // Start is called before the first frame update
    void Start()
    {
        if (terrain != null) {
            //terrainTex = terrain.GetComponent<MeshRenderer>().sharedMaterial.mainTexture as Texture2D;
            terrainSize = terrain.GetComponent<MeshRenderer>().bounds.size;
        }

        colors = terrain.GetComponent<DrawWithMouse>().colors;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnerBush();
        }
    }


    private void SpawnerBush() {

        terrainTex = toTexture2D(DrawWithMouse._canvas);
        Debug.Log(terrainTex);


        int i = 0;
        int safeCounter = 0;
        while (i < population && safeCounter < maxIterations) {
            float x = Random.Range(terrainMargin, 1 - terrainMargin);
            float z = Random.Range(terrainMargin, 1 - terrainMargin);
            float pixelLuma = terrainTex.GetPixel(Mathf.FloorToInt(x * terrainTex.width), Mathf.FloorToInt(z * terrainTex.height)).grayscale;
            Color pixelCol = terrainTex.GetPixel(Mathf.FloorToInt(x * terrainTex.width), Mathf.FloorToInt(z * terrainTex.height));

            int newCol = FindNearestCol(pixelCol);

            if (pixelLuma > threshold) {
                //float newX = (x - 0.5f) * terrainSize.x + terrain.transform.position.x;
                //float newZ = (z - 0.5f) * terrainSize.z + terrain.transform.position.z;
                float newX = (0.5f - x) * terrainSize.x + terrain.transform.position.x;
                float newZ = (0.5f - z) * terrainSize.z + terrain.transform.position.z;

                GameObject newItem = Instantiate(item, new Vector3(newX, 0, newZ), Quaternion.identity);
                newItem.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.color = colors[newCol];
                //newItem.transform.localScale = new Vector3(pixelLuma, pixelLuma, pixelLuma);
                newItem.transform.localScale = new Vector3(pixelLuma, pixelLuma * pixelLuma, pixelLuma);
                //newItem.transform.localScale = new Vector3(pixelLuma * pixelLuma, pixelLuma * pixelLuma, pixelLuma * pixelLuma);

                Debug.Log(i + "   " + pixelLuma);
                i++;
            }

            safeCounter++;

            //DeformTerrain(terrainTex);
        }
    }


    int FindNearestCol(Color _col) {
        float[] similarities = new float[colors.Length];

        for (int i = 0; i < similarities.Length; i++) {
            float deltaR = Mathf.Abs(_col.r - colors[i].r);
            float deltaG = Mathf.Abs(_col.g - colors[i].g);
            float deltaB = Mathf.Abs(_col.b - colors[i].b);

            similarities[i] = deltaR + deltaG + deltaB;
        }

        float min = 100f;
        int minIndex = 0;
        for (int j = 0; j < similarities.Length; j++) {
            if (similarities[j] < min) {
                min = similarities[j];
                minIndex = j;
            }
        }

        return minIndex;
    }


    private Texture2D toTexture2D(RenderTexture rTex)
    {
        RenderTexture.active = rTex;
        Texture2D dest = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);
        dest.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        dest.Apply();
        return dest;
    }


    private void DeformTerrain(Texture2D _tex) {
        Texture2D hm = new Texture2D(_tex.width, _tex.height, TextureFormat.RGBA32, false);

        for (int y = 0; y < _tex.height; y++)
        {
            for (int x = 0; x < _tex.width; x++)
            {
                Color pixel = _tex.GetPixel(x, y);

                float luma = 1 - ((pixel.r + pixel.g + pixel.b) / 3);
                pixel = new Color(luma, luma, luma, 1);

                hm.SetPixel(x, y, pixel);
            }
        }

        hm_material.SetTexture("_DispTex", hm);
        terrain.GetComponent<MeshRenderer>().sharedMaterial = hm_material;
    }


    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(.5f, .5f, 256, 256), terrainTex, ScaleMode.ScaleToFit, false, 1);
    }
}
