// Developed by Tushar. All Rights Reserved (c) VLab IIT Kanpur
// Description: A Component to Initialize XR in desired Position and Rotation

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class AutoTeleportToAnchorXRI33 : MonoBehaviour
{
    public TeleportationProvider provider;
    public TeleportationAnchor anchor;

    private void Start()
    {
        var point = anchor.teleportAnchorTransform;

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = point.position,
            destinationRotation = point.rotation,

            matchOrientation = MatchOrientation.TargetUpAndForward
        };

        provider.QueueTeleportRequest(request);

    }
}
