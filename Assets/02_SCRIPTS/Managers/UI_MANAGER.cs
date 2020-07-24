using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MANAGER : MonoBehaviour
{
    public SCENE_MANAGER sceneManager;

    //public GameObject panel;
    //public GameObject intro;
    //public GameObject windDebug;
    //public GameObject background;

    [Header("Row selection")]
    public Button[] rows;

    [Header("Smell selection")]
    public Button[] smells;

    [Header("Vegetation selection")]
    public Button[] vegs;

    [Header("Description text")]
    public Text description;
    public string[] smellAdjectives;
    public string[] objectNames;

    [Header("Sounds")]
    public AudioClip rowSound;
    public AudioClip columnSound;
    public AudioClip tabSound;

    [Header("Preview")]
    public Previewer previewer;

    int activeRow = 0;
    int[] _selection = { 0, 0 };
    int[] rowLength = { 4, 2 };

    AudioSource audioSource;
    bool refresh;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        RefreshUI();
        //RefreshVisibilities();
    }


    // Update is called once per frame
    void Update()
    {

        // Standard input
        //if (started && sceneManager.creativeMode)
        if (sceneManager.creativeMode)
        {
            refresh = false;

            if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickUp)) Up(OVRInput.Controller.LTouch);
            if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickUp)) Up(OVRInput.Controller.RTouch);

            if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft)) Left(OVRInput.Controller.LTouch);
            if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickLeft)) Left(OVRInput.Controller.RTouch);

            if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickDown)) Down(OVRInput.Controller.LTouch);
            if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickDown)) Down(OVRInput.Controller.RTouch);

            if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight)) Right(OVRInput.Controller.LTouch);
            if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickRight)) Right(OVRInput.Controller.RTouch);

            if (refresh)
            {
                RefreshUI();
            }
        }

        // Show / hide controls window
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    intro.SetActive(started);
        //    panel.SetActive(false);

        //    PlaySound(tabSound);
        //    started = !started;

        //    RefreshVisibilities();
        //}

        // Switch mode
        //if (Input.GetKeyDown(KeyCode.Space)) {
        if (OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.X)) {
            sceneManager.toggleMode();
            //panel.SetActive(false);

            //RefreshVisibilities();
        }
    }


    void Up(OVRInput.Controller _controller) {
        activeRow--;
        if (activeRow < 0)
        {
            activeRow = 0;
        }
        else
        {
            refresh = true;
            PlaySound(rowSound);
            VIBRATION_MANAGER.singleton.TriggerVibration(150, _controller);
        }
    }


    void Left(OVRInput.Controller _controller) {
        _selection[activeRow]--;
        if (_selection[activeRow] < 0)
        {
            _selection[activeRow] = 0;
        }
        else
        {
            refresh = true;
            PlaySound(columnSound);
            VIBRATION_MANAGER.singleton.TriggerVibration(150, _controller);
        }
    }


    void Down(OVRInput.Controller _controller) {
        activeRow++;
        if (activeRow > 1)
        {
            activeRow = 1;
        }
        else
        {
            refresh = true;
            PlaySound(rowSound);
            VIBRATION_MANAGER.singleton.TriggerVibration(150, _controller);
        }
    }


    void Right(OVRInput.Controller _controller) {
        _selection[activeRow]++;
        if (_selection[activeRow] > rowLength[activeRow])
        {
            _selection[activeRow] = rowLength[activeRow];
        }
        else
        {
            refresh = true;
            PlaySound(columnSound);
            VIBRATION_MANAGER.singleton.TriggerVibration(150, _controller);
        }
    }


    void RefreshUI()
    {
        DisableAllButtons();
        rows[activeRow].interactable = true;
        smells[_selection[0]].interactable = true;
        vegs[_selection[1]].interactable = true;
        UpdateText();

        sceneManager.updateSelection(_selection);

        previewer.UpdatePreview();
    }


    //void RefreshVisibilities()
    //{
    //    if (windDebug != null) windDebug.SetActive(!sceneManager.creativeMode && started);
    //    background.SetActive(!started || panel.activeInHierarchy);
    //}


    void DisableAllButtons()
    {
        foreach (Button _r in rows) _r.interactable = false;
        foreach (Button _s in smells) _s.interactable = false;
        foreach (Button _v in vegs) _v.interactable = false;
    }


    void UpdateText()
    {
        string _newText = "A " + smellAdjectives[_selection[0]] + " " + objectNames[_selection[1]] + ".";
        description.text = _newText;
    }


    void PlaySound(AudioClip _clip) {
        audioSource.clip = _clip;
        audioSource.Play();
    }
}
