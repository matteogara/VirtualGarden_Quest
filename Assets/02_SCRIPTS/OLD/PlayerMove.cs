using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float margin = 5f;

    private float x, y, z;

    private void FixedUpdate()
    {
        Vector3 newPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5.1f);
        newPos = Camera.main.ScreenToWorldPoint(newPos);

        x = newPos.x;
        z = newPos.z;

        if (newPos.x <= -margin) x = -margin;
        if (newPos.x >= margin) x = margin;
        if (newPos.z <= -margin) z = -margin;
        if (newPos.z >= margin) z = margin;

        transform.position = new Vector3(x, newPos.y, z);
    }
}
