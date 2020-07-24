using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCENE_MANAGER : MonoBehaviour
{
    [HideInInspector] public int[] selection = { 0, 0 };
    [HideInInspector] public bool creativeMode = false;
    [HideInInspector] public bool UI_on = false;

    public GameObject pointer;
    public GameObject spawner;
    public GameObject eraser;
    public DrawColoredAreas canvas;
    public CastLine castLine;
    public GameObject preview;
    public GameObject UI_panel;

    SpawnMarker marker;
    int colBU;


    private void Awake()
    {
        marker = pointer.GetComponent<SpawnMarker>();
    }


    // Start is called before the first frame update
    void Start()
    {
        selection[0] = 0;
        selection[1] = 0;

        creativeMode = false;

        pointer.SetActive(false);
        castLine.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            colBU = selection[0];
            int[] newSel = { 5, selection[1] };
            updateSelection(newSel);
        }

        //if (Input.GetKeyUp(KeyCode.Mouse1))
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            int[] newSel = { colBU, selection[1] };
            updateSelection(newSel);
        }
    }


    public void updateSelection(int[] _sel) {
        selection = _sel;
        float _size = (_sel[0] != 5) ? 600 : 1200;

        canvas.ChangeSize(_size);
        canvas.ChangeCol(_sel[0]);

        marker.ChangeMarker(_sel[0]);
        castLine.ChangeLineCol(_sel[0]);

        spawner.SetActive(!(_sel[0] == 5));
    }

    public void toggleMode() {
        creativeMode = !creativeMode;

        pointer.SetActive(creativeMode);
        castLine.gameObject.SetActive(creativeMode);
        if (creativeMode) castLine.ChangeLineCol(selection[0]);
        //canvas.ShowAreas(creativeMode);

        preview.SetActive(creativeMode);
        UI_panel.SetActive(creativeMode);

        spawner.GetComponent<Spawner>().ResetLists();
    }
}
