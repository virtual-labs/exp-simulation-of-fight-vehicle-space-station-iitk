// Developed by Tushar.(c) 2025 Virtual Labs IITKanpur. All Rights Reserved.
// Experiment : Determination of Specific Resistance of a wire using Carey Foster Bridge.
// Description: This Component is Central Manager for the experiment.

using CareyFosterLib;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class CareyFosterManager : MonoBehaviour
{
    [Header("<color=red> ---- Core Utils ---- </color>")]
    public GameObject XROrigin;
    public GameObject cameraOffset, cam;

    [Header("<color=blue> ---- Input Controls ---- </color>")]
    public bool isControlsEnabled = false;
    public bool isJokeyLineEnabled = false;

    [Header("<color=yellow> ---- Audio ---- </color>")]
    public AudioSource main;
    public AudioSource sfxSource;
    public List<AudioClip> voiceOvers = new();
    public List<AudioClip> exclamationVOs = new();
    public List<AudioClip> soundFXs = new();

    [Header("<color=green> ---- Info Graphics ---- </color>")]
    public GameObject drImage;
    public TMP_Text headingText;
    public TMP_Text contentText;

    public LineRenderer line;
    public Transform point1, point2;
    public GameObject arrow;

    [Header("<color=blue> ---- Collections ---- </color>")]
    public List<WireSpecimen> wirespecimen = new();
    public List<Wire> wires = new();
    public Wire jockeyWire;

    [Header("<color=red> ---- Resistances ---- </color>")]
    public ResistanceBox rb1;
    public ResistanceBox rb2, rb3;
    public ResistanceBox copperStrip;

    [Header("<color=green> ---- Other Components ---- </color>")]
    public Galvanometer galvanomaeter;
    public Jockey jockey;
    public Battery battery;
    public MeterWire meterWire;
    public GameObject wirelabel, benchLabel;

    public Outline gripLeft, gripRight, copperBench;

    public DynamicMoveProvider moveProvider;
    public SnapTurnProvider snapTurnProvider;
    public ContinuousTurnProvider continuousTurnProvider;
    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        ConnectJockeyToGalvanometer();
    }

    public void InitializeGame()
    {
        isControlsEnabled = false;
        DisableWireSockets();

        moveProvider.enabled = false;
        snapTurnProvider.enabled = false;
        continuousTurnProvider.enabled = false;

        rb1.grabInteractable.enabled = false;
        rb2.grabInteractable.enabled = false;
        rb3.grabInteractable.enabled = false;
        copperStrip.grabInteractable.enabled = false;

        galvanomaeter.grabInteractable.enabled = false;
        battery.grabInteractable.enabled = false;
        jockey.grabInteractable.enabled = false;
        jockey.jockey.GetComponent<Rigidbody>().Sleep();
        jockeyWire.grabInteractable.enabled = false;

        foreach (var w in wirespecimen)
        {
            w.grabInteractable.enabled = false;
        }

        StartCoroutine(nameof(IntroVOCR));
    }

    IEnumerator IntroVOCR()
    {
        //===========================Intro Phase=======================
        main.clip = voiceOvers[0];
        main.Play();
        yield return new WaitForSeconds(main.clip.length + 1f);

        main.clip = voiceOvers[1];
        main.Play();
        yield return new WaitForSeconds(main.clip.length + 1f);

        main.clip = voiceOvers[2];
        main.Play();
        yield return new WaitForSeconds(main.clip.length + 1f);

        main.clip = voiceOvers[3];
        main.Play();
        yield return new WaitForSeconds(main.clip.length + 1f);

        main.clip = voiceOvers[4];
        main.Play();
        gripLeft.enabled = true;
        gripRight.enabled = true;
        yield return new WaitForSeconds(main.clip.length + 1f);

        main.clip = voiceOvers[5];
        main.Play();
        gripLeft.enabled = false;
        gripRight.enabled = false;
        yield return new WaitForSeconds(main.clip.length + 1f);

        main.clip = voiceOvers[6];
        main.Play();
        yield return new WaitForSeconds(main.clip.length + 1f);

        //============================Apparatus learning phase=====================

        //this is copper bench
        main.clip = voiceOvers[7];
        main.Play();
        benchLabel.SetActive(true);
        copperBench.enabled = true;
        yield return new WaitForSeconds(main.clip.length + 1f);

        //these are resistance boxes
        main.clip = voiceOvers[8];
        main.Play();
        copperBench.enabled = false;

        rb1.outline.enabled = true;
        rb2.outline.enabled = true;
        rb3.outline.enabled = true;
        copperStrip.outline.enabled = true;

        rb1.label.SetActive(true);
        rb2.label.SetActive(true);
        rb3.label.SetActive(true);
        copperStrip.label.SetActive(true);

        yield return new WaitForSeconds(main.clip.length + 1f);

        //this is battery
        main.clip = voiceOvers[9];
        main.Play();

        rb1.outline.enabled = false;
        rb2.outline.enabled = false;
        rb3.outline.enabled = false;
        copperStrip.outline.enabled = false;

        battery.outline.enabled = true;
        battery.label.SetActive(true);

        yield return new WaitForSeconds(main.clip.length + 1f);

        //this is galvanometer
        main.clip = voiceOvers[10];
        main.Play();

        battery.outline.enabled = false;

        galvanomaeter.outline.enabled = true;
        galvanomaeter.label.SetActive(true);

        yield return new WaitForSeconds(main.clip.length + 1f);

        //this is jockey
        main.clip = voiceOvers[11];
        main.Play();

        galvanomaeter.outline.enabled = false;
        jockey.outline.enabled = true;
        jockey.label.SetActive(true);

        yield return new WaitForSeconds(main.clip.length + 1f);

        //this is connecting wire
        main.clip = voiceOvers[12];
        main.Play();

        jockey.outline.enabled = false;
        wirelabel.SetActive(true);

        foreach(var wire in wires)
        {
            wire.outline.enabled = true;
        }
        yield return new WaitForSeconds(main.clip.length + 1f);

        //this is meter bridge wire
        main.clip = voiceOvers[13];
        main.Play();

        foreach (var wire in wires)
        {
            wire.outline.enabled = false;
        }

        meterWire.outline.enabled = true;
        meterWire.label.SetActive(true);

        yield return new WaitForSeconds(main.clip.length + 1f);

        //you are now familier
        main.clip = voiceOvers[14];
        main.Play();

        meterWire.outline.enabled = false;

        yield return new WaitForSeconds(main.clip.length + 1f);

        benchLabel.SetActive(false);
        rb1.label.SetActive(false);
        rb2.label.SetActive(false);
        rb3.label.SetActive(false);
        copperStrip.label.SetActive(false);
        battery.label.SetActive(false);
        galvanomaeter.label.SetActive(false);
        jockey.label.SetActive(false);
        wirelabel.SetActive(false);
        meterWire.label.SetActive(false);

        moveProvider.enabled = true;
        snapTurnProvider.enabled = true;
        continuousTurnProvider.enabled = true;

        yield return new WaitForSeconds(2f);
        StartCoroutine(nameof(EnableBatteryInteractionCR));

        yield break;
    }

    #region BatteryPlacement
    IEnumerator EnableBatteryInteractionCR()
    {
        main.clip = voiceOvers[15];
        main.Play();
        battery.outline.enabled = true;
        yield return new WaitForSeconds(main.clip.length + 1f);

        battery.outline.enabled = false;
        battery.grabInteractable.enabled = true;
        battery.zone.SetActive(true);
        yield break;
    }

    public void OnBatteryPlaced()
    {
        StartCoroutine(nameof(OnBatteryPlacedCR));
    }

    IEnumerator OnBatteryPlacedCR()
    {
        battery.zone.SetActive(false);
        
        sfxSource.clip = soundFXs[Random.Range(0, soundFXs.Count)];
        sfxSource.Play();

        yield return new WaitForSeconds(sfxSource.clip.length + 1f);

        battery.grabInteractable.enabled = false;
        battery.battery.GetComponent<Rigidbody>().Sleep();
        battery.battery.GetComponent<BoxCollider>().enabled = false;

        main.clip = exclamationVOs[Random.Range(0, exclamationVOs.Count)];
        main.Play();

        yield return new WaitForSeconds(main.clip.length + 1f);
        StartCoroutine(nameof(EnableRB1InteractionCR));

        yield break;
    }

    #endregion

    //----------------------------------------------------------------

    #region RB1 Placement
    IEnumerator EnableRB1InteractionCR()
    {
        main.clip = voiceOvers[16];
        main.Play();
        rb1.outline.enabled = true;
        yield return new WaitForSeconds(main.clip.length + 1f);

        rb1.outline.enabled = false;
        rb1.grabInteractable.enabled = true;
        rb1.zone.SetActive(true);
        yield break;
    }

    public void OnRB1Placed()
    {
        StartCoroutine(nameof(OnRB1PlacedCR));
    }

    IEnumerator OnRB1PlacedCR()
    {
        rb1.zone.SetActive(false);

        sfxSource.clip = soundFXs[Random.Range(0, soundFXs.Count)];
        sfxSource.Play();

        yield return new WaitForSeconds(sfxSource.clip.length + 1f);

        rb1.grabInteractable.enabled = false;
        rb1.rBox.GetComponent<Rigidbody>().Sleep();
        rb1.rBox.GetComponent<BoxCollider>().enabled = false;

        main.clip = exclamationVOs[Random.Range(0, exclamationVOs.Count)];
        main.Play();

        yield return new WaitForSeconds(main.clip.length + 1f);
        StartCoroutine(nameof(EnableRB2InteractionCR));

        yield break;
    }
    #endregion

    #region RB2 Placement
    IEnumerator EnableRB2InteractionCR()
    {
        main.clip = voiceOvers[17];
        main.Play();
        rb2.outline.enabled = true;
        yield return new WaitForSeconds(main.clip.length + 1f);

        rb2.outline.enabled = false;
        rb2.grabInteractable.enabled = true;
        rb2.zone.SetActive(true);
        yield break;
    }

    public void OnRB2Placed()
    {
        StartCoroutine(nameof(OnRB2PlacedCR));
    }

    IEnumerator OnRB2PlacedCR()
    {
        rb2.zone.SetActive(false);

        sfxSource.clip = soundFXs[Random.Range(0, soundFXs.Count)];
        sfxSource.Play();

        yield return new WaitForSeconds(sfxSource.clip.length + 1f);

        rb2.grabInteractable.enabled = false;
        rb2.rBox.GetComponent<Rigidbody>().Sleep();
        rb2.rBox.GetComponent<BoxCollider>().enabled = false;

        main.clip = exclamationVOs[Random.Range(0, exclamationVOs.Count)];
        main.Play();

        yield return new WaitForSeconds(main.clip.length + 1f);
        StartCoroutine(nameof(EnableRB3InteractionCR));

        yield break;
    }
    #endregion

    #region RB3 Placement
    IEnumerator EnableRB3InteractionCR()
    {
        main.clip = voiceOvers[18];
        main.Play();
        rb3.outline.enabled = true;
        yield return new WaitForSeconds(main.clip.length + 1f);

        rb3.outline.enabled = false;
        rb3.grabInteractable.enabled = true;
        rb3.zone.SetActive(true);
        yield break;
    }

    public void OnRB3Placed()
    {
        StartCoroutine(nameof(OnRB3PlacedCR));
    }

    IEnumerator OnRB3PlacedCR()
    {
        rb3.zone.SetActive(false);

        sfxSource.clip = soundFXs[Random.Range(0, soundFXs.Count)];
        sfxSource.Play();

        yield return new WaitForSeconds(sfxSource.clip.length + 1f);

        rb3.grabInteractable.enabled = false;
        rb3.rBox.GetComponent<Rigidbody>().Sleep();
        rb3.rBox.GetComponent<BoxCollider>().enabled = false;

        main.clip = exclamationVOs[Random.Range(0, exclamationVOs.Count)];
        main.Play();

        yield return new WaitForSeconds(main.clip.length + 1f);
        StartCoroutine(nameof(EnableCopperStripInteractionCR));

        yield break;
    }
    #endregion

    #region CopperStrip Placement
    IEnumerator EnableCopperStripInteractionCR()
    {
        main.clip = voiceOvers[19];
        main.Play();
        copperStrip.outline.enabled = true;
        yield return new WaitForSeconds(main.clip.length + 1f);

        copperStrip.outline.enabled = false;
        copperStrip.grabInteractable.enabled = true;
        copperStrip.zone.SetActive(true);
        yield break;
    }

    public void OnCopperStripPlaced()
    {
        StartCoroutine(nameof(OnCopperStripPlacedCR));
    }

    IEnumerator OnCopperStripPlacedCR()
    {
        copperStrip.zone.SetActive(false);

        sfxSource.clip = soundFXs[Random.Range(0, soundFXs.Count)];
        sfxSource.Play();

        yield return new WaitForSeconds(sfxSource.clip.length + 1f);

        copperStrip.grabInteractable.enabled = false;
        copperStrip.rBox.GetComponent<Rigidbody>().Sleep();
        copperStrip.rBox.GetComponent<BoxCollider>().enabled = false;

        main.clip = exclamationVOs[Random.Range(0, exclamationVOs.Count)];
        main.Play();

        yield return new WaitForSeconds(main.clip.length + 1f);
        StartCoroutine(nameof(EnableGalvanometerInteractionCR));

        yield break;
    }
    #endregion

    #region Galvanometer Placement
    IEnumerator EnableGalvanometerInteractionCR()
    {
        main.clip = voiceOvers[20];
        main.Play();
        galvanomaeter.outline.enabled = true;
        yield return new WaitForSeconds(main.clip.length + 1f);

        galvanomaeter.outline.enabled = false;
        galvanomaeter.grabInteractable.enabled = true;
        galvanomaeter.zone.SetActive(true);
        yield break;
    }

    public void OnGalvanometerPlaced()
    {
        StartCoroutine(nameof(OnGalvanometerPlacedCR));
    }

    IEnumerator OnGalvanometerPlacedCR()
    {
        galvanomaeter.zone.SetActive(false);

        sfxSource.clip = soundFXs[Random.Range(0, soundFXs.Count)];
        sfxSource.Play();

        yield return new WaitForSeconds(sfxSource.clip.length + 1f);

        galvanomaeter.grabInteractable.enabled = false;
        galvanomaeter.galvanometer.GetComponent<Rigidbody>().Sleep();
        galvanomaeter.galvanometer.GetComponent<BoxCollider>().enabled = false;

        main.clip = exclamationVOs[Random.Range(0, exclamationVOs.Count)];
        main.Play();

        yield return new WaitForSeconds(main.clip.length + 1f);
        EnableWireSockets();
        StartCoroutine(nameof(EnableWireInteractionCR));

        yield break;
    }
    #endregion

    #region Wire Placement
    int wireIndex = 0;
    IEnumerator EnableWireInteractionCR()
    {
        main.clip = voiceOvers[21];
        main.Play();
        wires[wireIndex].outline.enabled = true;
        yield return new WaitForSeconds(main.clip.length + 1f);

        wires[wireIndex].outline.enabled = false;
        wires[wireIndex].grabInteractable.enabled = true;
        wires[wireIndex].zone.SetActive(true);
        yield break;
    }

    public void OnWirePlaced()
    {
        StartCoroutine(nameof(OnWirePlacedCR));
    }

    IEnumerator OnWirePlacedCR()
    {
        wires[wireIndex].zone.SetActive(false);

        sfxSource.clip = soundFXs[Random.Range(0, soundFXs.Count)];
        sfxSource.Play();

        yield return new WaitForSeconds(sfxSource.clip.length + 1f);

        wires[wireIndex].grabInteractable.enabled = false;
        wires[wireIndex].wire.GetComponent<Rigidbody>().Sleep();
        wires[wireIndex].wire.GetComponent<MeshCollider>().enabled = false;

        main.clip = exclamationVOs[Random.Range(0, exclamationVOs.Count)];
        main.Play();

        yield return new WaitForSeconds(main.clip.length + 1f);

        wireIndex++;

        if(wireIndex <= 8)
        {
            StartCoroutine(nameof(EnableWireInteractionCR));
        }
        else
        {
            main.clip = voiceOvers[22];
            main.Play();
            yield return new WaitForSeconds(main.clip.length + 1f);

            StartCoroutine(nameof(EnableJokeyWireInteractionCR));
        }
        yield break;
    }
    #endregion

    #region Jockey Connection

    IEnumerator EnableJokeyWireInteractionCR()
    {
        main.clip = voiceOvers[23];
        main.Play();
        jockeyWire.outline.enabled = true;
        yield return new WaitForSeconds(main.clip.length + 1f);

        jockeyWire.outline.enabled = false;
        jockeyWire.grabInteractable.enabled = true;
        jockeyWire.zone.SetActive(true);
        yield break;
    }

    public void OnJockeyWirePlaced()
    {
        StartCoroutine(nameof(OnJockeyWirePlacedCR));
    }

    IEnumerator OnJockeyWirePlacedCR()
    {
        isJokeyLineEnabled = true;

        jockeyWire.zone.SetActive(false);
        jockeyWire.wire.SetActive(false);
        jockey.jockey.GetComponent<Rigidbody>().WakeUp();

        sfxSource.clip = soundFXs[Random.Range(0, soundFXs.Count)];
        sfxSource.Play();

        yield return new WaitForSeconds(sfxSource.clip.length + 1f);

        jockeyWire.grabInteractable.enabled = false;
        jockeyWire.wire.GetComponent<Rigidbody>().Sleep();
        jockeyWire.wire.GetComponent<CapsuleCollider>().enabled = false;

        main.clip = exclamationVOs[Random.Range(0, exclamationVOs.Count)];
        main.Play();

        yield return new WaitForSeconds(main.clip.length + 1f);

        main.clip = voiceOvers[24];
        main.Play();
        yield return new WaitForSeconds(main.clip.length + 1f);

        jockey.grabInteractable.enabled = true;

        yield break;
    }
    #endregion

    #region Utilities
    public void PlayVO()
    {
        main.clip = voiceOvers[3];
        main.Play();
    }
    public float dist, dur;
    IEnumerator MoveUpDownCR()
    {
        arrow.transform.DOMoveY(arrow.transform.position.y - dist, dur);
        yield return new WaitForSeconds(dur + 0.1f);
        arrow.transform.DOMoveY(arrow.transform.position.y + dist, dur);
        yield return new WaitForSeconds(dur + 0.1f);
        StartCoroutine(nameof(MoveUpDownCR));
    }
    void ConnectJockeyToGalvanometer()
    {
        if (isJokeyLineEnabled)
        {
            line.SetPosition(0, point1.position);
            line.SetPosition(1, point2.position);
            line.transform.position = point1.position;
        }
    }
    void DisableWireSockets()
    {
        foreach (var wire in wires)
        {
            wire.socket.socketActive = false;
            wire.grabInteractable.enabled = false;
        }
    }
    void EnableWireSockets()
    {
        foreach (var wire in wires)
        {
            wire.socket.socketActive = true;
        }
    }

    #endregion
}