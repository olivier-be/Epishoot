
    using Photon.Pun.UtilityScripts;

    namespace PlayerClass.EpitaGame.Models
    {
        public class AssistantYAKA : Character
        {
            public AssistantYAKA(string name) 
            {
                this.name = name;
                this.maxHealthPoints = 80;
                this.healthPoints = 80;
                this.attackPower = 20;
                this._team = "Assistant";
            }
        
            public void RapidFire(IPlayer target)
            {
                if (!IsAlive) return;
                if (!target.IsAlive) return;
                if (target.Team.Equals(Team)) return;
            
                System.Console.WriteLine($" {Name} utilise TIR RAPIDE ! (3 attaques rapides)");
            
                for (int i = 0; i < 3; i++)
                {
                    if (!target.IsAlive) break;
                
                    int quickDamage = AttackPower / 2;
                    System.Console.WriteLine($"   ⚡ Attaque {i + 1} : -{quickDamage} PV");
                    target.TakeDamage(quickDamage);
                }
            }
        }
    }
