using UnityEngine;
using TMPro;

public class FlameTest : MonoBehaviour
{
    [SerializeField] private ParticleSystem flameParticles;
    [SerializeField] private TMP_Text elementNameText;

    private readonly Color defaultFlameColor = new Color(1f, 0.843f, 0f); // Yellow (Sodium default)

   private void OnTriggerEnter(Collider other)
{
     Debug.Log("Entered FlameZone: " + other.name);

    if (flameParticles == null || elementNameText == null) return;

    string element = other.tag;
    Color flameColor;

    switch (element)
    {
        case "Sodium":
            flameColor = new Color(1f, 0.843f, 0f);
            break;
        case "Potassium":
            flameColor = new Color(0.71f, 0.49f, 0.86f);
            break;
        case "Copper":
            flameColor = new Color(0f, 1f, 0.666f);
            break;
        case "Calcium":
            flameColor = new Color(1f, 0.27f, 0f);
            break;
        case "Lithium":
            flameColor = new Color(0.86f, 0.08f, 0.24f);
            break;
        default:
            return; // Ignore other objects like IronOxide
    }

    var main = flameParticles.main;
    main.startColor = flameColor;

    elementNameText.text = "Metal Ion: " + element;
}

    private void OnTriggerExit(Collider other)
    {
        if (flameParticles != null)
        {
            var main = flameParticles.main;
            main.startColor = defaultFlameColor;
        }

        if (elementNameText != null)
        {
            elementNameText.text = "No sample in flame";
        }
    }
}