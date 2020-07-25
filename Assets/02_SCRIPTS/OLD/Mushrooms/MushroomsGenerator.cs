using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomsGenerator : MonoBehaviour
{
    [Header("Master scale")]
    public float masterScale = 1;

    [Header("Models")]
    public List<GameObject> body = new List<GameObject>();
    public List<float> bodyHeights = new List<float>();
    public List<GameObject> head = new List<GameObject>();

    [Header("Materials")]
    public Material bodyMat;
    public Material headMat;

    [Header("body settings")]
    public float bodyMinScale = 0.5f;
    public float bodyMaxScale = 1f;

    [Header("head settings")]
    public float headMinScale = 1f;
    public float headMaxScale = 1.5f;

    private float count;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            DeleteMushrooms();
            CreateMushrooms(transform.position);
        }
    }


    public void CreateMushrooms(Vector3 _pos) {
        // Create object
        GameObject _mushroom = new GameObject("mushrooms_" + count);
        count++;

        // Create body
        int bodyIndex = Random.Range(0, body.Count);
        var _body = Instantiate(body[bodyIndex], Vector3.zero, Quaternion.Euler(Random.Range(-85, -95), Random.Range(0, 360), 0));
        _body.GetComponent<MeshRenderer>().sharedMaterial = bodyMat;
        float _bodyR = Random.Range(bodyMinScale, bodyMaxScale);
        float _bodyH = Random.Range(bodyMinScale, bodyMaxScale);
        _body.transform.localScale = new Vector3(_bodyR, _bodyR, _bodyH);
        _body.transform.parent = _mushroom.transform;

        // Create head
        int headIndex = Random.Range(0, head.Count);
        var _head = Instantiate(head[headIndex], Vector3.zero, Quaternion.Euler(Random.Range(-85, -95), Random.Range(0, 360), 0));
        _head.GetComponent<MeshRenderer>().sharedMaterial = headMat;
        float _headR = Random.Range(headMinScale, headMaxScale);
        float _headH = Random.Range(headMinScale, headMaxScale);
        _head.transform.localScale = new Vector3(_headR, _headR, _headH);
        _head.transform.parent = _body.transform;
        _head.transform.localPosition = new Vector3(0, 0, bodyHeights[bodyIndex]);


        // Set position
        _mushroom.transform.position = _pos;

        // Set master scale
        _mushroom.transform.localScale *= masterScale;
    }


    void DeleteMushrooms() {
        GameObject _mushrooms = GameObject.Find("mushrooms_" + (count - 1));
        if (_mushrooms != null) {
            Destroy(_mushrooms);
        }
    }
}
