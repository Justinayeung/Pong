using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public AudioSource bg;
    public AudioSource victoryMusic;

    [Header("Color")]
    public Color yellow;
    public Color purple;

    public bool Won;
    public bool over2;
    public bool once;
    public bool victory;
    public bool played;

    private int LScore;
    private int RScore;

    // Start is called before the first frame update
    void Start() {
        Cursor.visible = false;
        Won = false;
        over2 = false;
        once = false;
        victory = false;
        played = false;
        LScore = 0;
        RScore = 0;
        mainText.color = Color.white;
        subText.color = Color.white;
        subText.text = "SPACE TO START";
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)) { //Quit Application
            Application.Quit();
        }

        if (!once) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                mainText.color = Color.white; //Change color
                mainText.text = " "; //Main text is blank
                subText.text = " "; //Subtext is blank
                subText.color = Color.white; //Change color
                once = true; // once is true
                bg.Play(); //Play background music
                StartCoroutine(CountDown());
            }
        }

        if (Won) {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Restart scene
            }
        }

        WinCondition();

        //Victory music condition (Play audio once)
        if (victory && !played) {
            victoryMusic.Play();
            victory = false;
            played = true;
        }
    }

    /// <summary>
    /// Game countdown
    /// </summary>
    /// <returns></returns>
    IEnumerator CountDown() {
        if (!over2) { //If win condition is not met
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

    public void WinCondition() {
        if (LScore >= 2) { //Checking if score is greater than 2
            over2 = true; //Set win to true
        } else if (RScore >= 2) {
            over2 = true; //Set win to true
        }

        if (LScore >= 3) { //Win Text
            Won = true;
            mainText.text = "YELLOW WINS"; //Change text
            mainText.color = yellow; //Change text color
            subText.text = "SHIFT TO RESTART"; //Change subtext
            subText.color = yellow; //Change subtext color
            bg.Stop(); //Stop background music
            victory = true;
        } else if (RScore >= 3) {
            Won = true;
            mainText.text = "PURPLE WINS"; //Change text
            mainText.color = purple; //Change text color
            subText.text = "SHIFT TO RESTART"; // Change subtext
            subText.color = purple; //Change subtext color
            bg.Stop(); //Stop background music   
            victory = true;
        }
    }
}
