using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public abstract class Zombie : MonoBehaviour
{
    //----------Zombie characteristics----------//
    [Space(10), Header("Characteristics")]
    [SerializeField] protected int Health;
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected float Speed;
    [SerializeField] private int _healthRandomizerNumber = 30;
    [SerializeField] private int BaseMoneyToDrop;
    [SerializeField] private int MoneyRandomizingRange;

    public float SpeedForPushAwayEnemy = 10;

    [field: SerializeField] public float RotationInterval { get; private set; }

    protected int HoldedMoney;

    private int _maxHealthAfterRandomizing;
    public bool IsStartDying;
    public int GetHealthOfZombie() => Health;
    //----------Scins----------//
    [Space(10), Header("Scins and RididBodyes")]

    [SerializeField] protected Rigidbody RigidBodyCenter;
    [SerializeField] private List<GameObject> _skins;
    [SerializeField] protected List<Rigidbody> Bodies;
    private int _scinNumber;

    //----------Services----------//
    [Space(10), Header("Services")]
    [SerializeField] protected Animator Animator;
    [SerializeField] protected CharacterController CharacterController;
    [SerializeField] private TranslationZombieName ZombieName;
    [SerializeField] protected NavMeshAgent NavMeshAgent;
    protected CarMoovement Player;
    private EventBus _eventBus;
    protected GameObject ActiveScin;

    private UIMoneyShower _moneyShower;
    private HealthBar _healthBar;


    //----------Animator parameters fields----------//
    private int _isHited;
    private int _isDied;
    private int _isStandUp;
    private bool _isCrowling;
    private int _isCanAttack;
    private int _isEnemyFarAway;
    public bool CanDisableHealthBar;

    #region Initialization
    [Inject]
    public void Construct(HealthBar healthBar, UIMoneyShower moneyShower, CarMoovement player, EventBus eventBus)
    {
        _healthBar = healthBar;
        _moneyShower = moneyShower;
        Player = player;
        _eventBus = eventBus;
    }
    private void Awake()
    {
        _isHited = Animator.StringToHash("Hit");
        _isDied = Animator.StringToHash("isDead");
        _isStandUp = Animator.StringToHash("StandUp");
        _isCanAttack = Animator.StringToHash("CanAttack");
        _isEnemyFarAway = Animator.StringToHash("EnemyIsFarAway");

        CalculateMoneyHolding();
        _eventBus.Subscrube<PlayerLocationSignal>(SetNewMooveDirection);
    }
    #endregion

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Car")
        {
            var carSpeed = Player.CarRigidbody.velocity.magnitude;
            if (carSpeed >= SpeedForPushAwayEnemy)
            {
                NavMeshAgent.enabled = false;
                Animator.enabled = false;
                RigidBodyCenter.isKinematic = false;
                Player.Break();
                foreach (var body in Bodies)
                {
                    body.isKinematic = false;
                }

                RigidBodyCenter.AddForce(Vector3.up * 800, ForceMode.Impulse);

                StartCoroutine(Weiting());
            }
        }

    }

    private IEnumerator Weiting()
    {
        yield return new WaitForSeconds(4);
        var position = RigidBodyCenter.transform.localToWorldMatrix;
        transform.position = position.GetPosition();
        Animator.enabled = true;

        Animator.SetTrigger("StandUp");
        NavMeshAgent.enabled = true;
        yield return new WaitForSeconds(2);
        RigidBodyCenter.isKinematic = true;
        foreach (var body in Bodies)
        {
            body.isKinematic = true;
        }
    }

    private void SetNewMooveDirection(PlayerLocationSignal signal)
    {
        if (gameObject.activeSelf)
            NavMeshAgent.SetDestination(signal.PlayerPosition);
    }

    #region Ragdoll
    public void AddForceToBody(Vector3 forceDirection)
    {
        RigidBodyCenter.AddForce(forceDirection, ForceMode.Impulse);
    }

    /// <summary>
    /// True - Ragdoll is Unactive,
    /// False - Ragdoll is Active
    /// </summary>
    /// <param name="boolian"></param>
    public void DiactivatingRagdoll(bool boolian)
    {
        _isCrowling = boolian;
        Animator.enabled = boolian;
        foreach (var rigidbody in Bodies)
        {
            rigidbody.isKinematic = boolian;
        }
        StartCoroutine(StandUp());
    }

    private IEnumerator StandUp()
    {
        yield return new WaitForSeconds(5);
        Animator.enabled = true;
        foreach (var rigidbody in Bodies)
        {
            rigidbody.isKinematic = true;
        }
        if (Health > 0)
            Animator.SetTrigger(_isStandUp);
        else
        {
            Health = 0;
            _moneyShower.ChangeMoneyValue(HoldedMoney);
            DethActions();
            IsStartDying = true;
        }
    }
    #endregion

    #region ActionsAfterDeath
    public void DoActionsAfterSpawning()
    {
        ChangeZombieScin();
        RandomizeZombieHealth();
        CalculateMoneyHolding();
        IsStartDying = false;
    }
    private void ChangeZombieScin()
    {
        _scinNumber = UnityEngine.Random.Range(0, _skins.Count);
        _skins[_scinNumber].SetActive(true);
        ActiveScin = _skins[_scinNumber];
    }

    private void RandomizeZombieHealth()
    {
        _maxHealthAfterRandomizing = UnityEngine.Random.Range(MaxHealth - _healthRandomizerNumber, MaxHealth + _healthRandomizerNumber);
        Health = _maxHealthAfterRandomizing;
    }
    private void CalculateMoneyHolding()
    {
        HoldedMoney = UnityEngine.Random.Range(BaseMoneyToDrop - MoneyRandomizingRange, BaseMoneyToDrop + MoneyRandomizingRange);
    }
    public void ResetAllParameters()
    {
        _skins[_scinNumber].SetActive(false);
        Health = MaxHealth;
        transform.position = Vector3.zero;
    }

    public abstract void DethActions();
    #endregion

    #region HealthBarConnection
    public void OnUnderAim()
    {
        _healthBar.SetNewHealthBarValue(ZombieName.ZombieName, _maxHealthAfterRandomizing, Health);
    }
    #endregion


    public void GetDamage(int damageValue)
    {
        if (Health - damageValue > 0)
            Health -= damageValue;
        else if (Health - damageValue <= 0 && Health != 0)
        {
            Deading();
            IsStartDying = true;
        }

        _healthBar?.ChangeSliderValues(_maxHealthAfterRandomizing, 0, Health, damageValue);
        Animator.SetTrigger(_isHited);
    }
    private void Deading()
    {
        Health = 0;
        gameObject.layer = 1;
        if (!_isCrowling)
            Animator.SetTrigger(_isDied);
        else
            DethActions();

        _moneyShower.ChangeMoneyValue(HoldedMoney);
        _isCrowling = false;
        GameConstans.DiffcultyValue++;
    }
}
