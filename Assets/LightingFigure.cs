using System.Collections.Generic;

using UnityEngine;

public class LightingFigure : MonoBehaviour
{
    private const string LightingFigureTag = "Figure";
    private const string EmissionKeyword = "_EMISSION";

    private bool _isEmissionEnabled;
    private IEnumerable<Material> _materials;

    void Awake()
    {
        SetMaterials();

        DisableEmission();
    }

    public void EnableEmission()
    {
        foreach (var material in _materials)
        {
            material.EnableKeyword(EmissionKeyword);
        }

        _isEmissionEnabled = true;
    }

    public void DisableEmission()
    {
        foreach (var material in _materials)
        {
            material.DisableKeyword(EmissionKeyword);
        }

        _isEmissionEnabled = false;
    }

    public void SwitchEmission()
    {
        if (_isEmissionEnabled)
        {
            DisableEmission();
        }
        else
        {
            EnableEmission();
        }
    }

    private void SetMaterials()
    {
        var figures = GameObject.FindGameObjectsWithTag(LightingFigureTag);

        _materials = GetMaterials(figures);
    }

    private IEnumerable<Material> GetMaterials(IEnumerable<GameObject> gameObjects)
    {
        List<Material> materials = new();

        foreach (var gameObject in gameObjects)
        {
            if (gameObject.TryGetComponent<MeshRenderer>(out var rendeder))
            {
                materials.AddRange(rendeder.materials);
            }
        }

        return materials;
    }
}
