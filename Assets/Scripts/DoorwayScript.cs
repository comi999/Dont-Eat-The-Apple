using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorwayScript : MonoBehaviour
{
    public string scene;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            SceneTransition.Instance.TransitionScene(scene);
            collider.gameObject.transform.position = new Vector3(0, 2, 0);
        }
    }
}
