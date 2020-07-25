using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    public Camera _camera;
    public Shader _drawShader;
    public int resolution = 1024;
    public Color[] colors;
    public int showDiscontinuities = 0;

    [HideInInspector]
    public static RenderTexture _canvas;
    private Material _objMaterial, _drawMaterial;
    private RaycastHit _hit;
    private int currentCol = 0;


    // Start is called before the first frame update
    void Start()
    {
        _drawMaterial = new Material(_drawShader);
        //_drawMaterial.SetVector("_Color", Color.red);
        _drawMaterial.SetVector("_Color", colors[currentCol]);

        _objMaterial = GetComponent<MeshRenderer>().sharedMaterial;

        _canvas = new RenderTexture(resolution, resolution, 0, RenderTextureFormat.ARGBFloat);
        _objMaterial.SetTexture("_Paint", _canvas);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) Draw();
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ChangeColor();
        }

        _objMaterial.SetFloat("_DiscOn", 1.0f * showDiscontinuities);
    }


    private void Draw() {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hit))
        {
            _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y));
            RenderTexture temp = RenderTexture.GetTemporary(_canvas.width, _canvas.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(_canvas, temp);
            Graphics.Blit(temp, _canvas, _drawMaterial);
            RenderTexture.ReleaseTemporary(temp);
        }
    }


    private void ChangeColor() {
        //Vector4 col = new Vector4(Random.value * 0.5f, Random.value * 0.5f + 0.5f, Random.value * 0.5f + 0.5f, 1);
        //Vector4 col = new Vector4(Random.value * 0.5f + 0.5f, Random.value * 0.5f, Random.value * 0.5f, 1);
        //Vector4 col = new Vector4(Random.value * 0.5f, Random.value * 0.5f, Random.value * 0.5f + 0.5f, 1);

        currentCol++;
        Vector4 col = colors[currentCol % colors.Length];

        _drawMaterial.SetVector("_Color", col);
        //_drawMaterial.SetFloat("_Opacity", Random.Range(0.3f, 1.5f));
        //_drawMaterial.SetFloat("_Size", Random.Range(60f, 100f));
    }


    //private void OnGUI()
    //{
    //    GUI.DrawTexture(new Rect(0, 0, 256, 256), _canvas, ScaleMode.ScaleToFit, false, 1);
    //}
}
