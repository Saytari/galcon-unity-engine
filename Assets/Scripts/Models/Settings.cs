using System;
using System.Collections;
using System.Collections.Generic;

using Models.Enumerations;

public class Settings
{
    // human player custom planet type
    public static PlanetType customPlanetType = PlanetType.RedOctopus;

    // human player custom ship type;
    public static ShipType customShipType = ShipType.Venus;

    // all planets type
    public static PlanetType[] planetsTypes =  Enum.GetValues(typeof(PlanetType)) as PlanetType[];

    // bot player counts
    public static int botsCount = 3;

    // bot brain type
    public static BrainType brainType = BrainType.Simple;

}
