
    using PlayerClass;

    public interface IPlayer
    {
        string Name { get; }
        int HealthPoints { get; }
        int MaxHealthPoints { get; }
        int AttackPower { get; }
        bool IsAlive { get; }
        
        string Team { get; }

        void Attack(IPlayer target,Team team);
        void TakeDamage(int damage);
        void Reset();
        void DisplayStatus();
    }