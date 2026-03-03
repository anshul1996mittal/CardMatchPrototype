using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Info", menuName = "ScriptableObjects/Create Game Info", order = 1)]
public class GameInfo : ScriptableObject
{
    public Sprite[] allCardSprites;
    public float ComboResetTimer = 2;
}
