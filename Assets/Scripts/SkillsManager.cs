using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    [System.Serializable]
    private struct Skill
    {
        public SkillType Type;
        public Button Button;
        public int ReloadTime;
        public int Duration;
    }
    
    [SerializeField] private Skill[] _skillsPrefabs;
    
    private Dictionary<SkillType, Skill> _skills;

    private void Start()
    {
        SetupSkillsTypes();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying)
            return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_skills[SkillType.HailOfBullets].Button.interactable)
            {
                UseSkill(SkillType.HailOfBullets);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (_skills[SkillType.Shield].Button.interactable)
            {
                UseSkill(SkillType.Shield);
            }
        }
    }

    private void UseSkill(SkillType skillType)
    {
        Skill skill;
        
        switch (skillType)
        {
            case SkillType.HailOfBullets:
                skill = _skills[SkillType.HailOfBullets];
                PlayerShip.Instance.ActivateHailOfBullets(skill.Duration);
                break;
            case SkillType.Shield:
                skill = _skills[SkillType.Shield];
                PlayerShip.Instance.ActivateShield();
                break;
            default:
                skill = _skills[SkillType.Shield];
                break;
        }

        Button button = skill.Button;
        button.interactable = false;
        
        StartCoroutine(WaitForReload(button, skill.ReloadTime));
    }

    private void SetupSkillsTypes()
    {
        _skills = new Dictionary<SkillType, Skill>();

        foreach (Skill skill in _skillsPrefabs)
        {
            _skills.Add(skill.Type, skill);
        }
    }

    private IEnumerator WaitForReload(Button button, int reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        button.interactable = true;
    }
}