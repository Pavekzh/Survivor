public  abstract class EnemyFactory:SpawnObjectFactory
{
    protected GameBootstrap bootstrap;

    public void InitDependencies(GameBootstrap bootstrap)
    {
        this.bootstrap = bootstrap;
    }
}