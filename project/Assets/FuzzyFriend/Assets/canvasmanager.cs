using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

using UnityEngine.UI;


public class canvasmanager : MonoBehaviour
{
   


    public GameObject openingPage;
    public GameObject namingPage;
    public GameObject Show_CustomizeP;
    public GameObject showTutorialP;
    public GameObject Home;
    public GameObject MiniGame1;

    public float fadeDuration = 0.5f;

    GameObject currentPage;

    void Start()
    {
        currentPage = openingPage;
        SetPageInstant(openingPage);
        Debug.Log("we are in opening page ");
    }

    void SetPageInstant(GameObject page)
    {
        openingPage.SetActive(false);
        namingPage.SetActive(false);
        Show_CustomizeP.SetActive(false);
        showTutorialP.SetActive(false);
        Home.SetActive(false);
        MiniGame1.SetActive(false);

        page.SetActive(true);
        SetCanvasGroupAlpha(page, 1f);
    }

    public void ShowNamingP() => StartCoroutine(TransitionTo(namingPage));
    public void ShowCustomize() => StartCoroutine(TransitionTo(Show_CustomizeP));
    public void showTutorial() => StartCoroutine(TransitionTo(showTutorialP));
    public void ShowHP() => StartCoroutine(TransitionTo(Home));
    public void ShowMiniGame ()=> StartCoroutine(TransitionTo(MiniGame1));

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
}

