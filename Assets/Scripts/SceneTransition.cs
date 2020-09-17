using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : Singleton<SceneTransition>
{
    [SerializeField] UnityEngine.UI.Image fader;
    public float transitionTime = 2.0f;
    private bool fade = false;
    private bool transition = false;
    private float progress = 0.0f;
    private string scene;

    void Update()
    {
        if (fade)
        {
            Color color = new Color(0, 0, 0, 0);
            progress += Time.deltaTime;

            color.a = Mathf.PingPong(progress, transitionTime * 0.5f);
            fader.color = color;

            if (transition && progress >= transitionTime * 0.5f)
            {
                transition = false;
                DontDestroyOnLoad(gameObject);
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }

            if (progress >= transitionTime)
            {
                progress = 0.0f;
                fade = false;
            }
        } 
    }

    public void TransitionScene(string scene)
    {
        this.scene = scene;
        transition = true;
        fade = true;
    }
}
