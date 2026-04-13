using UnityEngine;
 // Important for socket references!

public class CircuitManager : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor batterySocket;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor switchSocket;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor bulbSocket;


    public MeshRenderer bulbRenderer;
    public Material glassMaterial;
    public Material glowMaterial;

    void Update()
    {
        if (batterySocket.hasSelection && switchSocket.hasSelection && bulbSocket.hasSelection)
        {
            bulbRenderer.material = glowMaterial;
        }
        else
        {
            bulbRenderer.material = glassMaterial;
        }
    }
}
