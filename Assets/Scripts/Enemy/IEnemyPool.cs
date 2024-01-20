namespace ShootEmUp
{
    public interface IEnemyPool
    {
        bool TrySpawnEnemy(out EnemyController spawned);
        void UnspawnEnemy(EnemyController enemy);
    }
}