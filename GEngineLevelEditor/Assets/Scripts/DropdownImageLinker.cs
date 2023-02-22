using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EDescentWallTextures
{
    METAL,
    GROUND,
    VOID
}
public enum EDescentFloorTextures
{
    METAL,
    GROUD,
    VOID
}

public class DropdownImageLinker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextureGroup m_wallTextureGroup = null;
    [SerializeField] private TextureGroup m_floorTextureGroup = null;
    [Space(20.0f)]
    [SerializeField] private TMP_Dropdown m_wallPanelDropdown = null;
    [SerializeField] private TMP_Dropdown m_floorTextureDropdown = null;
    [Space(20.0f)]
    [SerializeField] private RawImage m_wallPanelImage = null;
    [SerializeField] private RawImage m_floorPanelImage = null;

    //helpers
    private Texture m_selectedWallTexture = null;
    private Texture m_selectedFloorTexture = null;
    private bool m_didSwitchWallTexture = false;
    private bool m_didSwitchFloorTexture = false;
    private int m_currentWallTextureIndex = 0;
    private int m_currentFloorTextureIndex = 0;

    public bool GetDidSwitchWallTexture() { return m_didSwitchWallTexture; }
    public bool GetDidSwitchFloorTexture() { return m_didSwitchFloorTexture; }

    public void ResetDidSwitchWallTexture() { m_didSwitchWallTexture = false; }
    public void ResetDidSwitchFloorTexture() { m_didSwitchFloorTexture = false; }

    public Texture GetWallTexture(int index) { return m_wallTextureGroup.m_textureGroup[index]; }
    public Texture GetFloorTexture(int index) { return m_floorTextureGroup.m_textureGroup[index]; }
    public Texture GetCurrentWallTexture() { return m_selectedWallTexture; }
    public Texture GetCurrentFloorTexture() { return m_selectedFloorTexture; }
    public int GetCurrentWallTextureIndex() { return m_currentWallTextureIndex; }
    public int GetCurrentFloorTextureIndex() { return m_currentFloorTextureIndex; }
    public void InitializeTextureGroups()
    {
        if (!m_wallPanelDropdown)
            return;

        m_wallPanelDropdown.AddOptions(m_wallTextureGroup.m_spriteGroup);
        m_wallPanelDropdown.onValueChanged.AddListener(delegate { ChangeWallImageType(m_wallPanelDropdown); });
        m_selectedWallTexture = m_wallTextureGroup.m_textureGroup[m_wallPanelDropdown.value];
        m_wallPanelImage.texture = m_selectedWallTexture;

        if (!m_floorTextureDropdown)
            return;

        m_floorTextureDropdown.AddOptions(m_floorTextureGroup.m_spriteGroup);
        m_floorTextureDropdown.onValueChanged.AddListener(delegate { ChangeFloorImageType(m_floorTextureDropdown); });
        m_selectedFloorTexture = m_floorTextureGroup.m_textureGroup[m_wallPanelDropdown.value];
        m_floorPanelImage.texture = m_selectedFloorTexture;
    }

    public void ChangeWallImageType(TMP_Dropdown dropdown)
    {
        if (!m_wallPanelImage)
            return;

        m_currentWallTextureIndex = dropdown.value;
        m_selectedWallTexture = m_wallTextureGroup.m_textureGroup[dropdown.value];
        m_wallPanelImage.texture = m_selectedWallTexture;
        m_didSwitchWallTexture = true;
    }

    public void ChangeFloorImageType(TMP_Dropdown dropdown)
    {
        if (!m_floorPanelImage)
            return;

        m_currentFloorTextureIndex = dropdown.value;
        m_selectedFloorTexture = m_floorTextureGroup.m_textureGroup[dropdown.value];
        m_floorPanelImage.texture = m_selectedFloorTexture;
        m_didSwitchFloorTexture = true;
    }

    public void SetSelectedWallTextureIndex(int index)
    {
        if (index == -1)
            return;

        m_selectedWallTexture = m_wallTextureGroup.m_textureGroup[index];
        m_wallPanelImage.texture = m_selectedWallTexture;
        m_wallPanelDropdown.SetValueWithoutNotify(index);
    }

    public void SetSelectedFloorTextureIndex(int index)
    {
        if (index == -1)
            return;

        m_selectedFloorTexture = m_floorTextureGroup.m_textureGroup[index];
        m_floorPanelImage.texture = m_selectedFloorTexture;
        m_floorTextureDropdown.SetValueWithoutNotify(index);
    }
}
