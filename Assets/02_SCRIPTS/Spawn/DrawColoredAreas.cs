using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawColoredAreas : MonoBehaviour
{
    public Shader _drawShader;
    public int resolution = 1024;
    public Color[] colors;

    [HideInInspector]
    public static RenderTexture _canvas;

    private Material _objMaterial, _drawMaterial;
    private RaycastHit _hit;
    private int currentCol = 0;


    // Start is called before the first frame update
    void Awake()
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
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    ChangeCol(5);
        //}
    }


    public void Draw(Vector3 _pos)
    {
        _drawMaterial.SetVector("_Coordinate", new Vector4(_pos.x, _pos.y));
        RenderTexture temp = RenderTexture.GetTemporary(_canvas.width, _canvas.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(_canvas, temp);
        Graphics.Blit(temp, _canvas, _drawMaterial);
        RenderTexture.ReleaseTemporary(temp);
    }


    public void ChangeCol(int _index)
    {
        //Vector4 col = new Vector4(Random.value * 0.5f, Random.value * 0.5f + 0.5f, Random.value * 0.5f + 0.5f, 1);
        //Vector4 col = new Vector4(Random.value * 0.5f + 0.5f, Random.value * 0.5f, Random.value * 0.5f, 1);
        //Vector4 col = new Vector4(Random.value * 0.5f, Random.value * 0.5f, Random.value * 0.5f + 0.5f, 1);

        //Vector4 col = colors[currentCol % colors.Length];
        Vector4 col = colors[_index];

        _drawMaterial.SetVector("_Color", col);
        //_drawMaterial.SetFloat("_Opacity", Random.Range(0.3f, 1.5f));
        //_drawMaterial.SetFloat("_Size", Random.Range(60f, 100f));
    }


    public void ChangeSize(float _size) {
        _drawMaterial.SetFloat("_Size", _size);
    }


    //public void ShowAreas(bool _on) {
    //    float _hide = (_on) ? 0 : 1;
    //    _objMaterial.SetFloat("_HideAreas", _hide);
    //}


    //private void OnGUI()
    //{
    //    GUI.DrawTexture(new Rect(0, 0, 256, 256), _canvas, ScaleMode.ScaleToFit, false, 1);
    //}
}
