using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelData",menuName ="STO/LevelData")]
public class LevelData : ScriptableObject 
{
    public List<LevelInfor> data = new List<LevelInfor>();  
}
[System.Serializable]
public class LevelInfor
{
    public int id;
    public Vector3 levelTransform;
    public Vector3 nextPlayerPos;
}
