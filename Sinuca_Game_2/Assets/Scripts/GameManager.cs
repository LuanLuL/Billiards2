using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum CurrentPlayer
    {
        Player1,
        Player2
    }

    CurrentPlayer currentPlayer;
    bool isWinningShotForplayer1 = false;
    bool isWinningShotForplayer2 = false;
    int player1BallsRemaining = 7;
    int player2BallsRemaining = 7;
    bool isWaitingForBallMovementToStop = false;
    bool willSwapPlayers = false;
    bool isGameOver = false;
    [SerializeField] float shotTimer = 3f;
    private float currentTimer;
    [SerializeField] float movementThreshold;

    [SerializeField] TextMeshProUGUI player1BallsText;
    [SerializeField] TextMeshProUGUI player2BallsText;
    [SerializeField] TextMeshProUGUI currentTurnText;
    [SerializeField] TextMeshProUGUI messageText;


    [SerializeField] GameObject restartButtom;

    [SerializeField] Transform headPosition;

    [SerializeField] Camera cueStickCamera;
    [SerializeField] Camera overheadCamera;
    Camera currentCamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPlayer = CurrentPlayer.Player1;
        currentCamera = cueStickCamera;
        currentTimer = shotTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaitingForBallMovementToStop && !isGameOver)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer > 0)
            {
                return;
            }

            bool allStopped = true;
            foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
            {
                if (ball.GetComponent<Rigidbody>().velocity.magnitude >= movementThreshold)
                {
                    allStopped = false;
                    break;
                }
            }
            if (allStopped)
            {
                isWaitingForBallMovementToStop = false;
                if (willSwapPlayers)
                {
                    NextPlayerTurn();
                }
                else
                {
                    SwitchCameras();
                }
                currentTimer = shotTimer;
            }
        }
    }

    public void SwitchCameras(){
        if (currentCamera == cueStickCamera){
            cueStickCamera.enabled = false;
            overheadCamera.enabled = true;
            currentCamera = overheadCamera;
            isWaitingForBallMovementToStop = true;
        }
        else {
             cueStickCamera.enabled = true;
            overheadCamera.enabled = false;
            currentCamera = cueStickCamera;
            currentCamera.gameObject.GetComponent<CameraController>().ResetCamera();
        }
    }

    bool Scratch()
    {
        if (currentPlayer == CurrentPlayer.Player1)
        {
            if (isWinningShotForplayer1)
            {
                ScratchOnWinningShot("Player 1");
                return true;
            }
               
        }
        else
        {
            if (isWinningShotForplayer2)
            {
                ScratchOnWinningShot("Player 2");
                return true;
            }
        }
        willSwapPlayers = true;
        return false;
    }

    void EarlyEightBall()
    {
        if (currentPlayer == CurrentPlayer.Player1)
        {
            Lose("Jogador 1 caçapou a bola oito muito cedo e perdeu!");
        }
        else
        {
            Lose("Jogador 2 caçapou a bola oito muito cedo e perdeu!");
        }
    }
    void ScratchOnWinningShot(string player)
    {
        Lose(player + " errou na sua tacada final e perdeu!");
    }

    public void RestartTheGame()
    {
        SceneManager.LoadScene(0);
    }  

    bool CheckBall(Ball ball)
    {
        if (ball.IsCueBall())
        {
            if (Scratch())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (ball.IsEightBall())
        {
            if (currentPlayer == CurrentPlayer.Player1)
            {
                if (isWinningShotForplayer1)
                {
                    Win("Jogador 1");
                    return true;
                }
            }
            else
            {
                if (isWinningShotForplayer2)
                {
                    Win("Jogador 2");
                    return true;
                }
            }
            EarlyEightBall();
        }
        else 
        {
            if (ball.isBallRed())
            {
                player1BallsRemaining--;
                player1BallsText.text = "Jogador 1: " + player1BallsRemaining + " bolas restantes";
                if(player1BallsRemaining <= 0)
                {
                    isWinningShotForplayer1 = true;
                }
                if(currentPlayer != CurrentPlayer.Player1)
                {
                  willSwapPlayers = true;
                }
            }
            else
            {
                player2BallsRemaining--;
                player2BallsText.text = "Jogador 2: " + player2BallsRemaining + " bolas restantes";
                if (player2BallsRemaining <= 0)
                {
                    isWinningShotForplayer2 = true;
                }
                if (currentPlayer != CurrentPlayer.Player2)
                {
                   willSwapPlayers = true;

                }

            }
        }
        return true;
    }

    void Lose(string message)
    {
        isGameOver = true;
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        restartButtom.SetActive(true);
    }
    void Win(string player)
    {
        isGameOver = true;
        messageText.gameObject.SetActive(true);
        messageText.text = player + " venceu!";
        restartButtom.SetActive(true);
    }

    void NextPlayerTurn()
    {
        if (currentPlayer == CurrentPlayer.Player1)
        {
            currentPlayer = CurrentPlayer.Player2;
            currentTurnText.text = "Vez do Jogador 2";
        }
        else
        {
            currentPlayer = CurrentPlayer.Player1;
            currentTurnText.text = "Vez do Jogador 1";
        }
        willSwapPlayers = false;
        SwitchCameras();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (CheckBall(other.gameObject.GetComponent<Ball>()))
            {
                Destroy(other.gameObject);
            }
            else
            {
                other.gameObject.transform.position = headPosition.position;
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
    }
}
