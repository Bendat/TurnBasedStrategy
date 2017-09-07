using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Examples.Attributable
{
    [Serializable]
    public struct BuilderAttributes: IUnitAttribute
    {
        public int MovesPerTurn => _movesPerTurn;
        public int SpeedBonus => _speedBonus;

        [SerializeField]private int _movesPerTurn;
        [SerializeField]private int _speedBonus;

        public BuilderAttributes(int movesPerTurn, int speedBonus)
        {
            _movesPerTurn = movesPerTurn;
            _speedBonus = speedBonus;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BuilderAttributes))
            {
                return false;
            }

            var attribute = (BuilderAttributes)obj;
            return _movesPerTurn == attribute._movesPerTurn &&
                   _speedBonus == attribute._speedBonus;
        }

        public override int GetHashCode()
        {
            var hashCode = 1916520042;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + _movesPerTurn.GetHashCode();
            hashCode = hashCode * -1521134295 + _speedBonus.GetHashCode();
            return hashCode;
        }

        public Dictionary<string, int> ToDictionary()
        {
            throw new System.NotImplementedException();
        }
    }
}