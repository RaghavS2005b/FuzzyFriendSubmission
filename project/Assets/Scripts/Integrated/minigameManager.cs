using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class minigameManager : MonoBehaviour
{

    public GameObject Home;
    public GameObject MiniGame1;
       public GameObject GoodEnding;
   public GameObject BadEnding;

    public float fadeDuration = 0.5f;

    

    GameObject currentPage;

    void Start()
    {
        currentPage = Home;
        SetPageInstant(Home);
        //Debug.Log("we are in opening page ");
    }

    void SetPageInstant(GameObject page)
    {
        Home.SetActive(true);
        MiniGame1.SetActive(false);
        
     GoodEnding.SetActive(false);
     BadEnding.SetActive(false);

        page.SetActive(true);
        SetCanvasGroupAlpha(page, 1f);
    }

    public void ShowHP() => StartCoroutine(TransitionTo(Home));
    public void ShowMiniGame ()=> StartCoroutine(TransitionTo(MiniGame1));
    
        //public void ShowStart() => StartCoroutine(TransitionTo(Home));

public void ShowGoodEnding() => StartCoroutine(TransitionTo(GoodEnding));
    public void ShowBadEnding() => StartCoroutine(TransitionTo(BadEnding));

    IEnumerator TransitionTo(GameObject nextPage)
    {
        if (currentPage == nextPage) yield break;

        CanvasGroup currentGroup = currentPage.GetComponent<CanvasGroup>();
        CanvasGroup nextGroup = nextPage.GetComponent<CanvasGroup>();

        nextPage.SetActive(true);
        nextGroup.alpha = 0f;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalized = t / fadeDuration;
            currentGroup.alpha = 1f - normalized;
            nextGroup.alpha = normalized;
            yield return null;
        }

        currentGroup.alpha = 0f;
        currentPage.SetActive(false);
        nextGroup.alpha = 1f;

        currentPage = nextPage;
    }

    void SetCanvasGroupAlpha(GameObject obj, float alpha)
    {
        var cg = obj.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.alpha = alpha;
        }
    }

    public void returnHP(){
        SceneManager.LoadScene("MainScene");
    }
}

