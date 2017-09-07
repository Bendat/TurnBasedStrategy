using System.Collections.Generic;
using System.Linq;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Examples.Attributable
{
    public class MilitaryUnit: AbstractAttributable<MilitaryAttributes>
    {
        public override Dictionary<string, int> GetAttributes()
        {
            return Attributes.ToDictionary()//.concat other attributes;
        }
    }
}