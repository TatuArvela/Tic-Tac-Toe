using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private GameObject[] boxes = new GameObject[9];
    private Text[] boxTexts = new Text[9];
    private GameObject X;
    private GameObject O;
    private GameObject ScoreX;
    private GameObject ScoreO;
    private GameObject ClickToStart;

    private bool initialized = false;
    private bool willRestart = false;
    private string currentCharacter;
    private int scoreX = 0;
    private int scoreO = 0;
    private int step = 0;

    // Use this for initialization
    void Start() {
        X = GameObject.Find("X");
        O = GameObject.Find("O");
        ScoreX = GameObject.Find("ScoreX");
        ScoreO = GameObject.Find("ScoreO");
        ClickToStart = GameObject.Find("ClickToStart");

        X.SetActive(false);
        O.SetActive(false);
        ScoreX.SetActive(false);
        ScoreO.SetActive(false);
    }

    void Update() {
        if (Input.GetMouseButton(0) && !initialized)
            Initialize();

        if (Input.GetMouseButton(0) && willRestart)
            RestartGame();
    }

    void Initialize() {
        X.SetActive(true);
        O.SetActive(true);
        ScoreX.SetActive(true);
        ScoreO.SetActive(true);
        ClickToStart.SetActive(false);

        for (int i = 0; i < boxes.Length; i++) {
            initialized = true;

            // Mapping boxes
            boxes[i] = GameObject.Find("Box" + (i + 1));
            boxTexts[i] = boxes[i].transform.GetChild(0).GetComponent<Text>();

            // Clearing boxes
            boxTexts[i].text = "";

            // Adding onClick listeners to boxes. 
            // For some reason, the index needs to be set as a new variable inside the for loop
            int index = i;
            boxes[i].GetComponent<Button>().onClick.AddListener(() => {
                ClickOnBox(index);
            });
        }

        NextPlayer();
    }

    void ClickOnBox(int i) {
        // The text object to use
        Text text = boxTexts[i];

        // If box is empty, put the current character in it
        if (text.text == "") {
            text.text = currentCharacter;
            GameStep();
        }
    }

    void GameStep() {
        step++;
        CheckVictoryCondition();
        NextPlayer();
        Debug.Log(step.ToString() + " " + willRestart.ToString());
    }

    void NextPlayer() {
        if (currentCharacter == "O") {
            ScoreO.GetComponent<Text>().fontSize = 24;
            O.GetComponent<Text>().fontSize = 24;
            ScoreX.GetComponent<Text>().fontSize = 36;
            X.GetComponent<Text>().fontSize = 36;
            currentCharacter = "X";
        } else {
            ScoreX.GetComponent<Text>().fontSize = 24;
            X.GetComponent<Text>().fontSize = 24;
            ScoreO.GetComponent<Text>().fontSize = 36;
            O.GetComponent<Text>().fontSize = 36;
            currentCharacter = "O";
        }
    }

    void CheckVictoryCondition() {
        if (boxTexts[0].text == currentCharacter && boxTexts[1].text == currentCharacter && boxTexts[2].text == currentCharacter) {
            FinishedGame();
        }

        if (boxTexts[3].text == currentCharacter && boxTexts[4].text == currentCharacter && boxTexts[5].text == currentCharacter) {
            FinishedGame();
        }

        if (boxTexts[6].text == currentCharacter && boxTexts[7].text == currentCharacter && boxTexts[8].text == currentCharacter) {
            FinishedGame();
        }

        if (boxTexts[0].text == currentCharacter && boxTexts[3].text == currentCharacter && boxTexts[6].text == currentCharacter) {
            FinishedGame();
        }

        if (boxTexts[1].text == currentCharacter && boxTexts[4].text == currentCharacter && boxTexts[7].text == currentCharacter) {
            FinishedGame();
        }

        if (boxTexts[2].text == currentCharacter && boxTexts[5].text == currentCharacter && boxTexts[8].text == currentCharacter) {
            FinishedGame();
        }

        if (boxTexts[0].text == currentCharacter && boxTexts[4].text == currentCharacter && boxTexts[8].text == currentCharacter) {
            FinishedGame();
        }

        if (boxTexts[2].text == currentCharacter && boxTexts[4].text == currentCharacter && boxTexts[6].text == currentCharacter) {
            FinishedGame();
        }

        if (step == 9 && !willRestart) {
            FinishedGame(true);
        }
    }

    void FinishedGame(bool tie = false) {
        ClickToStart.SetActive(true);
        willRestart = true;

        if (currentCharacter == "X" && !tie) {
            scoreX++;
            ScoreX.GetComponent<Text>().text = scoreX.ToString();
        } else if (!tie) {
            scoreO++;
            ScoreO.GetComponent<Text>().text = scoreO.ToString();
        }
    }

    void RestartGame() {
        ClickToStart.SetActive(false);
        willRestart = false;
        step = 0;

        for (int i = 0; i < boxes.Length; i++) {
            boxes[i].transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

}
