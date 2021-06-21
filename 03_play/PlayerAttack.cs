using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NewPlayerState
{
    ControlWalk,
    NormalAttack,
    SkillAttack,
    Death
}
public enum AttackState
{
    Idle,
    Moving,
    Attack
}

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack _instance; 
    public NewPlayerState state = NewPlayerState.ControlWalk;
   
    public float ani_normalAttackTime;
    private float timer = 0;
    public float min_distance = 5;
    private Transform target_normalAttack;
    private PlayerMove playerMove;
    public AttackState attackState = AttackState.Idle;
   
    private Animator ani;
    public float normalAttackRate=1;
    private bool isShowEffect=false;

    public GameObject effect;
    private PlayerStatus playerStatus;
    public float miss_rate = 0.25f;

    public GameObject hudTextPrefab;
    private GameObject hudTextFollow;
    private GameObject hudTextGo;
    private HUDText hudText;
    public AudioClip missSound;
    public Renderer rend;
    private Color normal;

    public GameObject[] efxArray;
    private Dictionary<string, GameObject> efxDict = new Dictionary<string, GameObject>();
    public bool isLocking = false;
    private SkillInfo info=null;
    private void Awake()
    {
        _instance = this;
        ani = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerStatus = GetComponent<PlayerStatus>();

        hudTextFollow = transform.Find("HudText").gameObject;

        foreach(GameObject go in efxArray)
        {
            efxDict.Add(go.name, go);
        }
        normal = rend.material.color;
    }
    private void Start()
    {
        hudTextGo = NGUITools.AddChild(HudTextParent._instance.gameObject, hudTextPrefab);
        hudText = hudTextGo.GetComponent<HUDText>();
      UIFollowTarget  followTarget = hudTextGo.GetComponent<UIFollowTarget>();

        followTarget.target = hudTextFollow.transform;
        followTarget.gameCamera = Camera.main;
      
    }
    private void Update()
    {
        if (isLocking==false&& Input.GetMouseButtonDown(0) && state!=NewPlayerState.Death)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray,out hitInfo);
            if(isCollider && hitInfo.collider.tag == Tags.enemy)
            {
                target_normalAttack = hitInfo.collider.transform;//进入攻击状态
                state = NewPlayerState.NormalAttack;
                timer = 0;

                isShowEffect = false;
            }
            else
            {
                state = NewPlayerState.ControlWalk;
                target_normalAttack = null;
            }
        }

        if (state == NewPlayerState.NormalAttack)
        {
            if (target_normalAttack == null)
            {
                state = NewPlayerState.ControlWalk;
                ani.SetBool("normalAttack", false);
                
            }
            else
            {
                float distance = Vector3.Distance(transform.position, target_normalAttack.position);
                if (distance <= min_distance)
                {
                    transform.LookAt(target_normalAttack.position);
                    attackState = AttackState.Attack;
                    timer += Time.deltaTime;
                    ani.SetBool("run", false);
                    ani.SetBool("normalAttack", true);
                    if (timer >= ani_normalAttackTime)
                    {
                        ani.SetBool("normalAttack", false);
                        attackState = AttackState.Idle;
                        if (isShowEffect == false)
                        {
                            isShowEffect = true;
                           GameObject effectGo= GameObject.Instantiate(effect, target_normalAttack.position, Quaternion.identity);
                            target_normalAttack.GetComponent<WolfBaby>().TakeDamage(GetAttack());

                            Destroy(effectGo, 2);
                        }
                    }
                    if (timer >= (1f / normalAttackRate))
                    {
                        attackState = AttackState.Attack;
                        timer = 0;
                        isShowEffect = false;
                        ani.SetBool("normalAttack", true);
                    }

                }
                else
                {
                    ani.SetBool("normalAttack", false);

                    attackState = AttackState.Moving;
                    playerMove.SimpleMove(target_normalAttack.position);
                }
            }
        }
       else if (state == NewPlayerState.Death)
        {
           // ani.SetBool("normalAttack", false);
       //     ani.SetBool("run", false);
            ani.SetBool("die", true);
          

        }

        if (Input.GetMouseButtonDown(0) && isLocking)
        {
            OnLockTarget();
        }
    }

    public int GetAttack()
    {
        return (int)playerStatus.attack + playerStatus.attack_plus + EquipmentUi._instance.attack;
    }
    public void TakeDamage(int attack)
    {if (state == NewPlayerState.Death)
        {
           
            return;
        }
        float def = EquipmentUi._instance.def + playerStatus.def + playerStatus.def_plus;
        float temp = attack * ((200 - def) / 200);
        if (temp < 1) temp = 1;
        float value = Random.Range(0f, 1f);
        if(value < miss_rate)
        {
            AudioSource.PlayClipAtPoint(missSound, transform.position);
            hudText.Add("Miss", Color.gray, 1);
        }
        else
        {
            hudText.Add("-" + temp, Color.red, 1);
            playerStatus.hp_remain -= (int)temp;

            StartCoroutine(ShowBodyRed());

            if (playerStatus.hp_remain <= 0)
            {
                state = NewPlayerState.Death;
            
            }
        }
        HeadStatusUi._instance.UpdateShow();
    }
    IEnumerator ShowBodyRed()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        rend.material.color = normal;

    }
    public void UseSkill(SkillInfo info)
    {
        if (playerStatus.heroType == HeroType.Magician)
        {
            if (info.applicableRole == ApplicableRole.Swordman)
            {
                return;
            }
        }

        if (playerStatus.heroType == HeroType.Swordman)
        {
            if (info.applicableRole == ApplicableRole.Magician)
            {
                return;
            }
        }

        switch (info.applyType)
        {
            case ApplyType.Passive:
               StartCoroutine(OnPassiveSkillUse(info));
                break;
            case ApplyType.Buff:
                StartCoroutine(OnBuffSkillUse(info));
                break;
            case ApplyType.MultiTarget:
               OnMultiTargetSkillUse(info);
                break;
            case ApplyType.SingleTarget:
                OnSingleTargetSkillUse(info);
                break;
        }
    }
   void OnMultiTargetSkillUse(SkillInfo info)
    {
        state = NewPlayerState.SkillAttack;
        CursorManager.instance.SetLockTarget();
        isLocking = true;
        this.info = info;
    }
    void OnSingleTargetSkillUse(SkillInfo info)   //准备选择目标
    {
        state = NewPlayerState.SkillAttack;
        CursorManager.instance.SetLockTarget();
        isLocking = true;
        this.info = info;


      
    }
    void OnLockTarget()//选择目标后 开始技能的释放
    {
        isLocking = false;
        switch (info.applyType)
        {
            case ApplyType.SingleTarget:
               StartCoroutine( OnLockSingTarget());
                break;
            case ApplyType.MultiTarget:
                StartCoroutine(OnLockmultiTarget());
                break;

        }


    }
    IEnumerator OnLockmultiTarget()
    {
        CursorManager.instance.SetNormal();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool isColider = Physics.Raycast(ray, out hitInfo);
        if(isColider && hitInfo.collider.tag == Tags.ground)
        {
            ani.SetTrigger(info.ani_name);
            yield return new WaitForSeconds(info.ani_time);
            state = NewPlayerState.ControlWalk;

            GameObject prefab = null;
            efxDict.TryGetValue(info.efx_name, out prefab);
         GameObject go = GameObject.Instantiate(prefab, hitInfo.point+Vector3.up, Quaternion.identity);
           
            go.GetComponent<MagicSpehe>().attack = GetAttack() * (info.applyValue / 100);
            ani.SetBool("normalAttack", false);
            Destroy(go,8f);

        }
        else
        {
            state = NewPlayerState.ControlWalk;

        }


    }
   IEnumerator OnLockSingTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool isColider = Physics.Raycast(ray, out hitInfo);
        if (isColider && hitInfo.collider.tag == Tags.enemy)
        {
            ani.SetTrigger(info.ani_name);
            yield return new WaitForSeconds(info.ani_time);
            state = NewPlayerState.ControlWalk;
            GameObject prefab = null;
          efxDict.TryGetValue(info.efx_name, out prefab);
            GameObject effectgo = GameObject.Instantiate(prefab,hitInfo.collider.transform.position+Vector3.up*1.5f, Quaternion.identity);
            hitInfo.collider.GetComponent<WolfBaby>().TakeDamage((int)(GetAttack() * (info.applyValue / 100f)));
            ani.SetBool("normalAttack", false);
            Destroy(effectgo, 1f);
        }
        else
        {
            state = NewPlayerState.NormalAttack;
        }
        CursorManager.instance.SetNormal();
    }
    IEnumerator OnBuffSkillUse(SkillInfo info)
    {
        state = NewPlayerState.SkillAttack;
        ani.SetTrigger(info.ani_name);
        yield return new WaitForSeconds(info.ani_time);
        state = NewPlayerState.ControlWalk;
        switch (info.applyProperty)
        {
            case ApplyProperty.Attack:
                playerStatus.attack *= (info.applyValue / 100f);

                break;
            case ApplyProperty.Def:
                playerStatus.def *= (info.applyValue / 100f);
                break;
            case ApplyProperty.Speed:
                playerMove.speed *= (info.applyValue / 100f);
                break;
            case ApplyProperty.AttackSpeed:
                normalAttackRate *= (info.applyValue / 100f);
                break;

        }
        GameObject prefab = null;
        efxDict.TryGetValue(info.efx_name, out prefab);
       GameObject effectgo= GameObject.Instantiate(prefab, transform.position + Vector3.up*2.5f, Quaternion.identity);
        Destroy(effectgo, 5f);
        yield return new WaitForSeconds(info.applyTime);
        switch (info.applyProperty)
        {
            case ApplyProperty.Attack:
                playerStatus.attack /= (info.applyValue / 100f);

                break;
            case ApplyProperty.Def:
                playerStatus.def /= (info.applyValue / 100f);
                break;
            case ApplyProperty.Speed:
                playerMove.speed /= (info.applyValue / 100f);
                break;
            case ApplyProperty.AttackSpeed:
                normalAttackRate /= (info.applyValue / 100f);
                break;

        }

    }
   IEnumerator OnPassiveSkillUse(SkillInfo info)
    {
        state = NewPlayerState.SkillAttack;
       
        ani.SetTrigger(info.ani_name);
        yield return new WaitForSeconds(info.ani_time);
        state = NewPlayerState.ControlWalk;
        int hp = 0, mp = 0;
        if (info.applyProperty == ApplyProperty.Hp)
        {
            hp = info.applyValue;
        }else if (info.applyProperty == ApplyProperty.Mp)
        {
            mp = info.applyValue;
        }
        playerStatus.GetDrug(hp, mp);
        GameObject prefab = null;
        efxDict.TryGetValue(info.efx_name, out prefab);
       GameObject effectgo= GameObject.Instantiate(prefab, transform.position+Vector3.up*2.5f, Quaternion.identity);
        Destroy(effectgo, 5f);

    }
}
