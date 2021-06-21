using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum WolfState
{
    Idle,
    Walk,
    Attack,
    Death
}



public class WolfBaby : MonoBehaviour
{
    public WolfState state = WolfState.Idle;
    public int hp=100;
    public int attack=10;
    public float miss_rate=0.2f;
    public string aniname_death;
    private Animation ani;
    public int hpm;
    public string aniname_idle;
    public string aniname_walk;
    public string aniname_now;
    public float time = 1;
    public float timer = 0;
    private CharacterController cc;
    private float speed=0.5f;

    private Color normal;
    public AudioClip miss_sound;
    public Renderer rend;

    private GameObject hudTextFollow;
    private GameObject hudTextGo;
    public GameObject hudTextPrefab;

    private HUDText hudText;
    private UIFollowTarget followTarget;
    public Camera cam;

    public string ani_normalAttack;
    public string ani_crazyAttack;
    public float ani_normalAttackTime;
    public float ani_crazyAttackTime;
    public string ani_attack_now;

    public float crazyAttackRate;
    public int attackRate=1;

    private float attack_timer = 0;
    //自动攻击
    public float minDistance = 2;
    public float maxDistance = 5;
    public Transform target;
    public WolfSpawn wolfSpawn;
    public int exp = 20;
    private PlayerStatus playerStatus;
    private void Awake()
    {
        aniname_now = aniname_idle;
        cc = GetComponent<CharacterController>();
        hudTextFollow = transform.Find("HudText").gameObject;
        normal = rend.material.color;
      

    }
    private void Start()
    {
        
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
       
        ani = GetComponent<Animation>();
       // hudTextGo = GameObject.Instantiate(hudTextPrefab, Vector3.zero, Quaternion.identity) as GameObject;
    //    hudTextGo.transform.parent = HudTextParent._instance.gameObject.transform;

        hudTextGo = NGUITools.AddChild(HudTextParent._instance.gameObject, hudTextPrefab);
        hudText = hudTextGo.GetComponent<HUDText>();
        followTarget = hudTextGo.GetComponent<UIFollowTarget>();
       
        followTarget.target = hudTextFollow.transform;
        followTarget.gameCamera = Camera.main;
        followTarget.uiCamera = cam;

    }
    
    public float GetHp()
    {
        return (float)hp / hpm;
    }
    private void Update()
    {
      

        if (state == WolfState.Death)
        {

            ani.CrossFade(aniname_death);
        }
        else if (state==WolfState.Attack)
        {
            AudioAttack();
        }
        else
        {
            ani.CrossFade(aniname_now);

            if (aniname_now == aniname_walk)
            {
                cc.SimpleMove(transform.forward*speed);
            }
            timer += Time.deltaTime;
            if (timer >= time)
            {
                timer = 0;
                RandomState();
            }
        }
    }

    void AudioAttack()
    {
       
        if (target != null)
        {
            NewPlayerState playerState = target.GetComponent<PlayerAttack>().state;
            if (playerState == NewPlayerState.Death)
            {
                target = null;
                state = WolfState.Idle;
                return;
            }

            float distance = Vector3.Distance(target.position, transform.position);
            if (distance > maxDistance)
            {
                target = null;
                state = WolfState.Idle;
            }else if (distance<=minDistance) {
                attack_timer += Time.deltaTime;
                transform.LookAt(target);
                ani.CrossFade(ani_attack_now);
                if (ani_attack_now == ani_normalAttack)
                {
                    if (attack_timer > ani_normalAttackTime)
                    {
                        //对玩家产生伤害
                        target.GetComponent<PlayerAttack>().TakeDamage(attack);
                        ani_attack_now = aniname_idle;
                    }
                }
                else if(ani_attack_now==ani_crazyAttack)
                {
                    if (attack_timer > ani_crazyAttackTime)
                    {
                        //对玩家产生伤害
                        target.GetComponent<PlayerAttack>().TakeDamage(attack);
                        ani_attack_now = aniname_idle;
                    }
                }
                if (attack_timer > (1f / attackRate))
                {
                    RandomAttack();
                    attack_timer = 0;
                }


            }
            else
            {
                transform.LookAt(target);
                cc.SimpleMove(transform.forward * speed);
                ani.CrossFade(aniname_walk);
            }


        }
        else
        {
            state = WolfState.Idle;
        }
    }

    void RandomAttack()
    {
        float value = Random.Range(0f, 1f);
        if (value < crazyAttackRate)
        {
            ani_attack_now = ani_crazyAttack;
        }
        else
        {
            ani_attack_now = ani_normalAttack;
        }
    }

    public void TakeDamage(int attack)
    {
        if (state == WolfState.Death) return;

        target = GameObject.FindGameObjectWithTag(Tags.player).gameObject.transform;
        state = WolfState.Attack;
        float value = Random.Range(0f, 1f);
        if (value < miss_rate)
        {
            AudioSource.PlayClipAtPoint(miss_sound, transform.position);
            hudText.Add("Miss", Color.gray, 1);
        }
        else
        {
            hudText.Add("-"+attack, Color.red, 1);
            this.hp -= attack;

            StartCoroutine(ShowBodyRed());

            if (hp <= 0)
            {
                state = WolfState.Death;
                Destroy(this.gameObject, 1);
                GameObject.Destroy(hudTextGo);
                wolfSpawn.MinusNumber();
                playerStatus.GetExp(exp);
                BarNpc._instance.OnKillWolf();
            }
        }

    }
    private void RandomState()
    {
        int value = Random.Range(0, 2);
        if (value == 0)
        {
            aniname_now = aniname_idle;
        }
        else
        {
            if (aniname_now != aniname_walk)
            {
                transform.Rotate(transform.up * Random.Range(0, 360));
            }
            aniname_now = aniname_walk;
        }
    }
    IEnumerator ShowBodyRed()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        rend.material.color = normal;

    }

    private void OnMouseEnter()
    {
        if(PlayerAttack._instance.isLocking==false)
        CursorManager.instance.SetAttack();
    }
    private void OnMouseExit()
    {
        if (PlayerAttack._instance.isLocking == false)
            CursorManager.instance.SetNormal();
    }
}
