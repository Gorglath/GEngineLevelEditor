using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescentEnemyLinker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown m_enemiesDropdown = null;
    [SerializeField] private Slider m_enemySlider = null;
    [SerializeField] private TMP_Text m_enemySliderText = null;
    //helpers
    private EDescentEnemyType m_enemyType = EDescentEnemyType.NONE;
    private string[] m_enumNames = null;
    private bool m_didChangeEnemyType = false;
    private bool m_didChangeEnemyHealth = false;
    private int m_enemyIndex = 0;
    public void InitializeDropDown()
    {
        if (!m_enemiesDropdown)
            return;

        m_enumNames = System.Enum.GetNames(typeof(EDescentEnemyType));
        List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < m_enumNames.Length; i++)
        {
            TMP_Dropdown.OptionData optionDataPlaceholder = new TMP_Dropdown.OptionData();
            optionDataPlaceholder.text = m_enumNames[i];
            optionData.Add(optionDataPlaceholder);
        }
        m_enemiesDropdown.AddOptions(optionData);
        m_enemyType = System.Enum.Parse<EDescentEnemyType>(m_enumNames[m_enemiesDropdown.value]);

        m_enemiesDropdown.onValueChanged.AddListener(delegate { ChangedEnemyType(m_enemiesDropdown); });
    }

    public void ChangeEnemyHealth(float value)
    {
        m_didChangeEnemyHealth = true;
        m_enemySliderText.text = ((int)value).ToString();
    }
    public void ChangedEnemyType(TMP_Dropdown dropdown)
    {
        m_enemyType = System.Enum.Parse<EDescentEnemyType>(m_enumNames[dropdown.value]);
        m_enemyIndex = dropdown.value;
        m_didChangeEnemyType = true;
    }
    public float GetNewEnemyHealth() { return m_enemySlider.value; }
    public EDescentEnemyType GetNewEnemyType() { return m_enemyType; }
    public bool GetDidChangeEnemyType() 
    {
        bool value = m_didChangeEnemyType;
        m_didChangeEnemyType = false;
        return value; 
    }
    public bool GetDidChangeHealth()
    {
        bool value = m_didChangeEnemyHealth;
        m_didChangeEnemyHealth = false;
        return value;
    }
    public void SetSelectedObjectEnemyType(int index,int health)
    {
        if (index == -1)
            return;

        m_enemyType = System.Enum.Parse<EDescentEnemyType>(m_enumNames[index]);
        m_enemiesDropdown.SetValueWithoutNotify(index);
        m_enemySlider.SetValueWithoutNotify(health);
        m_enemySliderText.text = health.ToString();
    }

    public int GetSelectedEnemyTypeIndex() { return m_enemyIndex; }

}
