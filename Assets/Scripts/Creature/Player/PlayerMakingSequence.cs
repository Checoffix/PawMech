using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerMakingSequence : MonoBehaviour
{
    [SerializeField] private Image[] _skillImage;
    [SerializeField] private float _skillCooldown;
    [SerializeField] private int _skillSequenceLength;
    [SerializeField] private SkillSequenceHUD _skillHUD;
    [SerializeField] private TextMeshProUGUI _skillNameTip;
    [SerializeField] private OverheatHUD _overheatHUD;
    [SerializeField] private SetPulse _pulseR;
    [SerializeField] private BaseSkill[] _skills;
    [SerializeField] private UnityEvent<int> _onCast;
    [SerializeField] private UnityEvent _onFailCast;
    private int[] _skillSequence;
    private int _currentIndex = 0;
    private int _currentSkillNum;
    private float[] _skillTime;

    private void Start()
    {
        _skillSequence = new int[_skillSequenceLength];
        for (int i = 0; i < _skillSequenceLength; i++)
        {
            _skillSequence[i] = -1;
        }
        _skillTime = new float[_skillImage.Length];
        for (int i = 0; i < _skillTime.Length; i++)
        {
            _skillTime[i] = _skillCooldown;
        }
    }
    public void UseSkill(int skillNum)
    {
        if (_skillTime[skillNum] >= _skillCooldown)
        {
            if (_currentIndex < _skillSequenceLength)
            {
                _skillTime[skillNum] = 0;
                _skillSequence[_currentIndex++] = skillNum;
                VisualizeSkillSequence();
                _currentSkillNum = CheckValidity();
            }
        }
    }


    private void FixedUpdate()
    {
        for (int i = 0; i < _skillTime.Length; i++)
        {
            if (_skillTime[i] != _skillCooldown)
            {
                _skillTime[i] += Time.fixedDeltaTime;
                _skillImage[i].fillAmount = _skillTime[i] / _skillCooldown;
            }
        }
    }
    private void VisualizeSkillSequence()
    {
        _skillHUD.Clear();
        for (int i = 0; i < _currentIndex; i++)
        {
            _skillHUD.SetSkill(_skillSequence[i]);
        }
    }

    public void DeleteSequence()
    {
        for (; _currentIndex != 0; _currentIndex--)
        {
            _skillSequence[_currentIndex - 1] = -1;
        }
        _skillHUD.Clear();
        _overheatHUD.DisableFlashHeat();
        _currentSkillNum = -1;
        _skillNameTip.text = "";
        _pulseR.enabled = false;
    }

    public int CheckValidity()
    {
        int skillNum = -1;
        string skillsInString = string.Empty;
        for (int i = 0; i < _currentIndex; i++)
        {
            skillsInString += _skillSequence[i].ToString();
        }
        switch (skillsInString)
        {
            case "00":
                _overheatHUD.FlashHeat(25);
                _skillNameTip.text = "Shotgun";
                skillNum = 0;
                break;
            case "01":
                _overheatHUD.FlashHeat(80);
                _skillNameTip.text = "Laser";
                skillNum = 1;
                break;
            case "02":
                _overheatHUD.FlashHeat(100);
                _skillNameTip.text = "Bomb";
                skillNum = 2;
                break;
            case "10":
                _overheatHUD.FlashHeat(70);
                _skillNameTip.text = "Blank";
                skillNum = 3;
                break;
            case "20":
                _overheatHUD.FlashHeat(70);
                _skillNameTip.text = "Buffs";
                skillNum = 4;
                break;
            case "21":
                _overheatHUD.FlashHeat(20);
                _skillNameTip.text = "Illusions";
                skillNum = 5;
                break;
            default:
                _overheatHUD.DisableFlashHeat();
                _skillNameTip.text = "";
                break;
        }
        if (skillNum != -1) _pulseR.enabled = true;
        else _pulseR.enabled = false;
        return skillNum;
    }

    public void Cast()
    {
        if (_currentSkillNum != -1 && _overheatHUD.IncreaseCurrentFlash())
        {
            _onCast?.Invoke(_currentSkillNum);
            _skills[_currentSkillNum].Use();
            _pulseR.enabled = false;
        }
        else
        {
            _onFailCast?.Invoke();
        }
        DeleteSequence();
    }
}
