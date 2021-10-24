using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Controllers.Enumerations;
using Models;

public class EndController : MonoBehaviour
{
    // score layout
    ScoreScreen scoreScreen;
    
    void Start()
    {
        // get screen canvas script
        scoreScreen = FindObjectOfType<ScoreScreen>();
    }

    public void scane(PlayerModel[] players)
    {
        // get human player model
        HumanPlayerModel humanPlayer = FindObjectOfType<HumanPlayerModel>();

        // number of lost players
        List<PlayerModel> activePlayers = new List<PlayerModel>();

        // filter players that still able to play
        foreach(PlayerModel player in players)
            if(! isLost(player))
                activePlayers.Add(player);

        // Check if human player stills playing
        if(! activePlayers.Contains(humanPlayer))
            scoreScreen.fadeIn(GameState.Lost, humanPlayer);

        // Check if human player is the only player
        else if(activePlayers.Count == 1)
            scoreScreen.fadeIn(GameState.Won, humanPlayer);
    }

    /**
     * Check if player has lost
     * 
     * @return bool
     */
    protected bool isLost(PlayerModel player)
    {
        if(! player.hasPlanets() && ! player.hasFlocks())
            return true;
        return false;
    }
}

