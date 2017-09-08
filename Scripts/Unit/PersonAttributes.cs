using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PersonAttributes : IAttribute{

    public int WeaponHandling => _weaponHandling;
    public int HeavyWeaponHandling => _heavyWeaponHandling;
    public int Accuracy => _accuracy;
    public int Composure => _composure;           
    public int Resilience => _resilience;          
    public int Stealth => _stealth;

    public int WorkRate => _workRate;
    public int Stamina => _stamina;

    [SerializeField]
    private int _weaponHandling;
    [SerializeField]
    private int _heavyWeaponHandling;
    [SerializeField]
    private int _accuracy;
    [SerializeField]
    private int _composure;
    [SerializeField]
    private int _resilience;
    [SerializeField]
    private int _stealth;

    [SerializeField]
    private int _workRate;
    [SerializeField]
    private int _stamina;

    public PersonAttributes(int weaponHandling, int heavyWeaponHandling, int accuracy, int composure, int resilience, int stealth, int workRate, int stamina){
        _weaponHandling = weaponHandling;
        _heavyWeaponHandling = heavyWeaponHandling;
        _accuracy = accuracy;
        _composure = composure;
        _resilience = resilience;
        _stealth = stealth;
        _workRate = workRate;
        _stamina = stamina;
    }

    public Dictionary<string, int> ToDictionary(){
        return new Dictionary<string, int>(){
            {"Weapon Handling", WeaponHandling},
            {"Heavy Weapon Handling", HeavyWeaponHandling},
            {"Accuracy", Accuracy},
            {"Composure", Composure},
            {"Resilience", Resilience},
            {"Stealth", Stealth},
            {"Work Rate", WorkRate},
            {"Stamina", Stamina}
        };
    }

}
