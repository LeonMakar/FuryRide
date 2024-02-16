using Zenject;

public class SimpleZombie : Zombie
{
    public override void DethActions()
    {
        gameObject.SetActive(false);
        ResetAllParameters();
    }

    public class Factory : PlaceholderFactory<SimpleZombie>
    {

    }
}
