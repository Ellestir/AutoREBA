using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ethancontroler : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btn_show_avatar;
    public Button btn_hide_avatar;
    public Button btn_show_score;
    public Button btn_hide_score;
    public GameObject avatar;

    private string userScore;
    // Start is called before the first frame update
    public void Start()
    {
        avatar.SetActive(false);
        btn_show_avatar.onClick.AddListener(showAvatar);
        btn_hide_avatar.onClick.AddListener(hideAvatar);
    }
    public void showAvatar()
    {
        avatar.SetActive(true);
    }
    public void hideAvatar()
    {
        avatar.SetActive(false);
    }

    public void updateScore(string score)
    {
        userScore = score;
    }

    private void Update()
    {
        
    }
}
