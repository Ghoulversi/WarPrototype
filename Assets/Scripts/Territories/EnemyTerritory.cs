using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerritory : BotTerritory
{
    public override void SetTextMeshPro()
    {
        PointsTxtPro.text = $"{CurrentPoints}";
    }

}
