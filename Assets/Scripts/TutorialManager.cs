using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private Spawner spawner;
    private ScoreManager scoreManager;
    private GameControl gameControl;
    public GameObject[] instructions;
    private int instructionIndex = 0;
    public Character character;
    private bool tutorialFinished;
    

    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<Spawner>();
        spawner.enabled = false;
        scoreManager = GetComponent<ScoreManager>();
        scoreManager.enabled = false;
        gameControl = GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorialFinished)
        {
            gameControl.BackgroundScrollSpeed = gameControl.initialBackgroundScrollSpeed;
            gameControl.NextBackgroundScrollSpeed = gameControl.initialBackgroundScrollSpeed;
            for (int i = 0; i < instructions.Length; i++)
            {
                if (i == instructionIndex)
                {
                    instructions[i].SetActive(true);
                }
                else
                {
                    instructions[i].SetActive(false);
                }
            }

            if (instructionIndex == 0)
            {
                if (character.IsJumping)
                {
                    instructionIndex++;
                }
            }
            else if(instructionIndex == 1)
            {
                if (character.IsDashing)
                {
                    instructionIndex++;
                }
            }
            else if (instructionIndex == 2)
            {
                if (character.IsDownDashing)
                {
                    spawner.enabled = true;
                    scoreManager.enabled = true;
                    instructions[instructionIndex].SetActive(false);
                    tutorialFinished = true;
                    this.enabled = false;
                }
            }
        }
    }


}
