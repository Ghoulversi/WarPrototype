using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralTerritory : BotTerritory
{
    public int MaxPoints;

    public override void UpdatePoints()
    {
        base.UpdatePoints();

        if (CurrentPoints > MaxPoints)
        {
            CurrentPoints = MaxPoints;
        }
    }

    public override void SetTextMeshPro()
    {
        PointsTxtPro.text = $"{CurrentPoints}/{MaxPoints}";
    }
}
