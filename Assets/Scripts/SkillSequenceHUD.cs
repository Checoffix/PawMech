using System;
using UnityEngine;

public class SkillSequenceHUD : MonoBehaviour
{
    [SerializeField] private SkillType[] _skills;
    private int _skillNum;
    public void Clear()
    {
        foreach (var skillsType in _skills)
        {
            foreach (var skill in skillsType.skills)
            {
                if (skill.activeSelf) skill.SetActive(false);
            }
        }
        _skillNum = 0;
    }

    public void SetSkill(int skillNum)
    {
        _skills[skillNum].skills[_skillNum++].SetActive(true);
    }

    [Serializable]
    private class SkillType
    {
        public GameObject[] skills;
    }
}
