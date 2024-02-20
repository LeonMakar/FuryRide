using UnityEngine;

[CreateAssetMenu(fileName = "BonusPool", menuName = "ScriptData/BonusPoll")]
public class BonusePool : ScriptableObject
{
    [SerializeField] private DoubleDamageBonus _damageBonus;
    [SerializeField] private MoneyBonus _moneyBonus;






    private CustomePool<DoubleDamageBonus> _damageBonusPool;
    private CustomePool<MoneyBonus> _moneyBonusPool;



    public CustomePool<DoubleDamageBonus> GetDoubleDamagePool() => _damageBonusPool;
    public CustomePool<MoneyBonus> GetMoneyPool() => _moneyBonusPool;



    public void Initialize()
    {
       
        _damageBonusPool = new CustomePool<DoubleDamageBonus>(new IntreractibleFactory<DoubleDamageBonus>(_damageBonus), 3, false);
        _moneyBonusPool = new CustomePool<MoneyBonus>(new IntreractibleFactory<MoneyBonus>(_moneyBonus), 3, false);
    }



    public void RemooveAllObjectFromScene()
    {
        _damageBonusPool.RemooveAllObject();
        _moneyBonusPool.RemooveAllObject();
    }
}
