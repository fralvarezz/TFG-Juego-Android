using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private TMP_Text signInButtonText;
    public Button signInButton;
    public Text authStatus;
    
    void Start()
    {

        signInButtonText = signInButton.GetComponentInChildren<TMP_Text>();
        
        Debug.Log(signInButtonText.text);
        
        // Create client configuration
        PlayGamesClientConfiguration config = new 
                PlayGamesClientConfiguration.Builder()
            .Build();

        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;
        
        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        
        // Try silent sign-in (second parameter is isSilent)
        PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated) {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        } else {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut();
            
            // Reset UI
            signInButtonText.text = "LOGIN";
            authStatus.text = "";
        }
    }
    
    private void SignInCallback(bool success) {
        if (success) {
            Debug.Log("(Rockpunch) Signed in!");

            // Change sign-in button text
            signInButtonText.text = "LOGOUT";
            
            // Show the user's name
            authStatus.text = "Logeado como: " + Social.localUser.userName;
        } else {
            Debug.Log("(Rockpunch) Sign-in failed...");
            
            // Show failure message
            signInButtonText.text = "LOGIN";
            authStatus.text = "Login fallido";
        }
    }
    
    
}
