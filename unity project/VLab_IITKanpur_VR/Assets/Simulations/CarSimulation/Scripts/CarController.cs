using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class CarController : MonoBehaviour
{
    public float carSpeed;

    public TMP_Text debugText;
    public Transform steering;

    public Transform mainCamera;
    public Transform anchor;

    public GameObject car;
    public Vector3 carOffset;

    public Transform carTransform;
    public Transform xrOriginTransform;


    public DynamicMoveProvider moveProvider;
    public ManualContinuousTurn manualContinuousTurn;
    public ManualContinuousMove manualContinuousMove;

    public XRSimpleInteractable interactable;

    public bool isPowerOn;
    public AudioSource carAud;

    public AudioClip introClip, carStartClip, carMoveClip, carIdleClip;


    void Start()
    {
        /*debugText.text = $"Camera Local: {mainCamera.localPosition}, {mainCamera.localRotation} \n Anchor Local: {anchor.localPosition}, {anchor.localRotation} \n" +
            $"Camera Global: {mainCamera.position}, {mainCamera.rotation} \n Anchor Global: {anchor.position}, {anchor.rotation}";

        mainCamera.localPosition = anchor.localPosition;
        mainCamera.localRotation = anchor.localRotation;
        mainCamera.position = anchor.position;
        mainCamera.rotation = anchor.rotation;*/

        //Invoke(nameof(Delayer), 0.5f);
        
        StartCoroutine(PlayIntroCR());
    }

    void Delayer()
    {
        mainCamera.localPosition = anchor.localPosition;
        mainCamera.localRotation = anchor.localRotation;
        mainCamera.position = anchor.position;
        mainCamera.rotation = anchor.rotation;

        debugText.text = $"{debugText.text}+ \n Camera Local: {mainCamera.localPosition}, {mainCamera.localRotation} \n Anchor Local: {anchor.localPosition}, {anchor.localRotation} \n" +
            $"Camera Global: {mainCamera.position}, {mainCamera.rotation} \n Anchor Global: {anchor.position}, {anchor.rotation}";

    }


    IEnumerator PlayIntroCR()
    {
        carAud.clip = introClip;
        carAud.Play();
        yield return new WaitForSeconds(introClip.length + 1f);
        interactable.enabled = true;
    }


    void Update()
    {
        //debugText.text = $"XR origin POS-ROT: {transform.position}, {transform.rotation.eulerAngles} \n Camera POS-ROT: {mainCamera.position}, {mainCamera.rotation.eulerAngles}";
        //car.transform.rotation = Quaternion.Euler(0f, mainCamera.rotation.y, 0f);
        /*if (track) 
        {
            car.transform.position = mainCamera.position + carOffset;
            car.transform.rotation = Quaternion.Euler(0f, mainCamera.rotation.y, 0f);
        }*/
    }

    public void MakeCarChildOfOrigin()
    {
        //carTransform.parent = mainCamera.transform;
        //track = true;

        /*xrOriginTransform.rotation = Quaternion.Euler(Vector3.zero);
        car.transform.rotation = Quaternion.Euler(Vector3.zero);

        car.transform.position = xrOriginTransform.position + carOffset;
        car.transform.rotation = xrOriginTransform.rotation;
        carTransform.parent = xrOriginTransform;*/
    }

    public void SwitchPower() 
    { 
        isPowerOn = !isPowerOn;

        if(isPowerOn)
        {
            StartCoroutine(CarStartCR());
        }
        else
        {
            carAud.loop = false;
            carAud.Stop();
            moveProvider.enabled = false;
            manualContinuousTurn.enabled = false;
            manualContinuousMove.enabled = false;
        }
    }

    IEnumerator CarStartCR()
    {
        carAud.clip = carStartClip;
        carAud.Play();

        yield return new WaitForSeconds(carStartClip.length);

        carAud.loop = true;
        moveProvider.enabled = true;
        manualContinuousTurn.enabled = true;
        manualContinuousMove.enabled = true;

        carAud.clip = carIdleClip;
        carAud.Play();

        yield break;
    }

    public void OnCarMove()
    {
        carAud.clip = carMoveClip;
        if (!carAud.isPlaying)
        {
            carAud.Play();
        }
    }

    public void OnCarStop()
    {
        carAud.clip = carIdleClip;
        if (!carAud.isPlaying)
        {
            carAud.Play();
        }
    }

    public void TurnSteering(float x)
    {
        if(x > 0)
        {
            steering.localRotation = Quaternion.Euler(25f, 0f, -30f);
        }
        else if(x < 0)
        {
            steering.localRotation = Quaternion.Euler(25f, 0f, 30f);
        }
        else
        {
            steering.localRotation = Quaternion.Euler(25f, 0f, 0f);
        }
    }
}
