using System;

namespace PlayerClass.EpitaGame.Models
{
    public abstract class Character : IPlayer
    {
        protected string name;
        protected int healthPoints;
        protected int maxHealthPoints;
        protected int attackPower;
        protected string _team;
        public string Name => name;
        public int HealthPoints => healthPoints;
        public int MaxHealthPoints => maxHealthPoints;
        public int AttackPower => attackPower;
        public bool IsAlive => healthPoints > 0;
        public string Team => _team;

        /*
        public Character(string name, int maxHealthPoints, int attackPower,string team)
        {
            this.name = name;
            this.maxHealthPoints = maxHealthPoints;
            this.healthPoints = maxHealthPoints;
            this.attackPower = attackPower;
            this._team = team;
        }
        */

        public virtual void Attack(IPlayer target)
        {
            if (target.Team.Equals(Team))
            {
                Console.WriteLine($" {name} same team !");
                return;
            };

            if (!IsAlive)
            {
                Console.WriteLine($" {name} est mort et ne peut pas attaquer !");
                return;
            }

            if (!target.IsAlive)
            {
                Console.WriteLine($" {target.Name} est déjà mort !");
                return;
            }

            Console.WriteLine($"  {name} attaque {target.Name} : -{attackPower} PV");
            target.TakeDamage(attackPower);
        }

        public virtual void TakeDamage(int damage)
        {
            healthPoints -= damage;
            
            if (healthPoints < 0)
            {
                healthPoints = 0;
            }

            Console.WriteLine($"    {name} a maintenant {healthPoints}/{maxHealthPoints} PV");

            if (!IsAlive)
            {
                Console.WriteLine($"     {name} est mort !\n");
            }
        }

        public virtual void Reset()
        {
            healthPoints = maxHealthPoints;
            Console.WriteLine($" {name} a été réinitialisé : {healthPoints}/{maxHealthPoints} PV");
        }

        public virtual void DisplayStatus()
        {
            string status = IsAlive ? "✅ Vivant" : "☠️  Mort";
            Console.WriteLine($" {name} | PV: {healthPoints}/{maxHealthPoints} | Attaque: {attackPower} | {status}");
        }
    }
}
