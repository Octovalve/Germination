using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ToolTip", menuName = "Add Tip")]
public class ToolTipSO : ScriptableObject
{
    [TextArea]  public string tipText;
}
