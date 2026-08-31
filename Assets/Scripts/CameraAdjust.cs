using UnityEngine;

public class CameraAdjust : MonoBehaviour
{
    [SerializeField] private float std_width;
    [SerializeField] private float std_height;
    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        float screen_aspect = Screen.width / (float)Screen.height;
        float std_aspect = std_width / std_height;
        float rect_value = std_aspect/screen_aspect;

        Rect rect = new Rect(0,0,1,1);
        if(rect_value > 1)
        {
            rect.height = 1 / rect_value;
            rect.y = (rect_value - 1) / (2 * rect_value);
        }
        else if(rect_value < 1)
        {
            rect.width = rect_value;
            rect.x = (1 - rect_value) / 2;
        }
        cam.rect = rect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

