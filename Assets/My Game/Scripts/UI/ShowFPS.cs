using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    public Text fpsText;

    private float fps;

    void Update()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }
    public void GetFPS()
    {
        fps = (int)(1f/ Time.unscaledDeltaTime);
        fpsText.text = fps + " FPS";
    }
}
