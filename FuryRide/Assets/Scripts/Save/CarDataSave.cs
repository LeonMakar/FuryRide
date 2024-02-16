﻿using UnityEngine;

[System.Serializable]
public class CarDataSave
{
    public string GunName;
    public int Damage;
    public float RateOfFire;
    public float GunSpred;
    public int BulletAmmount;
    public float ReloadingTime;

    public int DamageUpgradeCost;
    public int RateOfFireUpgradeCost;
    public int SpredUpgradeCost;
    public int AmmoUpgradeCost;
    public int ReloadingUpgradeCost;

    public int DamageUpStep;
    public float RateOfFireUpStep;
    public int SpredUpStep;
    public int AmmoUpStep;
    public int ReloadingUpStep;

    public CarDataSave Init(CarData gunData)
    {
        GunName = gunData.CarName;
        Damage = gunData.Damage;
        RateOfFire = gunData.RateOfFire;
        GunSpred = gunData.GunSpred;
        BulletAmmount = gunData.BulletAmmount;
        ReloadingTime = gunData.ReloadingTime;

        DamageUpgradeCost = gunData.DamageUpgradeCost;
        RateOfFireUpgradeCost = gunData.RateOfFireUpgradeCost;
        SpredUpgradeCost = gunData.SpredUpgradeCost;
        AmmoUpgradeCost = gunData.AmmoUpgradeCost;
        ReloadingUpgradeCost = gunData.ReloadingUpgradeCost;

        DamageUpStep = gunData.DamageUpStep;
        RateOfFireUpStep = gunData.RateOfFireUpStep;
        SpredUpStep = gunData.SpredUpStep;
        AmmoUpStep = gunData.AmmoUpStep;
        ReloadingUpStep = gunData.ReloadingUpStep;

        return this;
    }
}