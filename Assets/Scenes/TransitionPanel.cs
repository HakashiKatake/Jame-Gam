using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionPanel : MonoBehaviour
{
    public static TransitionPanel instance;
    private void Awake()
    {
        instance = this;
    }

    Animator animator;
    string scene;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TransitionScene(string sceneName)
    {
        animator.SetTrigger("transition");
        scene = sceneName;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }
}
