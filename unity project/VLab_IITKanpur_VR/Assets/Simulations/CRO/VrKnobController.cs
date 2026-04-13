using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WaveParameterButton : MonoBehaviour
{
    public enum ParameterType { Amplitude, Wavelength }
    public ParameterType parameterToModify = ParameterType.Amplitude;

    public SineWaveAnimator sineWave;
    public TriangleWaveAnimator triangleWave;
    public SquareWaveAnimator squareWave;

    public float amplitudeStep = 0.2f;
    public float minAmplitude = 0.1f;
    public float maxAmplitude = 10f;

    public float wavelengthStep = 1f;
    public float minWavelength = 2f;
    public float maxWavelength = 30f;

    void Start()
    {
        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.activated.AddListener(OnClicked);
        }
        else
        {
            Debug.LogError("XRBaseInteractable component missing from this GameObject.");
        }
    }

    private void OnClicked(ActivateEventArgs args)
    {
        if (parameterToModify == ParameterType.Amplitude)
        {
            float current = sineWave ? sineWave.amplitude : triangleWave ? triangleWave.amplitude : squareWave.amplitude;
            float newValue = Mathf.Clamp(current + amplitudeStep, minAmplitude, maxAmplitude);

            if (sineWave) sineWave.amplitude = newValue;
            if (triangleWave) triangleWave.amplitude = newValue;
            if (squareWave) squareWave.amplitude = newValue;

            Debug.Log("Amplitude increased to: " + newValue);
        }
        else if (parameterToModify == ParameterType.Wavelength)
        {
            float current = sineWave ? sineWave.waveLength : triangleWave ? triangleWave.waveLength : squareWave.waveLength;
            float newValue = Mathf.Clamp(current + wavelengthStep, minWavelength, maxWavelength);

            if (sineWave) sineWave.waveLength = newValue;
            if (triangleWave) triangleWave.waveLength = newValue;
            if (squareWave) squareWave.waveLength = newValue;

            Debug.Log("Wavelength increased to: " + newValue);
        }
    }
}
