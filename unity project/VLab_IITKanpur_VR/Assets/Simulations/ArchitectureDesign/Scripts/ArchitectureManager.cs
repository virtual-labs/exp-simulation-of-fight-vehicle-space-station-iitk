using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ArchitectureManager : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip introClip, endClip;
    int count = 0;

    public TMP_Text debugText;
    public List<XRGrabInteractable> items = new();
    public Transform fan1, fan2;
    public float fanSpeed = 1f;

    void Start()
    {
        foreach(var item in items)
        {
            item.enabled = false;
        }

        StartCoroutine(PlayIntroCR());
    }

    void Update()
    {
        fan1.Rotate(0f, fanSpeed, 0f);
        fan2.Rotate(0f, fanSpeed, 0f);
    }

    IEnumerator PlayIntroCR()
    {
        aud.clip = introClip;
        aud.Play();
        yield return new WaitForSeconds(introClip.length + 1f);

        foreach (var item in items)
        {
            item.enabled = true;
        }

        yield break;
    }

    public void OnGrab(ObjectScaleData data)
    {
        data.gameObject.transform.localScale = data.grabbedScale;
        debugText.text = $" SELECT ENTERED : {data.gameObject.name} : {data.grabbedScale} : scale : {data.gameObject.transform.localScale}";
    }

    public void OnGrabReleased(ObjectScaleData data)
    {
        data.gameObject.transform.localScale = data.releasedScale;
        debugText.text = $" SELECT EXITED : {data.gameObject.name} : {data.releasedScale} : scale : {data.gameObject.transform.localScale}";
    }

    public void OnItemPlaced(XRGrabInteractable obj)
    {
        aud.clip = obj.gameObject.GetComponent<ObjectScaleData>().clip;
        aud.Play();
        StartCoroutine(DisableGrabCR(obj));
        count++;

        if(count == 4)
        {
            aud.clip = endClip;
            aud.Play();
        }
    }

    IEnumerator DisableGrabCR(XRGrabInteractable item)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(1f);
        item.enabled = false;
        item.gameObject.GetComponent<Rigidbody>().Sleep();
        yield break;
    }
}
