using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public enum Arrow
{
    Up, Down, Left, Right
}

[CreateAssetMenu(fileName = "ArrowList", menuName = "Scriptable Objects/ArrowList")]
public class ArrowList : ScriptableObject
{
    public List<Arrow> arrowList;
}
