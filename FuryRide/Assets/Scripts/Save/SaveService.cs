using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

public class SaveService
{
    private UIMoneyShower _money;
    private CarsSaveData _carsSaveData;
    [SerializeField] private List<CarData> _allDefaultGuns;
    private Dictionary<string, CarData> _allGunDataDictionary = new Dictionary<string, CarData>();


    [Inject]
    private void Construct(UIMoneyShower money, CarsSaveData weaponSaveData)
    {
        _money = money;
        _carsSaveData = weaponSaveData;
        AllGunDataDictionaryInitialization();
    }
    private void Awake()
    {
        YandexGame.GetDataEvent += Initialize;
    }
    private void AllGunDataDictionaryInitialization()
    {
        foreach (var gun in _allDefaultGuns)
            _allGunDataDictionary.Add(gun.CarName, gun);
    }

    private void Initialize()
    {
        LoadGameData();
    }

    public void SaveGameData()
    {
        YandexGame.savesData.Money = _money.AllMoney;
        YandexGame.savesData.Gold = _money.AllGold;

        SaveGunShopAssortment();
        //YandexGame.savesData.IsFirstSwitchingOn = false;
        YandexGame.SaveProgress();
    }


    public void LoadGameData()
    {
        if (!YandexGame.savesData.IsFirstSwitchingOn)
            LoadGunShopAssortment();

        _money.Loading();
        _carsSaveData.Loading();
        YandexGame.GameReadyAPI();
    }

    private void LoadGunShopAssortment()
    {
        //_weaponSaveData.DefoltGun = _allGunDataDictionary[YandexGame.savesData.DefoltGun.GunName];

        //{
        //    _weaponSaveData.NonByedWeapon = new List<GunData>();
        //    foreach (var gun in YandexGame.savesData.NonByedGuns)
        //        _weaponSaveData.NonByedWeapon.Add(_allGunDataDictionary[gun.GunName].Init(gun));
        //}
        //{
        //    _weaponSaveData.ByedWeapon = new List<GunData>();
        //    foreach (var gun in YandexGame.savesData.ByedGuns)
        //        _weaponSaveData.ByedWeapon.Add(_allGunDataDictionary[gun.GunName].Init(gun));
        //}
    }

    private void SaveGunShopAssortment()
    {
        //YandexGame.savesData.DefoltGun = new GunDataSave().Init(_weaponSaveData.DefoltGun);
        //{
        //    YandexGame.savesData.NonByedGuns = new List<GunDataSave>();
        //    foreach (var gunData in _weaponSaveData.NonByedWeapon)
        //        YandexGame.savesData.NonByedGuns.Add(new GunDataSave().Init(gunData));
        //}
        //{
        //    YandexGame.savesData.ByedGuns = new List<GunDataSave>();
        //    foreach (var gunData in _weaponSaveData.ByedWeapon)
        //        YandexGame.savesData.ByedGuns.Add(new GunDataSave().Init(gunData));
        //}
    }
}