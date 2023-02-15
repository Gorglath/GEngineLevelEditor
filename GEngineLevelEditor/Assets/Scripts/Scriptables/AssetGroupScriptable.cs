using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GEngine/AssetGroup")]
public class AssetGroupScriptable : ScriptableObject
{
    public Sprite m_assetGroupIcon = null;
    public AssetTileScriptable[] m_groupAssetTiles = null;
}
