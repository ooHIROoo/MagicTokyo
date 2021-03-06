﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayer : NetworkLobbyPlayer
{

    readonly int TITLE_HASH_CODE = "title".GetHashCode();
    bool is_ready = false;
    public bool IsReady { get { return is_ready; } }

    [SerializeField]
    bool DEBUG = false;


    public void Reset()
    {
        is_ready = false;
        var wait_comment = GameObject.Find("WaitComment");
        wait_comment.GetComponent<PopComment>().Alpha = 0.0f;
        wait_comment.GetComponent<RawImage>().enabled = false;
        SendNotReadyToBeginMessage();
    }

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        if (DEBUG)
        {
            if (Application.loadedLevelName == "title" ||
                Application.loadedLevelName == "yanai_title")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (!MyNetworkLobbyManager.s_singleton.IsTutorial)
                    {
                        ChangeReady();
                        //FindObjectOfType<SceneState>().Ready();
                    }
                }
            }

        }
        if (Application.loadedLevelName == "gamemain")
        {
            is_ready = false;
        }
    }

    public void ChangeReady()
    {
        if (is_ready) return;
        if (!isLocalPlayer) return;
        if (null == MyNetworkLobbyManager.s_singleton.lobbySlots[1]) return;

        SendReadyToBeginMessage();
        GameObject.Find("WaitText").GetComponent<Text>().enabled = true;
        GameObject.Find("WaitComment").GetComponent<RawImage>().enabled = true;
        is_ready = true;
    }

    public override void OnStopAuthority()
    {
        base.OnStopAuthority();
    }

    public override void OnNetworkDestroy()
    {
        base.OnNetworkDestroy();
    }
}
