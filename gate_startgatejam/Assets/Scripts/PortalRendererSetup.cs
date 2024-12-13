using UnityEngine;

public class PortalRendererSetup : MonoBehaviour
{
    public Camera cameraShort;
    public Material cameraMatShort;

    void Start()
    {
        if (cameraShort.targetTexture != null)
        {
            cameraShort.targetTexture.Release();
        }

        cameraShort.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatShort.mainTexture = cameraShort.targetTexture;
    }
}
