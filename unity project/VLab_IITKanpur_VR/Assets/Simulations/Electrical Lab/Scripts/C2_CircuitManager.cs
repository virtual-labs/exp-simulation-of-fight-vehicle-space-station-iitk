using UnityEngine;

public class C2_CircuitManager : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor batterySocket;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor switchSocket1;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor switchSocket2;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor bulbSocket;


    public MeshRenderer bulbRenderer;
    public Material glassMaterial;
    public Material glowMaterial;

    void Update()
    {
        if (batterySocket.hasSelection && (switchSocket1.hasSelection && switchSocket2.hasSelection) && bulbSocket.hasSelection)
        {
            bulbRenderer.material = glowMaterial;
        }
        else
        {
            bulbRenderer.material = glassMaterial;
        }
    }
}

