
    namespace PlayerClass.EpitaGame.Models
    {
        public class StudentAPPING : Character
        {
            public StudentAPPING(string name) 
            {
                this.name = name;
                this.maxHealthPoints = 100;
                this.healthPoints = 100;
                this.attackPower = 15;
                this._team = "Student";
            }
        
            public void SpecialSkill(IPlayer target)
            {
                if (!IsAlive) return;
                if (!target.IsAlive) return;
            
                int bonusDamage = AttackPower * 2;
                System.Console.WriteLine($" {Name} utilise CODING POWER ! (Double dégâts)");
                target.TakeDamage(bonusDamage);
            }
        }
    }
