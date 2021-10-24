﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(mainMenu);
    }

    
    void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
