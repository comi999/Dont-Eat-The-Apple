using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorwayScript : MonoBehaviour
{
    //public string scene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        GameObject persistentObject;

        if (collider.gameObject.name == "Player")
        {
            persistentObject = GameObject.Find("Persistant Objects");
            //persistentObject.GetComponent<SceneTransition>().TransitionScene(scene);
        }
    }
}
