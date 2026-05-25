using UnityEngine;
using UnityEngine.XR;
public class ManualContinuousMove : MonoBehaviour
{
    InputDevice leftHand;

    public CarController controller;
    void Start()
    {
        leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    void Update()
    {
        if (!leftHand.isValid)
            leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (leftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 stick))
        {
            if(stick.x != 0 && stick.y != 0)
            {
                controller.OnCarMove();
            }
            else
            {
                controller.OnCarStop();
            }
        }
    }
}
