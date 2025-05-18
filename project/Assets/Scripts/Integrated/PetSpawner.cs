
using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    [SerializeField] private Transcript transcript;
    [SerializeField] private JetsIntegrated jets;

    void Start()
    {
        var prefab = PetSelectionManager.SelectedPetPrefab;
        if (prefab == null)
        {
            Debug.LogError("No pet selected; please go back to Customize.");
            return;
        }

        var pet = Instantiate(prefab, transform.position, transform.rotation);
        var animator = pet.GetComponent<DogAnimator>();
        if (animator != null)
            animator.Configure(transcript, jets);
    }
}
