using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForAreaColor : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit raycastHit;

        //if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, 0), out raycastHit))
        //{
        //    if (raycastHit.collider.tag != "Ground")
        //    {
        //        return;
        //    }

        //    //Renderer rend = raycastHit.collider.GetComponent<MeshRenderer>();
        //    Texture2D texture2D = ToTexture2D(DrawColoredAreas._canvas);
        //    Vector2 pCoord = raycastHit.textureCoord;

        //    Debug.Log(texture2D);

        //    pCoord.x *= texture2D.width;
        //    pCoord.y *= texture2D.height;

        //    Color color = texture2D.GetPixel(Mathf.FloorToInt(pCoord.x), Mathf.FloorToInt(pCoord.y));

        //    Debug.Log("Picked color : " + color);
        //}
    }
}
