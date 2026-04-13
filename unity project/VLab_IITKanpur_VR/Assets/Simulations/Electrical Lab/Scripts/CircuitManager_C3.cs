using UnityEngine;

public class CircuitManager_C3 : MonoBehaviour
{
    
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor batterySocket;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor switchSocketAND;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor switchSocketOR1;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor switchSocketOR2;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor bulbSocket;


    public MeshRenderer bulbRenderer;
    public Material glassMaterial;
    public Material glowMaterial;

    void Update()
    {
        if (batterySocket.hasSelection && ((switchSocketOR1.hasSelection || switchSocketOR2.hasSelection) && switchSocketAND) && bulbSocket.hasSelection)
        {
            bulbRenderer.material = glowMaterial;
        }
        else
        {
            bulbRenderer.material = glassMaterial;
        }
    }
}
