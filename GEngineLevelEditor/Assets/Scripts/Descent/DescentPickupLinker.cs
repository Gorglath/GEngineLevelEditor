using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescentPickupLinker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown m_pickupDropdown = null;
    [SerializeField] private Slider m_pickupSlider = null;
    [SerializeField] private TMP_Text m_pickupSliderText = null;

    [Header("Parameters")]
    [SerializeField] private int m_sliderMaxScoreValue = 100;
    [SerializeField] private int m_sliderMinScoreValue = 1;
    [Space(20.0f)]
    [SerializeField] private int m_sliderMaxLifeValue = 3;
    [SerializeField] private int m_sliderMinLifeValue = 1;
    [Space(20.0f)]
    [SerializeField] private int m_sliderMaxAmmoValue = 100;
    [SerializeField] private int m_sliderMinAmmoValue = 1;
    //helpers
    private EDescentPickupType m_pickupType = EDescentPickupType.NONE;
    private string[] m_enumNames = null;
    private int m_pickupHealth = 0;
    private int m_pickupScore = 0;
    private int m_pickupAmmo = 0;
    private int m_pickupIndex = 0;
    private bool m_didChangePickupType = false;
    private bool m_didChangePickupSlider = false;
    public void InitializeDropDown()
    {
        if (!m_pickupDropdown)
            return;

        m_enumNames = System.Enum.GetNames(typeof(EDescentPickupType));
        List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < m_enumNames.Length; i++)
        {
            TMP_Dropdown.OptionData optionDataPlaceholder = new TMP_Dropdown.OptionData();
            optionDataPlaceholder.text = m_enumNames[i];
            optionData.Add(optionDataPlaceholder);
        }
        m_pickupDropdown.AddOptions(optionData);
        m_pickupType = System.Enum.Parse<EDescentPickupType>(m_enumNames[m_pickupDropdown.value]);

        m_pickupDropdown.onValueChanged.AddListener(delegate { ChangedPickupType(m_pickupDropdown); });
    }

    public void ChangePickupSlider(float value)
    {
        switch (m_pickupType)
        {
            case EDescentPickupType.SCORE:
                m_pickupScore = (int)value;
                break;
            case EDescentPickupType.HEALTH:
                m_pickupHealth = (int)value;
                break;
            case EDescentPickupType.AMMO:
                m_pickupAmmo = (int)value;
                break;
            case EDescentPickupType.HOSTAGE:
                m_pickupScore = (int)value;
                break;
            default:
                break;
        }
        m_didChangePickupSlider = true;
        m_pickupSliderText.text = ((int)value).ToString();
    }
    public void ChangedPickupType(TMP_Dropdown dropdown)
    {
        m_pickupType = System.Enum.Parse<EDescentPickupType>(m_enumNames[dropdown.value]);
        m_pickupIndex = dropdown.value;
        SetSlidersMinMax();
        m_didChangePickupType = true;
    }
    public int GetPickupLastSelectedValue()
    {
        switch (m_pickupType)
        {
            case EDescentPickupType.SCORE:
                return m_pickupScore;
            case EDescentPickupType.HEALTH:
                return m_pickupHealth;
            case EDescentPickupType.AMMO:
                return m_pickupAmmo;
            case EDescentPickupType.HOSTAGE:
                return m_pickupScore;
            default:
                return 0;
        }
    }
    public float GetNewPickupValue() { return m_pickupSlider.value; }
    public EDescentPickupType GetNewPickupType() { return m_pickupType; }
    public bool GetDidChangePickupType()
    {
        bool value = m_didChangePickupType;
        m_didChangePickupType = false;
        return value;
    }
    public bool GetDidChangeSlider()
    {
        bool value = m_didChangePickupSlider;
        m_didChangePickupSlider = false;
        return value;
    }
    private void SetSlidersMinMax()
    {
        switch (m_pickupType)
        {
            case EDescentPickupType.SCORE:
                m_pickupSlider.maxValue = m_sliderMaxScoreValue;
                m_pickupSlider.minValue = m_sliderMinScoreValue;

                m_pickupSlider.SetValueWithoutNotify(m_pickupScore);
                m_pickupSliderText.text = m_pickupScore.ToString();
                break;
            case EDescentPickupType.HEALTH:
                m_pickupSlider.maxValue = m_sliderMaxLifeValue;
                m_pickupSlider.minValue = m_sliderMinLifeValue;

                m_pickupSlider.SetValueWithoutNotify(m_pickupHealth);
                m_pickupSliderText.text = m_pickupHealth.ToString();
                break;
            case EDescentPickupType.AMMO:
                m_pickupSlider.maxValue = m_sliderMaxAmmoValue;
                m_pickupSlider.minValue = m_sliderMinAmmoValue;

                m_pickupSlider.SetValueWithoutNotify(m_pickupAmmo);
                m_pickupSliderText.text = m_pickupAmmo.ToString();
                break;
            case EDescentPickupType.HOSTAGE:
                m_pickupSlider.maxValue = m_sliderMaxScoreValue;
                m_pickupSlider.minValue = m_sliderMinScoreValue;

                m_pickupSlider.SetValueWithoutNotify(m_pickupScore);
                m_pickupSliderText.text = m_pickupScore.ToString();
                break;
            default:
                break;
        }
    }
    public void SetSelectedObjectPropType(int index,int score, int ammo, int health)
    {
        if (index == -1)
            return;

        m_pickupType = System.Enum.Parse<EDescentPickupType>(m_enumNames[index]);
        m_pickupHealth = health;
        m_pickupAmmo = ammo;
        m_pickupScore = score;
        m_pickupIndex = index;
        SetSlidersMinMax();
        m_pickupDropdown.SetValueWithoutNotify(index);
    }

    public int GetSelectedPickupTypeIndex() { return m_pickupIndex; }
}
