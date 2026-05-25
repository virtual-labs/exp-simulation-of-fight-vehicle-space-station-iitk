using UnityEngine;

public class VolcanoReaction3D : MonoBehaviour
{
    [SerializeField] private Transform reactionSpawnPoint;
    [SerializeField] private GameObject foamEffectPrefab;

    private bool hasSoda;
    private bool hasVinegar;
    private bool hasReacted;
    public GameObject bubbleBlast, cap1, cap2;

    private void OnTriggerEnter(Collider other)
    {
        if (hasReacted) return;

        if (other.CompareTag("BakingSoda"))
        {
            cap1.SetActive(false);
            hasSoda = true;
        }
            

        if (other.CompareTag("Vinegar"))
        {
            cap2.SetActive(false);
            hasVinegar = true;
        }
            

        if (hasSoda && hasVinegar)
            React();
    }

    private void React()
    {
        hasReacted = true;

        //Instantiate(foamEffectPrefab, reactionSpawnPoint.position, Quaternion.identity);
        bubbleBlast.SetActive(true);
        bubbleBlast.GetComponent<ParticleSystem>().Play();

        Debug.Log("Volcano Eruption Triggered!");
    }
}
