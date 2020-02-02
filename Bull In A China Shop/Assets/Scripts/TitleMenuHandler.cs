using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuHandler : MonoBehaviour
{
    [SerializeField] private Scene nextScene = new Scene();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProgressToGameplay()
    {
        Debug.Log("BUTTON PRESSED SUCCESSFULLY");
        SceneManager.LoadScene(nextScene.name);
    }
}
