using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GEngine/TextureGroup")]
public class TextureGroup : ScriptableObject
{
    public List<Sprite> m_spriteGroup = new List<Sprite>();
    public List<Texture> m_textureGroup = new List<Texture>();
}
