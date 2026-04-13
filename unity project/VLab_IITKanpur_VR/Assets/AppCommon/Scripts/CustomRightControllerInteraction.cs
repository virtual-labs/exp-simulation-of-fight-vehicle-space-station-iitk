// Developed by Tushar. All Rights Reserved (c) VLab IIT Kanpur
// Description: Custom Right Controller Interactions. use this to override A and B button bindings on Right controller.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomRightControllerInteraction : MonoBehaviour, XRIDefaultInputActions.IXRIRightInteractionActions
{
    private XRIDefaultInputActions actions;
    private void OnEnable()
    {
        if (actions == null)
        {
            actions = new XRIDefaultInputActions();
            actions.XRIRightInteraction.SetCallbacks(this);
        }
        actions.XRIRightInteraction.Enable();
    }
    private void OnDisable()
    {
        actions.XRIRightInteraction.Disable();
    }

    //----------------------------------------------------------------
    public void OnAActivate(InputAction.CallbackContext context) { }
    public void OnActivate(InputAction.CallbackContext context) { }
    public void OnActivateValue(InputAction.CallbackContext context) { }
    public void OnBActivate(InputAction.CallbackContext context) { }
    public void OnRotateAnchor(InputAction.CallbackContext context) { }
    public void OnScaleDelta(InputAction.CallbackContext context) { }
    public void OnScaleToggle(InputAction.CallbackContext context) { }
    public void OnSelect(InputAction.CallbackContext context) { }
    public void OnSelectValue(InputAction.CallbackContext context) { }
    public void OnTranslateAnchor(InputAction.CallbackContext context) { }
    public void OnUIPress(InputAction.CallbackContext context) { }
    public void OnUIPressValue(InputAction.CallbackContext context) { }
    public void OnUIScroll(InputAction.CallbackContext context) { }
    public void OnTranslateManipulation(InputAction.CallbackContext context) { }
    public void OnRotateManipulation(InputAction.CallbackContext context) { }
    public void OnManipulation(InputAction.CallbackContext context) { }
    public void OnScaleOverTime(InputAction.CallbackContext context) { }
}