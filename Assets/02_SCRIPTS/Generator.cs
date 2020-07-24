using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public SCENE_MANAGER sceneManager;

    [Header("Smell collider size")]
    public float smellCollSize;

    [Header("Erase collider size")]
    public float eraseCollSize;

    private float treeCount, bushCount, flowerCount, mushroomCount, herbCount;



    public GameObject CreateTree(Vector3 _pos, TreeScriptableObject _data)
    {
        // Create object
        GameObject _tree = new GameObject("Tree_" + treeCount);
        _tree.tag = "Spawn_Tree";
        treeCount++;

        // Create trunk
        int trunkIndex = Random.Range(0, _data.trunk.Count);
        var _trunk = Instantiate(_data.trunk[trunkIndex], Vector3.zero, Quaternion.Euler(-90, Random.Range(0, 360), 0));
        _trunk.GetComponent<MeshRenderer>().sharedMaterial = _data.trMat;
        _trunk.transform.parent = _tree.transform;
        float _trR = Random.Range(_data.trMinR, _data.trMaxR);
        float _trH = Random.Range(_data.trMinH, _data.trMaxH);
        _trunk.transform.localScale = new Vector3(_trR, _trR, _trH);

        // Create trunk foliage
        var _trFoliage = Instantiate(_data.foliage, Vector3.zero, Quaternion.Euler(-90, 0, 0));

        _trFoliage.GetComponent<MeshRenderer>().sharedMaterial = _data.folMat;

        float _folR = Random.Range(_data.folMinScale, _data.folMaxScale) / _trH;
        float _folH = Random.Range(_data.folMinScale, _data.folMaxScale) / _trH;
        _trFoliage.transform.localScale = new Vector3(_folR, _folR, _folH);
        _trFoliage.transform.parent = _trunk.transform;
        _trFoliage.transform.localPosition = new Vector3(0, 0, _data.trunkHeights[trunkIndex]);


        // Create brenches
        float _brenchNum = Random.Range(0, 3);
        for (int i = 0; i < _brenchNum; i++)
        {
            var _brench = Instantiate(_data.brench, Vector3.zero, Quaternion.Euler(-45, Random.Range(0, 360), -90));
            _brench.GetComponent<MeshRenderer>().sharedMaterial = _data.trMat;
            float _z = Random.Range(_data.brMinH, _data.brMaxH);
            float s;
            if (_data.largerBrenchesBelow)
            {
                float brDeltaH = Mathf.Abs(_data.brMaxH - _data.brMinH);
                s = (Random.Range(_data.brMinScale, _data.brMaxScale) + (_data.brMaxH - _z) / brDeltaH) / 2;
            }
            else
            {
                s = Random.Range(_data.brMinScale, _data.brMaxScale);
            }
            _brench.transform.localScale = new Vector3(s, s, s);
            _brench.transform.parent = _trunk.transform;
            _brench.transform.localPosition = new Vector3(0, 0, _z);

            // Create brench foliage
            _trFoliage = Instantiate(_data.foliage, Vector3.zero, Quaternion.Euler(-90, 0, 0));

            _trFoliage.GetComponent<MeshRenderer>().sharedMaterial = _data.folMat;

            _folR = Random.Range(_data.folMinScale, _data.folMaxScale) / _trH * _data.brFolPropScale;
            _folH = Random.Range(_data.folMinScale, _data.folMaxScale) / _trH * _data.brFolPropScale;
            _trFoliage.transform.localScale = new Vector3(_folR, _folR, _folH);
            _trFoliage.transform.parent = _brench.transform;
            _trFoliage.transform.localPosition = _data.folOffset;
        }

        // Set position
        _tree.transform.position = _pos;

        // Set master scale
        _tree.transform.localScale *= _data.masterScale;

        // Create collider
        if (sceneManager != null) AddColliders(_tree, sceneManager.selection[0], _data.minCollScale, _data.maxCollScale, eraseCollSize, smellCollSize);

        //// Add color grabber
        //AddColorGrabber(_tree);

        return _tree;
    }


    public GameObject CreateBush(Vector3 _pos, BushScriptableObject _data)
    {
        // Create object
        GameObject _bush = new GameObject("Bush_" + bushCount);
        _bush.tag = "Spawn_Bush";
        bushCount++;

        float _shrubsNum = Random.Range(1, 5);
        for (int i = 0; i < _shrubsNum; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-_data.shrMaxDist, _data.shrMaxDist), 0, Random.Range(-_data.shrMaxDist, _data.shrMaxDist));

            var _shrubs = Instantiate(_data.shrubs, offset, Quaternion.Euler(-90, Random.Range(0, 360), 90));

            _shrubs.GetComponent<MeshRenderer>().sharedMaterial = _data.shrubsMat;

            Vector3 s = new Vector3(Random.Range(_data.minScale, _data.maxScale), Random.Range(_data.minScale, _data.maxScale), Random.Range(_data.minScale, _data.maxScale));
            if (_data.largerShrubsAtCenter)
            {
                float maxMagnitude = new Vector3(_data.shrMaxDist, 0, _data.shrMaxDist).magnitude;
                Debug.Log(maxMagnitude);
                Debug.Log(offset.magnitude);
                s = s * (1 - offset.magnitude / maxMagnitude);
            }

            _shrubs.transform.localScale = s;
            _shrubs.transform.parent = _bush.transform;
        }

        // Set position
        _bush.transform.position = _pos;

        // Set master scale
        _bush.transform.localScale *= _data.masterScale;

        // Create collider
        if (sceneManager != null) AddColliders(_bush, sceneManager.selection[0], _data.minCollScale, _data.maxCollScale, eraseCollSize, smellCollSize);

        //// Add color grabber
        //AddColorGrabber(_bush);

        return _bush;
    }


    public GameObject CreateFlower(Vector3 _pos, FlowerScriptableObject _data)
    {
        // Create object
        GameObject _flower = new GameObject("flower_" + flowerCount);
        _flower.tag = "Spawn_Grass";
        flowerCount++;

        // Create stem
        int stemIndex = Random.Range(0, _data.stems.Count);
        var _stem = Instantiate(_data.stems[stemIndex], Vector3.zero, Quaternion.Euler(-90, 0, 0));
        _stem.GetComponent<MeshRenderer>().sharedMaterial = _data.stemMat;
        _stem.transform.parent = _flower.transform;

        // Create corolla
        int corollaIndex = Random.Range(0, _data.corollas.Count);
        var _corolla = Instantiate(_data.corollas[corollaIndex], Vector3.zero, Quaternion.Euler(-90, 0, 0));
        _corolla.GetComponent<MeshRenderer>().sharedMaterial = _data.corollaMat;

        _corolla.transform.parent = _stem.transform;
        _corolla.transform.localPosition = new Vector3(_data.stemsWidth[stemIndex], 0, _data.stemsHeights[stemIndex]);

        // Set position
        _flower.transform.position = _pos;

        // Set master scale
        _flower.transform.localScale *= _data.masterScale;

        // Create collider
        if (sceneManager != null) AddColliders(_flower, sceneManager.selection[0], _data.minCollScale, _data.maxCollScale, eraseCollSize, smellCollSize);

        //// Add color grabber
        //AddColorGrabber(_flower);

        // Make grabbable
        MakeGrabbable(_flower);

        return _flower;
    }


    public GameObject CreateMushroom(Vector3 _pos, MushroomScriptableObject _data)
    {
        // Create object
        GameObject _mushroom = new GameObject("mushrooms_" + mushroomCount);
        _mushroom.tag = "Spawn_Grass";
        mushroomCount++;

        // Create body
        int bodyIndex = Random.Range(0, _data.body.Count - 1);
        var _body = Instantiate(_data.body[bodyIndex], Vector3.zero, Quaternion.Euler(Random.Range(-85, -95), Random.Range(0, 360), 0));
        _body.GetComponent<MeshRenderer>().sharedMaterial = _data.bodyMat;
        float _bodyR = Random.Range(_data.bodyMinScale, _data.bodyMaxScale);
        float _bodyH = Random.Range(_data.bodyMinScale, _data.bodyMaxScale);
        _body.transform.localScale = new Vector3(_bodyR, _bodyR, _bodyH);
        _body.transform.parent = _mushroom.transform;

        // Create head
        int headIndex = Random.Range(0, _data.head.Count - 1);
        var _head = Instantiate(_data.head[headIndex], Vector3.zero, Quaternion.Euler(Random.Range(-85, -95), Random.Range(0, 360), 0));
        _head.GetComponent<MeshRenderer>().sharedMaterial = _data.headMat;
        float _headR = Random.Range(_data.headMinScale, _data.headMaxScale);
        float _headH = Random.Range(_data.headMinScale, _data.headMaxScale);
        _head.transform.localScale = new Vector3(_headR, _headR, _headH);
        _head.transform.parent = _body.transform;
        _head.transform.localPosition = new Vector3(0, 0, _data.bodyHeights[bodyIndex]);

        // Set position
        _mushroom.transform.position = _pos;

        // Set master scale
        _mushroom.transform.localScale *= _data.masterScale;

        // Create collider
        if (sceneManager != null) AddColliders(_mushroom, sceneManager.selection[0], _data.minCollScale, _data.maxCollScale, eraseCollSize, smellCollSize);

        //// Add color grabber
        //AddColorGrabber(_mushroom);

        // Make grabbable
        MakeGrabbable(_mushroom);

        return _mushroom;
    }


    public GameObject CreateHerb(Vector3 _pos, HerbScriptableObject _data)
    {
        // Create object
        GameObject _herb = new GameObject("Herb_" + herbCount);
        _herb.tag = "Spawn_Grass";
        bushCount++;

        int num = Random.Range(_data.tussMinNum, _data.tussMaxNum);
        for (int i = 0; i < num; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-_data.tussMaxDist, _data.tussMaxDist), 0, Random.Range(-_data.tussMaxDist, _data.tussMaxDist));

            var _tussock = Instantiate(_data.tussock, offset, Quaternion.Euler(-90, Random.Range(0, 360), 0));
            _tussock.GetComponent<MeshRenderer>().sharedMaterial = _data.tussockMat;

            float r = Random.Range(_data.minScale, _data.maxScale);
            float h = Random.Range(_data.minScale, _data.maxScale);
            Vector3 s = new Vector3(r, h, r);
            _tussock.transform.localScale = s;
            _tussock.transform.parent = _herb.transform;
        }

        // Set position
        _herb.transform.position = _pos;

        // Set master scale
        _herb.transform.localScale *= _data.masterScale;

        // Create collider
        if (sceneManager != null) AddColliders(_herb, sceneManager.selection[0], _data.minCollScale, _data.maxCollScale, eraseCollSize, smellCollSize);

        //// Add color grabber
        //AddColorGrabber(_herb);

        return _herb;
    }


    private void AddColliders(GameObject _obj, int _tagNum, float _spawnCollMinSize, float _spawnCollMaxSize, float _deleteCollSize, float _smellCollSize)
    {
        // Add spawn collider
        GameObject _spawn = new GameObject("Spawn collider");
        _spawn.transform.parent = _obj.transform;
        _spawn.transform.localPosition = Vector3.zero;
        _spawn.layer = LayerMask.NameToLayer("Spawn");
        SphereCollider _spawnColl = _spawn.AddComponent<SphereCollider>();
        _spawnColl.radius = Random.Range(_spawnCollMinSize, _spawnCollMaxSize);
        _spawnColl.isTrigger = true;

        // Add delete collider
        GameObject _delete = new GameObject("Erase collider");
        _delete.transform.parent = _obj.transform;
        _delete.transform.localPosition = Vector3.zero;
        _delete.layer = LayerMask.NameToLayer("Erase");
        SphereCollider _deleteColl = _delete.AddComponent<SphereCollider>();
        _deleteColl.radius = _deleteCollSize;
        _deleteColl.isTrigger = true;
        _delete.tag = "Smell_" + _tagNum.ToString();

        // Add smell collider
        GameObject _smell = new GameObject("Smell collider");
        _smell.transform.parent = _obj.transform;
        _smell.transform.localPosition = Vector3.zero;
        _smell.layer = LayerMask.NameToLayer("Smell");
        CapsuleCollider _smellColl = _smell.AddComponent<CapsuleCollider>();
        _smellColl.radius = _smellCollSize;
        _smellColl.height = 10;
        _smellColl.isTrigger = true;
    }


    //private void AddColorGrabber(GameObject _obj) {
    //    _obj.AddComponent<ColorGrabber>();
    //}


    private void MakeGrabbable(GameObject _obj) {
        Rigidbody _rb = _obj.AddComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.isKinematic = true;

        CapsuleCollider _coll = _obj.AddComponent<CapsuleCollider>();
        _coll.height = 9;
        _coll.radius = 1;

        CustomGrabbable _grabbable = _obj.AddComponent<CustomGrabbable>();
        _grabbable.enabled = true;
        _grabbable.M_GrabPoints = _obj.GetComponents<CapsuleCollider>();
    }
}
