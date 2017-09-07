using System;
using System.Collections.Generic;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Examples.Attributable
{
    public interface IUnitAttribute
    {
        Dictionary<String, int> ToDictionary();
    }
}