using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float mouseSensitivity = 10;
    float yaw;
    float pitch;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float hRot =  Input.GetAxis("Mouse X") * mouseSensitivity;
        float vRot = -Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw   += hRot;
        pitch = Mathf.Clamp(pitch += vRot, -90, 90);

        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        transform.parent.rotation = Quaternion.Euler(0, yaw, 0);

        if (Input.GetKey(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
