using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeDButton : MonoBehaviour
{
    [SerializeField]
    private Carousel _carousel;

    public void GetAcitveItemIndex()
    {
        Config.PrefabToLoad = _carousel.GetActiveItemName();

        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Snow");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
