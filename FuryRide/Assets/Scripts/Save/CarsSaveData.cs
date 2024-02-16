using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CarsSaveData
{
    private List<CarData> ByedCar = new List<CarData>();
    private List<CarData> NonByedCar = new List<CarData>();
    public CarData DefoltCar;

    //private Gun _gun;
    private UIMoneyShower _money;

    [Inject]
    private void Construct(/*Gun gun,*/ UIMoneyShower money)
    {
        //_gun = gun;
        _money = money;
    }
    public void ByingWeapon(CarData weaponData, PurchaseType purchaseType)
    {
        ByedCar.Add(weaponData);
        NonByedCar.Remove(weaponData);
        if (purchaseType == PurchaseType.Money)
            _money.AllMoney -= weaponData.GunCoast;
        else
            _money.AllGold -= weaponData.GunCoast;
        //_gun.EqipeNewGun(weaponData);

    }

    public void Loading()
    {
        //_gun.EqipeNewGun(DefoltCar);
    }

    public List<CarData> GetAllByedWeapon() => ByedCar;
    public void SetNewDefaultGun(CarData weaponData)
    {
        //_gun.EqipeNewGun(weaponData);
        DefoltCar = weaponData;
    }

}
