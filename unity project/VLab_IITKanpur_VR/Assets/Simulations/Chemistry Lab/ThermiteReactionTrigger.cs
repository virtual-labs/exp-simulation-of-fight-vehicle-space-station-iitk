using UnityEngine;

public class ThermiteReactionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject thermiteEffect;
    [SerializeField] private GameObject moltenIronPrefab;

    private bool aluminumInZone = false;
    private bool ironOxideInZone = false;
    private bool hasReacted = false;
    public GameObject m1, m2;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        
        if (hasReacted) return;

        if (other.CompareTag("Aluminum"))
            aluminumInZone = true;

        if (other.CompareTag("IronOxide"))
            ironOxideInZone = true;

        if (aluminumInZone && ironOxideInZone)
        {
            React();
        }
    }

    private void React()
    {
        hasReacted = true;
        m1.SetActive(false);
        m2.SetActive(false);

        if (thermiteEffect != null)
            Instantiate(thermiteEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);

        //if (moltenIronPrefab != null) Instantiate(moltenIronPrefab, transform.position + Vector3.up * 0.2f, Quaternion.identity);
        moltenIronPrefab.SetActive(true);
        Debug.Log(" Thermite Reaction Triggered!");

        //ApplyExplosionForce();
    }


    private void ApplyExplosionForce()
    {
        float explosionRadius = 5f;
        float explosionForce = 700f;
        Vector3 explosionPosition = transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 1.0f, ForceMode.Impulse);
            }
        }
        //moltenIronPrefab.SetActive(true);
    }
}
