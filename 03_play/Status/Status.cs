using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public static Status _instance;
    private TweenPosition tween;
    private bool isShow = false;
    private UILabel atkLable;
    private UILabel defLable;
    private UILabel speedLable;
    private UILabel pointRemainLable;
    private UILabel summaryLable;

    private GameObject atkButtonGo;
    private GameObject defButtonGo;
    private GameObject speedButtonGo;

    private int summaryNum;
    private PlayerStatus playerStatus;
    private void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();


        atkLable = transform.Find("Attack").GetComponent<UILabel>();
        defLable = transform.Find("Def").GetComponent<UILabel>();
        speedLable = transform.Find("Speed").GetComponent<UILabel>();
        pointRemainLable = transform.Find("Point_Remain").GetComponent<UILabel>();
        summaryLable = transform.Find("Summary").GetComponent<UILabel>();

        atkButtonGo = transform.Find("AttackPlusButton").gameObject;
        defButtonGo = transform.Find("DefPlusButton").gameObject;
        speedButtonGo = transform.Find("SpeedPlusButton").gameObject;

        
    }
    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();

    }
    public void TransformState()
    {
        if (isShow == false)
        {
            UpdateShow();
            tween.PlayForward();
            isShow = true;
        }
        else
        {
            tween.PlayReverse();
            isShow = false;
        }
    }

    void UpdateShow()
    {
        atkLable.text = playerStatus.attack + " + " + playerStatus.attack_plus;
        defLable.text = playerStatus.def + " + " + playerStatus.def_plus;
        speedLable.text = playerStatus.speed + " + " + playerStatus.speed_plus;

        pointRemainLable.text = playerStatus.point_remain.ToString();
        summaryNum= (int)(playerStatus.attack + playerStatus.attack_plus+ playerStatus.def +  playerStatus.def_plus + playerStatus.speed + playerStatus.speed_plus);
        summaryLable.text = " " + summaryNum;

        if (playerStatus.point_remain > 0)
        {
            atkButtonGo.SetActive(true);
            speedButtonGo.SetActive(true);
            defButtonGo.SetActive(true);

        }
        else
        {
            atkButtonGo.SetActive(false);
            speedButtonGo.SetActive(false);
            defButtonGo.SetActive(false);
        }



    }
  public void AtkButtonClick()
    {

        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.attack_plus++;
            UpdateShow();

        }
    }
    public void SpeedButtonClick()
    {
        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.speed_plus++;
            UpdateShow();

        }

    }
    public void DefButtonClick()
    {
        bool success = playerStatus.GetPoint();
        if (success)
        {
            playerStatus.def_plus++;
            UpdateShow();

        }

    }




}
