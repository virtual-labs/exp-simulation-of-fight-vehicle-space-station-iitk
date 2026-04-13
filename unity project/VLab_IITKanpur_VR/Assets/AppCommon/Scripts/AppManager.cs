// Developed by Tushar.(c) 2025 Virtual Labs IITKanpur. All Rights Reserved.
// Description: App Level Manager to provide global functions and utilities accross multiple scenes and Application.

using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AppManager : MonoBehaviour
{
    public static AppManager Instance;
    public GameObject confirmationCanvas;
    public TMP_Text messageText;
    public Button yesButton, noButton;
    public AppState appState;
    public bool isMenuRequested = false;
    AudioSource aud;
    public AudioClip popSFX, clickSFX;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        aud = GetComponent<AudioSource>();
    }

    public void OnMenuRequest() 
    {
        if(appState != AppState.menu)
        {
            confirmationCanvas.SetActive(true);
            aud.clip = popSFX;
            aud.Play();
            var o = GameObject.Find("PopUpPoint");
            if (o != null)
            {
                confirmationCanvas.transform.SetParent(o.transform, true);
                confirmationCanvas.transform.position = o.transform.position + new Vector3(0f, 0.1f, 0.1f);
                confirmationCanvas.transform.localRotation = Quaternion.Euler(Vector3.zero);

                messageText.text = "Go Back to Menu ?";
                isMenuRequested = true;
            }
        }
    }
    public void OnQuitRequest()
    {
        confirmationCanvas.SetActive(true);
        aud.clip = popSFX;
        aud.Play();
        var o = GameObject.Find("PopUpPoint");
        if (o != null)
        {
            confirmationCanvas.transform.SetParent(o.transform, true);
            confirmationCanvas.transform.position = o.transform.position + new Vector3(0f, 0.1f, 0.1f);
            confirmationCanvas.transform.localRotation = Quaternion.Euler(Vector3.zero);

            messageText.text = "Do you really want to quit ?";
            isMenuRequested = false;
        }
    }

    
    void OnQuitConfirmed()
    {
        Application.Quit();
    }
    void OnMenuConfirmed()
    {
        confirmationCanvas.transform.SetParent(gameObject.transform, true);
        appState = AppState.menu;
        confirmationCanvas.SetActive(false);
        aud.clip = clickSFX;
        aud.Play();
        SceneManager.LoadScene("Menu");
    }
    public void OnNoConfirmed()
    {
        confirmationCanvas.transform.SetParent(gameObject.transform, true);
        confirmationCanvas.SetActive(false);
    }

    public void OnYesConfirmed()
    {
        if (isMenuRequested)
        {
            OnMenuConfirmed();
        }
        else
        {
            OnQuitConfirmed();
        }
    }
    public void PlaySFX()
    {
        aud.clip = clickSFX;
        aud.Play();
    }

    public enum AppState
    {
        menu,
        game
    }
}
