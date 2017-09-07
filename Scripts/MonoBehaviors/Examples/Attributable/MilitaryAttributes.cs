using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Examples.Attributable
{
    /*
     * Attributes unique to the builder class.
     */
    [Serializable]
    public struct MilitaryAttributes: IAttribute
    {
        public int WeaponHandling => _weaponHandling;
        public int HeavyWeaponHandling => _heavyWeaponHandling;

        [SerializeField]private int _weaponHandling;
        [SerializeField]private int _heavyWeaponHandling;

        public MilitaryAttributes(int weaponHandling, int heavyWeaponHandling)
        {
            _weaponHandling = weaponHandling;
            _heavyWeaponHandling = heavyWeaponHandling;
        }

        /*
         * This doesnt matter
         */
        public override bool Equals(object obj)
        {
            if (!(obj is MilitaryAttributes))
            {
                return false;
            }

            var attribute = (MilitaryAttributes)obj;
            return _weaponHandling == attribute._weaponHandling &&
                   _heavyWeaponHandling == attribute._heavyWeaponHandling;
        }

        /*
         * This doesnt either.
         */
        public override int GetHashCode()
        {
            var hashCode = 1916520042;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + _weaponHandling.GetHashCode();
            hashCode = hashCode * -1521134295 + _heavyWeaponHandling.GetHashCode();
            return hashCode;
        }

        /*
         * 
         */
        public Dictionary<string, int> ToDictionary()
        {
            return new Dictionary<string, int>()
            {
                {"WeaponHandling", WeaponHandling },
                {"HeavyWeaponHandling", HeavyWeaponHandling }
            };
        }
    }
}