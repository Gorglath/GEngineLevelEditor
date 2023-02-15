using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GEngine/AssetTile")]
public class AssetTileScriptable : ScriptableObject
{
    public Sprite m_assetIconSprite = null;
    public GameObject m_assetPrefab = null;
}
