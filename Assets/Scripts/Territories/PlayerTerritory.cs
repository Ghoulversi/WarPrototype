using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTerritory : BotTerritory
{
    public override void SetTextMeshPro()
    {
        PointsTxtPro.text = $"{CurrentPoints}";
    }
}
