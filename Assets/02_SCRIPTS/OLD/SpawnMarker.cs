using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMarker : MonoBehaviour
{
    public Texture[] markers;

    public MeshRenderer rend;


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeMarker(int _index)
    {
        rend.sharedMaterial.mainTexture = markers[_index];
    }
}
