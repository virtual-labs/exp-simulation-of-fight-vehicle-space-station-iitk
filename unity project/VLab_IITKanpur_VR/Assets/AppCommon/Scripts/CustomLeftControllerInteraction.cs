// Developed by Tushar. All Rights Reserved (c) VLab IIT Kanpur
// Description: Custom Left Controller Interactions. 
// !! Warning !! DO NOT OVERRIDE CALLBACKS FOR LEFT CONTROLLER BUTTONS X AND Y AS THEY PROVIDE GLOBAL INTERFACE FOR APP NAVIGATION.

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CustomLeftControllerInteraction : MonoBehaviour, XRIDefaultInputActions.IXRILeftInteractionActions

{
    private XRIDefaultInputActions actions;

    private void OnEnable()
    {
        if (actions == null)
        {
            actions = new XRIDefaultInputActions();
            actions.XRILeftInteraction.SetCallbacks(this);
        }
        actions.XRILeftInteraction.Enable();
    }
    private void OnDisable()
    {
        actions.XRILeftInteraction.Disable();
    }

    //----------------------------------------------------------------
    

    public void OnXPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AppManager.Instance.OnMenuRequest();
        }
    }

    public void OnYPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AppManager.Instance.OnQuitRequest();
        }
    }

    public void OnSelect(InputAction.CallbackContext context) { }
    public void OnSelectValue(InputAction.CallbackContext context) { }
    public void OnActivate(InputAction.CallbackContext context) { }
    public void OnActivateValue(InputAction.CallbackContext context) { }
    public void OnUIPress(InputAction.CallbackContext context) { }
    public void OnUIPressValue(InputAction.CallbackContext context) { }
    public void OnUIScroll(InputAction.CallbackContext context) { }
    public void OnTranslateManipulation(InputAction.CallbackContext context) { }
    public void OnRotateManipulation(InputAction.CallbackContext context) { }
    public void OnManipulation(InputAction.CallbackContext context) { }
    public void OnScaleToggle(InputAction.CallbackContext context) { }
    public void OnScaleOverTime(InputAction.CallbackContext context) { }

}