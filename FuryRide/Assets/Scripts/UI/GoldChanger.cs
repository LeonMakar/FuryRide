using System.Collections;
using TMPro;
using UnityEngine;

public class GoldChanger : MonoBehaviour 
{

    [SerializeField] private UIMoneyShower _money;
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private SaveService _save;

    private int secondsToSaveGame = 1;

    public void InitGOldAndMoneyCount()
    {
        money.text = _money.AllMoney.ToString();
        gold.text = _money.AllGold.ToString();
    }

    public void ChangeGold()
    {
        if (_money.AllGold >= 10)
        {
            _money.AllGold -= 10;
            _money.AllMoney += 100;
            money.text = _money.AllMoney.ToString();
            gold.text = _money.AllGold.ToString();
        }
    }

    public void GoldChengerButtoneIsDone()
    {
        StopAllCoroutines();
        StartCoroutine(WaitTwoSecond());
    }

    private IEnumerator WaitTwoSecond()
    {
        yield return new WaitForSeconds(0.5f);
        _save.SaveGameData();
    }
}