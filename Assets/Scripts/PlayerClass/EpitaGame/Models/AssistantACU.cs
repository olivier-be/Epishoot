
    namespace PlayerClass.EpitaGame.Models
    {
        public class AssistantACU : Character
        {
            public AssistantACU(string name) 
                {
                    this.name = name;
                    this.maxHealthPoints = 90;
                    this.healthPoints = 90;
                    this.attackPower = 18;
                    this._team = "Assistant";
                } 
        
            public void CriticalStrike(IPlayer target)
            {
                if (!IsAlive) return;
                if (!target.IsAlive) return;
            
                int critDamage = AttackPower * 3;
                System.Console.WriteLine($" {Name} utilise COUP CRITIQUE ! (Triple dégâts)");
                target.TakeDamage(critDamage);
            }
        }
    }
