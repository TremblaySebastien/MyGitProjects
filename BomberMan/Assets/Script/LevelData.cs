using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Level", fileName = "new Level", order = 1)]
public class LevelData : ScriptableObject
{
    public int m_Hp = 10;
    public int m_PlayerSpeed = 5;
    public int m_FoeSpeed = 1;
    public int m_BombDuration = 5;
    public int m_PowerUpDuration = 10;
    public int m_BombCount = 0;
    public int m_BombMaxQty = 1;


}

