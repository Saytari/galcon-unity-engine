using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Menu);
    }


    void Menu()
    {
        
        SceneManager.LoadScene("Battle" + Random.Range(1, SceneManager.sceneCountInBuildSettings - 2));
    }
}
