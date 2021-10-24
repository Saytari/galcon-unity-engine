using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Models;
using Models.Enumerations;

public class ThemeScript : OptionScript
{
    // theme planet type
    public PlanetType planetType;

    // theme ship type
    public ShipType shipType;

    void Update()
    {
        if(Settings.customPlanetType == planetType)
            fadeIn();
    }

    protected override void change()
    {
        // change game settings planet type
        Settings.customPlanetType = planetType;
        // change game settings ship type
        Settings.customShipType = shipType;

        foreach(ThemeScript script in FindObjectsOfType<ThemeScript>())
            script.fadeOut();

        fadeIn();
    }
}
