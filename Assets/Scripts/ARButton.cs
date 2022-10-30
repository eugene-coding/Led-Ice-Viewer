using System.Collections;

using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARButton : MonoBehaviour
{
    [SerializeField] ARSession m_Session;

    IEnumerator Start()
    {
        if (ARSession.state is ARSessionState.None or ARSessionState.CheckingAvailability)
        {
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state is ARSessionState.Unsupported)
        {
            gameObject.SetActive(false);

            Debug.Log("Not supported");
        }
    }
}
