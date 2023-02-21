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

    public bool GetDidSwitchWallTexture() { return m_didSwitchWallTexture; }
    public bool GetDidSwitchFloorTexture() { return m_didSwitchFloorTexture; }

    public void ResetDidSwitchWallTexture() { m_didSwitchWallTexture = false; }
    public void ResetDidSwitchFloorTexture() { m_didSwitchFloorTexture = false; }

    public Texture GetCurrentWallTexture() { return m_selectedWallTexture; }
    public Texture GetCurrentFloorTexture() { return m_selectedFloorTexture; }
    public void InitializeTextureGroups()
    {
        if (!m_wallPanelDropdown)
            return;

        m_wallPanelDropdown.AddOptions(m_wallTextureGroup.m_spriteGroup);
        m_wallPanelDropdown.onValueChanged.AddListener(delegate { ChangeWallImageType(m_wallPanelDropdown); });
        ChangeWallImageType(m_wallPanelDropdown);

        if (!m_floorTextureDropdown)
            return;

        m_floorTextureDropdown.AddOptions(m_floorTextureGroup.m_spriteGroup);
        m_floorTextureDropdown.onValueChanged.AddListener(delegate { ChangeFloorImageType(m_floorTextureDropdown); });
        ChangeFloorImageType(m_floorTextureDropdown);
    }

    public void ChangeWallImageType(TMP_Dropdown dropdown)
    {
        if (!m_wallPanelImage)
            return;

        m_selectedWallTexture = m_wallTextureGroup.m_textureGroup[dropdown.value];
        m_wallPanelImage.texture = m_selectedWallTexture;
        m_didSwitchWallTexture = true;
    }

    public void ChangeFloorImageType(TMP_Dropdown dropdown)
    {
        if (!m_floorPanelImage)
            return;

        m_selectedFloorTexture = m_floorTextureGroup.m_textureGroup[dropdown.value];
        m_floorPanelImage.texture = m_selectedFloorTexture;
        m_didSwitchFloorTexture = true;
    }
}
