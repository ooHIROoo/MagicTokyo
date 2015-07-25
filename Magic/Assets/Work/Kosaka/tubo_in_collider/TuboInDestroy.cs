﻿using UnityEngine;
using System.Collections;


public class TuboInDestroy : MonoBehaviour
{
    [SerializeField, Tooltip("鍋に入ってから消えるまでの時間(単位：秒)")]
    float DESTROY_TIME = 0.0f;

    [SerializeField, Range(0, 10), Tooltip("ラッシュイベント時の鍋に入っているアプモンの数")]
    int MAX_APPLE_NUM = 5;

    [SerializeField, Range(0, 10), Tooltip("ラッシュイベント時の鍋に入っているレーモンの数")]
    int MAX_LEMON_NUM = 5;

    //それぞれのくだモンの鍋に入った(消した)数
    int lemon_count_ = 0;
    int apumon_count_ = 0;
    bool is_in_momon_ = false;

    bool is_in_dorian_ = false;

    //くだモンの名前
    const string LEMON_NAME = "le-mon";
    const string APUMON_NAME = "apumon";
    const string MOMON_NAME = "momon";

    const string JAMAMON_NAME = "jamamon";
    const string DORIANBOM_NAME = "dorianbom_red";

    RushEventer rush_eventer_ = null;

    //-----------------------------------------------------------------

    //それぞれのくだモンの鍋に入った(消した)数のゲッター
    public int GetLemonCount() { return lemon_count_; }
    public int GetApumonCount() { return apumon_count_; }
    public bool GetMomonCount() { return is_in_momon_; }
    public int GetKudamonCount()
    {
        var kudamon_add = lemon_count_ + apumon_count_;
        return kudamon_add;
    }
    public bool IsInDorain { get { return is_in_dorian_; } }

    void Start()
    {
        rush_eventer_ = FindObjectOfType<RushEventer>();
    }

    public void Update()
    {
        //蓋のコライダーを作る
        var lid = new GameObject();
        if (GetKudamonCount() == 10)
        {
            lid.transform.position = new Vector3(0, 3, 2.5f);
            lid.transform.rotation = new Quaternion(-20, 0, 0, 0.0f);
            lid.transform.localScale = new Vector3(2, 0.1f, 2);

            lid.gameObject.AddComponent<BoxCollider>();
        }

        //ジェスチャーしたらコライダーを消す
        //条件にサークルのジェスチャーを取得して入れれば行けるはず
        if (true)
        {
            GameObject.Destroy(lid);
        }

        RushEvent();
    }

    //鍋の中のTrigger判定
    void OnTriggerEnter(Collider other)
    {

        //それぞれのくだモンを「消す処理」と「カウント処理」（と「入ったものを出力」するためのデバッグ）
        if (other.name == LEMON_NAME)
        {
            Destroy(other.gameObject, DESTROY_TIME);
            lemon_count_++;
            //Debug.Log(" Lemon Destroy " + lemon_count);
        }
        else if (other.name == APUMON_NAME)
        {
            Destroy(other.gameObject, DESTROY_TIME);
            apumon_count_++;
            //Debug.Log(" Apple Destroy " + apumon_count);
        }
        else if (other.name == MOMON_NAME)
        {
            Destroy(other.gameObject, DESTROY_TIME);
            is_in_momon_ = true;
            //Debug.Log(" Peach Destroy " + momon_count);
        }
        else if (other.name == JAMAMON_NAME)
        {
            Destroy(other.gameObject, DESTROY_TIME);
        }
        else if (other.name == DORIANBOM_NAME)
        {
            Destroy(other.gameObject);
            is_in_dorian_ = true;
        }
    }

    void RushEvent()
    {
        if (!rush_eventer_.IsStart) return;
        lemon_count_ = MAX_LEMON_NUM;
        apumon_count_ = MAX_APPLE_NUM;
    }

    public void ResetCount()
    {
        lemon_count_ = 0;
        apumon_count_ = 0;
    }

    public void ResetMomon()
    {
        is_in_momon_ = false;
    }

    public void ResetDorian()
    {
        is_in_dorian_ = false;
    }
}