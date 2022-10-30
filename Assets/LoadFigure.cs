using UnityEngine;

public class LoadFigure : MonoBehaviour
{
    [SerializeField]
    private Transform _figurePlacing;

    void Start()
    {
        var figure = (GameObject) Instantiate(
            Resources.Load(Config.PrefabToLoad));

        figure.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        figure.transform.SetParent(_figurePlacing, false);
    }
}
