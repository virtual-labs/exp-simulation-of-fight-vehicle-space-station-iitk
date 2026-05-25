// Developed by Tushar.(c) 2025 Virtual Labs IITKanpur. All Rights Reserved.
// Experiment : Determination of Specific Resistance of a wire using Carey Foster Bridge.
// Description: Central Library for Supporting utility classes and structures.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace CareyFosterLib
{
    [Serializable]
    public class WireSpecimen
    {
        public GameObject wire;
        public GameObject zone;
        public XRSocketInteractor socket;
        public XRGrabInteractable grabInteractable;
        public Outline outline;

        public float radius = 1f;
        public float length = 1f;
        public Metaltype metaltype;
    }

    [Serializable]
    public enum Metaltype
    {
        copper,
        nichrome
    }

    [Serializable]
    public class Galvanometer
    {
        public GameObject galvanometer;
        public GameObject zone;
        public GameObject label;
        public XRSocketInteractor socket;
        public XRGrabInteractable grabInteractable;
        public Outline outline;
    }

    [Serializable]
    public class Battery
    {
        public GameObject battery;
        public GameObject zone;
        public GameObject label;
        public XRSocketInteractor socket;
        public XRGrabInteractable grabInteractable;
        public Outline outline;
    }

    [Serializable]
    public class ResistanceBox
    {
        public GameObject rBox;
        public GameObject zone;
        public GameObject label;
        public XRSocketInteractor socket;
        public XRGrabInteractable grabInteractable;
        public Outline outline;
    }

    [Serializable]
    public class Wire
    {
        public GameObject wire;
        public GameObject zone;
        public XRSocketInteractor socket;
        public XRGrabInteractable grabInteractable;
        public Outline outline;
    }

    [Serializable]
    public class Jockey
    {
        public GameObject jockey;
        public GameObject label;
        public XRGrabInteractable grabInteractable;
        public Outline outline;
    }

    [Serializable]
    public class MeterWire
    {
        public GameObject meterWire;
        public GameObject label;
        public Outline outline;
    }
}
