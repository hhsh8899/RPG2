using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInfo : MonoBehaviour
{
    public static SkillsInfo _instance; 
    public TextAsset skillsInfoText;
    private Dictionary<int, SkillInfo> skillInfoDict = new Dictionary<int, SkillInfo>();
    private void Awake()
    {
        _instance = this;
        InitSkillInfoDict();
    }
    void InitSkillInfoDict()
    {
        string text = skillsInfoText.text;
        string[] strArray = text.Split('\n');

        foreach (string str in strArray)
        {
            string[] proArray = str.Split(',');
            SkillInfo info = new SkillInfo();

            int id = int.Parse(proArray[0]);
            string name = proArray[1];
            string icon_name = proArray[2];
            string des = proArray[3];
            string str_type = proArray[4];
            ApplyType applyType = ApplyType.Buff;
            switch (str_type)
            {
                case "Buff":
                    applyType = ApplyType.Buff;
                    break;
                case "MultiTarget":
                    applyType = ApplyType.MultiTarget;
                    break;
                case "Passive":
                    applyType = ApplyType.Passive;
                    break;
                case "SingleTarget":
                    applyType = ApplyType.SingleTarget;
                    break;
              

            }
            string str_applyProperty = proArray[5];
            ApplyProperty applyProperty = ApplyProperty.Attack;
            switch (str_applyProperty)
            {
                case "Attack":
                    applyProperty = ApplyProperty.Attack;
                    break;
                case "AttackSpeed":
                    applyProperty = ApplyProperty.AttackSpeed;
                    break;
                case "Def":
                    applyProperty = ApplyProperty.Def;
                    break;
                case "Hp":
                    applyProperty = ApplyProperty.Hp;
                    break;
                case "Mp":
                    applyProperty = ApplyProperty.Mp;
                    break;
                case "Speed":
                    applyProperty = ApplyProperty.Speed;
                    break;


            }
            int applyValue = int.Parse(proArray[6]);
            int applyTime = int.Parse(proArray[7]);
            int mp = int.Parse(proArray[8]);
            int coldTime = int.Parse(proArray[9]);
            string str_applicableRole = proArray[10];
            ApplicableRole applicableRole = ApplicableRole.Magician;
            switch (str_applicableRole)
            {
                case "Magician":
                    applicableRole = ApplicableRole.Magician;
                    break;
                case "Swordman":
                    applicableRole = ApplicableRole.Swordman;
                    break;
             
            }
            int level = int.Parse(proArray[11]);
            string str_releaseType = proArray[12];
            ReleaseType releaseType = ReleaseType.Enemy;
            switch (str_releaseType)
            {
                case "Enemy":
                    releaseType = ReleaseType.Enemy;
                    break;
                case "Position":
                    releaseType = ReleaseType.Position;
                    break;
                case "Self":
                    releaseType = ReleaseType.Self;
                    break;



            }
            float distance = float.Parse(proArray[13]);
            
            info.id = id; info.name = name; info.icon_name = icon_name;
            info.applyType = applyType;
            info.applyProperty = applyProperty;
            info.applyValue = applyValue;
            info.applyTime = applyTime;
            info.mp = mp;
            info.coldTime = coldTime;
            info.applicableRole = applicableRole;
            info.level = level;
            info.releaseType = releaseType;
            info.distance = distance;

            info.efx_name = proArray[14];
            info.ani_name = proArray[15];
            info.ani_time = float.Parse(proArray[16]);

            skillInfoDict.Add(id, info);//添加到字典中，id为key，可以很方便的根据id查找到这个物品信息
        }
    }
    public SkillInfo GetSkillInfoById(int id)
    {
        SkillInfo info = null;

        skillInfoDict.TryGetValue(id, out info);

        return info;
    }



}

public enum ApplicableRole{
    Swordman,
        Magician
}
public enum ApplyType
{
    //增益
    Passive,
    Buff,
    SingleTarget,
    MultiTarget
}
public enum ApplyProperty {
    Attack,
    Def,
    Speed,
    AttackSpeed,
    Hp,
    Mp
}
public enum ReleaseType
{
    Self,
    Enemy,
    Position
}
public class SkillInfo {
    public int id;
    public string name;
    public string icon_name;
    public string des;
    public ApplyType applyType;
    public ApplyProperty applyProperty;
    public int applyValue;
    public int applyTime;
    public int mp;
    public int coldTime;
    public ApplicableRole applicableRole;
    public int level;
    public ReleaseType releaseType;
    public float distance;

    public string efx_name;
    public string ani_name;
    public float ani_time=0;

}


