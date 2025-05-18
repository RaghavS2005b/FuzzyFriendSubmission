using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomizeUI : MonoBehaviour
{
    [SerializeField] private RawImage  characterDisplay;
    [SerializeField] private Button  nextButton;
    [SerializeField] private Texture2D[] portraits;
    [SerializeField] private GameObject[] petPrefabs;

    private int _currentIndex = 0;

    void Start()
    {
        if (portraits.Length != petPrefabs.Length)
            Debug.LogError("Portraits and Prefabs arrays must be the same length!");
        characterDisplay.texture = portraits[0];
        nextButton.onClick.AddListener(() => Debug.Log("🐞 [DEBUG] Next button was actually clicked!"));
        nextButton.onClick.AddListener(OnNextButtonPressed);
        Debug.Log($"nextButton = {nextButton.name}");
    }
    public void OnSelectPortrait(int index)
    {
        _currentIndex = index;
        characterDisplay.texture = portraits[index];
    }

    public void OnNextButtonPressed()
    {
        Debug.Log("Next button pressed, loading LLM scene…");
        if (petPrefabs == null || _currentIndex < 0 || _currentIndex >= petPrefabs.Length)
    {
        Debug.LogError($"Cannot select pet #{_currentIndex}: array length = {petPrefabs?.Length}");
        return;
    }
        PetSelectionManager.SelectedPetPrefab = petPrefabs[_currentIndex];
        SceneManager.LoadScene("LLMForUnity");
        Debug.Log("scene switch");
    }
}
