using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Models;
using Models.Enumerations;

public class BotCountScript : OptionScript
{
    // game bot brain type
    public int count = 1;

    void Update()
    {
        if(Settings.botsCount == count)
            fadeIn();
    }

    protected override void change()
    {
        // change game settings planet type
        Settings.botsCount = count;

        foreach(BotCountScript script in FindObjectsOfType<BotCountScript>())
            script.fadeOut();

        fadeIn();
    }
}
