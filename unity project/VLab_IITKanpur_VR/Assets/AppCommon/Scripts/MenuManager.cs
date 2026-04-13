// Developed by Tushar.(c) 2025 Virtual Labs IITKanpur. All Rights Reserved.
// Description: A Manager for Menu Operations in Menu Scene.

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public List<LabData> labs = new();
    public Image thumbnailImage;
    public TMP_Text headingText;
    public TMP_Text detailsText;

    public string sceneName;
    public int currentIndex;

    public Button left, right;

    void Start()
    {
        InitializeData();
    }

    public void InitializeData()
    {
        thumbnailImage.sprite = labs[0].thumbnail;
        headingText.text = labs[0].heading;
        detailsText.text = labs[0].details;
        sceneName = labs[0].sceneName;
        currentIndex = 0;
        left.interactable = false;
        right.interactable = true;
    }

    public void TogglePress(int n)
    {
        currentIndex += n;

        if(currentIndex >= 0 && currentIndex <= labs.Count - 1)
        {
            thumbnailImage.sprite = labs[currentIndex].thumbnail;
            headingText.text = labs[currentIndex].heading;
            detailsText.text = labs[currentIndex].details;
            sceneName = labs[currentIndex].sceneName;

            left.interactable = true;
            right.interactable = true;
            AppManager.Instance.PlaySFX();
        }
        if(currentIndex == 0)
        {
            left.interactable = false;
            right.interactable = true;
        }
        if (currentIndex == labs.Count - 1)
        {
            left.interactable = true;
            right.interactable = false;
        }
    }

    public void OnPlayPress()
    {
        if (!String.IsNullOrEmpty(sceneName))
        {
            AppManager.Instance.PlaySFX();
            AppManager.Instance.appState = AppManager.AppState.game;
            SceneManager.LoadScene(sceneName);
        }
    }

    public void OnInfoPress()
    { 

    }
    public void OnReportBugPress()
    {

    }
}
