using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BarNpc : NPC
{
    public RectTransform taskPanel;
    private bool isInTask=false;
    public GameObject okButton;
    public GameObject taskButton;
    public GameObject dialogButton;
    public GameObject acceptButton;
   private int killCount=0;
    private bool isTaskComplete = false;
    private bool isInDiolog=false;
    private bool isInMainP=true;
    public static BarNpc _instance;
    //  public GameObject okButton;
    public Text text;
    private PlayerStatus playerStatus;



    private AudioSource clip;


    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }
    public void OnKillWolf()
    {
        if (isInTask)
        {
            killCount++;
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clip = GetComponent<AudioSource>();
            clip.Play();
            ShowQuest();
         
        }
    }
    void ShowQuest()
    {
        taskPanel.gameObject.SetActive(true);
        taskPanel.DOLocalMoveX(267,0.6f);
    }
    void HideQuest()
    {
        taskPanel.DOLocalMoveX(529, 0.2f);
       // taskPanel.gameObject.SetActive(false);
    }
    public void ClickClosePanel()
    {
        HideQuest();
        ShowMainPanel();
    }
    public void OnCancelButton()
    {
        if (isInMainP)
        {
            HideQuest();
            ShowMainPanel();
        }
        else { ShowMainPanel(); }
        
      //  HideQuest();
    }
    public void OnTask()
    {
        if (isInTask)
        {
            ShowTaskProgress();
        }
        else
        {
            ShowTaskPanel();
        }
    }
    public void OnAcceptClick()
    {
        isInTask = true;
        ShowTaskProgress();
    }
   public void OnDialog()
    {
       
        ShowDialogPanel();
    }
  public void OnOk()
    {
        if (isInDiolog)
        {
            ShowMainPanel();
            isInDiolog = false;
        }
        else
        {if (killCount >= 10) isTaskComplete = true;
            if (isTaskComplete)//ע�⣬���������Ҫ��
            {
                playerStatus.AddCoin(1000);
                Inventory._instance.AddCoin(1000);
                killCount = 0;
                ShowMainPanel();
            }
            else
            {
                HideQuest();
                ShowMainPanel();
            }
        }
    }
    void ShowDialogPanel()
    {
        isInMainP = false;
        isInDiolog = true;
        text.text = "���������ѽ������ϣ�����ܸ����Ǵ��Ĺ���������ҫ��";
        okButton.SetActive(true);
        taskButton.SetActive(false);
        dialogButton.SetActive(false);
        acceptButton.SetActive(false);
        
    }
    void ShowMainPanel()
    {
        isInMainP = true;
        text.text="";

        okButton.SetActive(false);
        if (isTaskComplete)
        {
            taskButton.SetActive(false);
        }
        else
        {
            taskButton.SetActive(true);
        }
        taskButton.SetActive(true);
        dialogButton.SetActive(true);
        acceptButton.SetActive(false);
    }
    void ShowTaskPanel()
    {
        isInMainP = false;
        text.text = "�����������һȺ�ǣ�Ӱ������ó�����԰��������һ����\n����\nɱ��ʮֻ�ǡ�\n������1000���";
        okButton.SetActive(false);
        taskButton.SetActive(false);
        dialogButton.SetActive(false);
        acceptButton.SetActive(true);
    }
    void ShowTaskProgress()
    {
        isInMainP = false;
        text.text = "����\nɱ��"+killCount+"/10ֻ�ǡ�\n������1000���";
        okButton.SetActive(true);
        taskButton.SetActive(false);
        dialogButton.SetActive(false);
        acceptButton.SetActive(false);
    }
}
