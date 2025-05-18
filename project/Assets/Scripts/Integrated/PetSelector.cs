using UnityEngine;
using UnityEngine.UI;

public class PetSelector : MonoBehaviour
{
    [SerializeField] private Button[]  petButtons;
    [SerializeField] private GameObject[] petPrefabs;
    [SerializeField] private Transform  spawnPoint;
    [SerializeField] private Transcript transcript;
    [SerializeField] private JetsIntegrated jets;

    private GameObject currentPet;

    void Start()
    {
        if (petButtons.Length != petPrefabs.Length)
            Debug.LogError("More pets than buttons");
        for (int i = 0; i < petButtons.Length; i++)
        {
            int index = i; 
            petButtons[i].onClick.AddListener(() => SelectPet(index));
        }
    }

    public void SelectPet(int index)
    {
        if (currentPet != null)
            Destroy(currentPet);

        currentPet = Instantiate(
            petPrefabs[index],
            spawnPoint.position,
            spawnPoint.rotation
        );

        var anim = currentPet.GetComponent<DogAnimator>();
        if (anim != null)
        {
            anim.Configure(transcript, jets);
        }
        else
        {
            Debug.LogWarning("animator missing");
        }
    }
}
