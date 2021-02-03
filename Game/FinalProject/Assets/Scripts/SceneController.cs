using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    #region Singleton
    public static SceneController instance=null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    #endregion
    
    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadScene(1);
        }
    }
}
