using UnityEngine;
using UnityEngine.XR;

public class ManualContinuousTurn : MonoBehaviour
{
    public Transform xrOrigin;     // XR Origin (or Camera Offset)
    public float turnSpeed = 60f;  // degrees per second

    InputDevice rightHand;
    public CarController carController;

    void Start()
    {
        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        if (!rightHand.isValid)
            rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 stick))
        {
            float turn = stick.x;
            xrOrigin.Rotate(Vector3.up, turn * turnSpeed * Time.deltaTime);
            carController.TurnSteering(turn);
        }
    }
}
