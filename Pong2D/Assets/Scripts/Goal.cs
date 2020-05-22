using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool GoalL;

    /// <summary>
    /// If the ball hits a border, add to score
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ball")) {
            if (GoalL) {
                GameObject.FindObjectOfType<GameManager>().ScorePR(); //Add to ScoreR if you hit GoalL
            } else {
                GameObject.FindObjectOfType<GameManager>().ScorePL(); //Add to ScoreL if you hit GoalR
            }
        }
    }
}
