using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    [Header("Player L")]
    public Text L_Score;
    public GameObject GoalL;
    public GameObject PaddleL;

    [Header("Player R")]
    public Text R_Score;
    public GameObject GoalR;
    public GameObject PaddleR;

    [Header("Text")]
    public Text mainText;
    public Text subText;

    [Header("AudioSource")]
    public AudioSource beep;
    public AudioSource goal;

    private int LScore;
    private int RScore;

    // Start is called before the first frame update
    void Start() {
        LScore = 0;
        RScore = 0;
        mainText.color = Color.white;
        subText.color = Color.white;
        subText.text = "[SPACE] TO START";
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            mainText.color = Color.white; //Change color
            mainText.text = " "; //Main text is blank
            subText.text = " "; //Subtext is blank
            subText.color = Color.white; //Change color
            StartCoroutine(CountDown());
        }
    }

    /// <summary>
    /// Game countdown
    /// </summary>
    /// <returns></returns>
    IEnumerator CountDown() {
        ResetPositions();
        mainText.text = "3"; //Change text
        beep.Play(); //play beep audio
        yield return new WaitForSeconds(1f); //Wait for one second
        mainText.text = "2";
        beep.Play();
        yield return new WaitForSeconds(1f);
        mainText.text = "1";
        beep.Play();
        yield return new WaitForSeconds(1f);
        mainText.text = " ";
        ball.GetComponent<BallScript>().RandomDirection(); //Send ball in random direction
    }

    /// <summary>
    /// Add to Paddle L's score
    /// </summary>
    public void ScorePL()
    {
        goal.Play(); //play goal audio
        LScore++; //Add to score
        L_Score.text = LScore.ToString(); //Change text
        StartCoroutine(CountDown()); //Restart countdown
    }

    /// <summary>
    /// Add to Paddle R's score
    /// </summary>
    public void ScorePR() {
        goal.Play(); //play goal audio
        RScore++; //Add to score
        R_Score.text = RScore.ToString(); //Change text
        StartCoroutine(CountDown()); //Restart countdown
    }

    /// <summary>
    /// Reset game to original positions and restart countdown
    /// </summary>
    public void ResetPositions() {
        ball.GetComponent<BallScript>().ResetBall(); //Reset ball position
        PaddleL.GetComponent<PlayerScript>().ResetPaddle(); //Reset paddleL position
        PaddleR.GetComponent<PlayerScript>().ResetPaddle(); //Reset paddleR position
    }
}
