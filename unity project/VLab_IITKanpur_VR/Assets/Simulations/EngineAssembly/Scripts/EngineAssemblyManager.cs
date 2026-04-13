using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class EngineAssemblyManager : MonoBehaviour
{
    public List<Animator> engineAnimatorsList = new();
    public List<XRGrabInteractable> parts;
    public TMP_Text textBox;

    public List<string> instructions = new();

    int count = 0, instCount = 1;
    void Start()
    {
        DisableAnimators();
        StartCoroutine(DelayerCR());
    }

    IEnumerator DelayerCR()
    {
        yield return new WaitForSeconds(3f);
        StartInstructions();
        yield break;
    }

    void StartInstructions()
    {
        textBox.text = instructions[instCount];
        if (parts[count] != null)
            parts[count].enabled = true;
    }

    public void DisableAnimators()
    {
        foreach(var anim in engineAnimatorsList)
        {
            anim.enabled = false;
        }
    }

    public void EnableAnimators()
    {
        foreach (var anim in engineAnimatorsList)
        {
            anim.enabled = true;
        }

    }

    public void OnElementPlaced(XRGrabInteractable obj)
    {
        count++;
        instCount++;
        if(instCount <= 5)
        {
            StartInstructions();
        }
        else
        {
            textBox.text = "Congratulations! All parts are placed! now see the animation.";
        }
        

        StartCoroutine(DisableGrabCR(obj.GetComponent<BoxCollider>()));
        if (count == 5)
        {
            EnableAnimators();
        }
    }

    IEnumerator DisableGrabCR(BoxCollider box)
    {
        yield return new WaitForEndOfFrame();
        box.enabled = false;
        yield break;
    }
    
}
