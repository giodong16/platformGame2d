using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManagerSpecical01 : MonoBehaviour
{
    public Text textCoin;
    public RequirementAndTasksDialog requirementAndTasksDialog;
    public void UpdateTextCoin(int coin)
    {
        if(textCoin == null) return;
        textCoin.text = "x"+coin;
    }
}
