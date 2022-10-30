using Cinemachine;

using System.Collections.Generic;

using UnityEngine;

public class Carousel : MonoBehaviour
{
    private const string CameraName = "Camera";

    private LinkedList<CinemachineVirtualCameraBase> _cameras;
    private LinkedListNode<CinemachineVirtualCameraBase> _activeCamera;

    public List<GameObject> Items;
    public float BlendDuration = 1.2f;
    public float CameraDistance = 14f;
    public float RightPadding = 15f;

    private void Awake()
    {
        _cameras = new();

        for (var index = 0; index < Items.Count; index++)
        {
            GameObject item = Items[index];

            var parentFolder = new GameObject(item.name).transform;

            if (!item.activeInHierarchy)
            {
                Instantiate(item, Vector3.zero, Quaternion.identity, parentFolder);
            }
            else
            {
                item.transform.SetParent(parentFolder);
            }

            item.transform.position = Vector3.zero;

            var modelCenter = GetBounds(item).center;

            var camera = CreateCamera(parentFolder, modelCenter);

            _cameras.AddLast(camera);

            parentFolder.SetParent(gameObject.transform);

            parentFolder.position += index * RightPadding * Vector3.right;
        }

        _activeCamera = _cameras.First;

        SetBlendDuration();
    }

    private void Start()
    {
        _activeCamera.Value.Priority++;
    }

    public void Next()
    {
        var cameraToSwitch = _activeCamera.Next ?? _cameras.First;

        SwitchCamera(cameraToSwitch);
    }

    public void Previous()
    {
        var cameraToSwitch = _activeCamera.Previous ?? _cameras.Last;

        SwitchCamera(cameraToSwitch);
    }

    public int GetActiveItemIndex()
    {
        var index = -1;

        foreach (var camera in _cameras)
        {
            index++;

            if (camera == _activeCamera.Value)
            {
                return index;
            }
        }

        return -1;
    }

    public string GetActiveItemName()
    {
        var index = GetActiveItemIndex();
        
        return Items[index].name;
    }

    private void SwitchCamera(LinkedListNode<CinemachineVirtualCameraBase> cameraToSwitch)
    {
        _activeCamera.Value.Priority--;

        _activeCamera = cameraToSwitch;

        _activeCamera.Value.Priority++;
    }

    private void SetBlendDuration()
    {
        var camera = Camera.main;

        if (camera == null)
        {
            return;
        }

        if (camera.TryGetComponent<CinemachineBrain>(out var brain))
        {
            var style = brain.m_DefaultBlend.m_Style;

            CinemachineBlendDefinition blend = new(style, BlendDuration);
            brain.m_DefaultBlend = blend;
        }
    }

    private CinemachineVirtualCameraBase CreateCamera(Transform parent, Vector3 targetCenter)
    {
        GameObject camera = new(CameraName);
        Transform transform = camera.transform;

        Vector3 position = new(targetCenter.x, targetCenter.y, CameraDistance);
        Quaternion rotation = new(0f, -180f, 0f, 0f);

        transform.SetParent(parent);
        transform.SetPositionAndRotation(position, rotation);

        var cameraComponent = camera.AddComponent<CinemachineVirtualCamera>();

        return cameraComponent;
    }

    private Bounds GetBounds(GameObject objeto)
    {
        Bounds bounds;
        Renderer childRender;

        bounds = GetRenderBounds(objeto);

        if (bounds.extents.x == 0)
        {
            bounds = new Bounds(objeto.transform.position, Vector3.zero);

            foreach (Transform child in objeto.transform)
            {
                childRender = child.GetComponent<Renderer>();

                if (childRender)
                {
                    bounds.Encapsulate(childRender.bounds);
                }
                else
                {
                    bounds.Encapsulate(GetBounds(child.gameObject));
                }
            }
        }

        return bounds;
    }

    private Bounds GetRenderBounds(GameObject objeto)
    {
        Bounds bounds = new(Vector3.zero, Vector3.zero);

        if (objeto.TryGetComponent<Renderer>(out var render))
        {
            return render.bounds;
        }

        return bounds;
    }
}
