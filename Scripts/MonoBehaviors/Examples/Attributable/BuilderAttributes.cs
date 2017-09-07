using System.Collections.Generic;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Examples.Attributable
{
    public struct BuilderAttributes: IUnitAttribute
    {
        public string MovesPerTurn;
        public string SpeedBonus;

        public BuilderAttributes(string movesPerTurn, string speedBonus)
        {
            MovesPerTurn = movesPerTurn;
            SpeedBonus = speedBonus;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BuilderAttributes))
            {
                return false;
            }

            var attribute = (BuilderAttributes)obj;
            return MovesPerTurn == attribute.MovesPerTurn &&
                   SpeedBonus == attribute.SpeedBonus;
        }

        public override int GetHashCode()
        {
            var hashCode = 1916520042;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MovesPerTurn);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SpeedBonus);
            return hashCode;
        }

        public Dictionary<string, int> ToDictionary()
        {
            throw new System.NotImplementedException();
        }
    }
}