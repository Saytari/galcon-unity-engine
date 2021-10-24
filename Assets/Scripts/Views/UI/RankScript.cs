using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Models;
using Models.Enumerations;

public class RankScript : OptionScript
{
    // game bot brain type
    public BrainType brainType;

    void Update()
    {
        if(Settings.brainType == brainType)
            fadeIn();
    }

    protected override void change()
    {
        // change game settings planet type
        Settings.brainType = brainType;

        foreach(RankScript script in FindObjectsOfType<RankScript>())
            script.fadeOut();

        fadeIn();
    }
}
