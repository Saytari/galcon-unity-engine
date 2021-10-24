using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Controllers.Enumerations;
using Models;

public class ScoreScreen : MonoBehaviour
{
    // points
    public int planetPoints = 100;
    public int flockPoints = 10;
    public int shipPoints = 1;

    // screen won headline
    public string wonStatement = "Game Won";

    // screen won headline
    public string lostStatement = "Game Lost";

    // screen headline
    public Text headline;

    // total conquerd planet text
    public Text totalPlanets;

    // total released flocks
    public Text totalFlocks;

    // total released ships
    public Text totalShips;
    
    // total score
    public Text score;
    
    // current counters value
    protected int currentPlanets = 0;
    protected int currentFlocks = 0;
    protected int currentShips = 0;
    protected int currentScore = 0;

    // boolean for the counters if it should start
    protected bool start = false;

    // screen animator;
    Animator animator;

    // the winner
    protected PlayerModel player;

    // player score after calculation
    protected int playerScore = 0;

    void Awake()
    {
        // get the animator component
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
            increaseCounters();
    }

    public void fadeIn(GameState gameState, PlayerModel humanPlayer)
    {
        // setup player for increament animation
        this.player = humanPlayer;

        // calculate player score
        playerScore = humanPlayer.getTotalPlanets() * planetPoints
                    + humanPlayer.getTotalFlocks() * flockPoints
                    + humanPlayer.getTotalShips() * shipPoints;

        if(gameState == GameState.Won)
            headline.text = wonStatement;
        else 
            headline.text = lostStatement;

        animator.SetBool("FadeIn", true);

        // start increase the counters
        start = true;
    }

    public void showUp()
    {
        animator.SetBool("ShowUp", true);
    }

    protected void increaseCounters()
    {
        if(currentPlanets + 3 < player.getTotalPlanets())
            currentPlanets += 3;
        else
            currentPlanets = player.getTotalPlanets();
        
        if(currentFlocks + 3 < player.getTotalFlocks())
            currentFlocks += 3;
        else
            currentFlocks = player.getTotalFlocks();

        if(currentShips + 3 < player.getTotalShips())
            currentShips += 3;
        else
            currentShips = player.getTotalShips();

        if(currentScore + 7 < playerScore)
            currentScore += 7;
        else
            currentScore = playerScore;

        changeCounters();
    }

    protected void changeCounters()
    {
        totalPlanets.text = currentPlanets + "";

        totalFlocks.text = currentFlocks + "";

        totalShips.text = currentShips + "";

        score.text = currentScore + "";
    }
}
