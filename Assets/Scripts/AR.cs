using System.Collections;

using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AR : MonoBehaviour
{
    [SerializeField] ARSession _session;

    IEnumerator Start()
    {
        if (ARSession.state is ARSessionState.None or ARSessionState.CheckingAvailability)
        {
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported)
        {
            // Start some fallback experience for unsupported devices
        }
        else
        {
            // Start the AR session
            _session.enabled = true;
        }
    }
}
