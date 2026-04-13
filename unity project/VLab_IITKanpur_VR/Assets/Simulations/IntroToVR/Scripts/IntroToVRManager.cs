using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class IntroToVRManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject cup, skipBtn;
    public AudioSource aud;
    int count = 0;
    public List<XRGrabInteractable> carParts = new();

    public AudioClip clip1, clip2, clip3, clip4, clip5;

    bool grabbedAlready = false;
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoCompleted;
    }

    private void OnVideoCompleted(VideoPlayer source)
    {
       videoPlayer.gameObject.SetActive(false);
       StartCoroutine(IntroCR());
    }

    IEnumerator IntroCR()
    {
        aud.clip = clip1;
        aud.Play();
        yield return new WaitForSeconds(clip1.length + 1f);

        aud.clip = clip2;
        aud.Play();
        yield return new WaitForSeconds(clip2.length + 1f);

        aud.clip = clip3;
        aud.Play();
        yield return new WaitForSeconds(clip3.length + 1f);

        cup.SetActive(true);

        yield break;
    }

    public void GrabbedCupFirstTime()
    {
        IEnumerator GrabbedCupFirstTimeCR()
        {
            aud.clip = clip4;
            aud.Play();
            yield return new WaitForSeconds(clip4.length + 1f);

            FixCar();

            yield break;
        }
        if (!grabbedAlready)
        {
            grabbedAlready = true;
            StartCoroutine(GrabbedCupFirstTimeCR());
        }
        
    }

    public void FixCar()
    {
        foreach (var part in carParts)
        {
            part.gameObject.SetActive(true);
        }
    }

    public void OnItemPlaced(XRGrabInteractable obj)
    {
        count++;
        obj.gameObject.SetActive(false);

        if (count == 4)
        {
            aud.clip = clip5;
            aud.Play();
        }
    }

    public void OnSkip()
    {
        videoPlayer.loopPointReached -= OnVideoCompleted;
        videoPlayer.gameObject.SetActive(false);
        StartCoroutine(IntroCR());
        skipBtn.SetActive(false);
    }

    void Update()
    {
        
    }
}
