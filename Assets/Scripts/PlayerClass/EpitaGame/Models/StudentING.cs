

    namespace PlayerClass.EpitaGame.Models
    {
        public class StudentING : Character
        {
            private int shieldCharges;
        
            public StudentING(string name) 
            {
                this.name = name;
                this.maxHealthPoints = 120;
                this.healthPoints = 120;
                this.attackPower = 12;
                this._team = "Student";
            }
        
            public void Shield()
            {
                if (!IsAlive) return;
                if (shieldCharges <= 0)
                {
                    System.Console.WriteLine($" {Name} n'a plus de charges de bouclier !");
                    return;
                }
            
                System.Console.WriteLine($"  {Name} active MODE DÉFENSE ! (+20 PV)");
                shieldCharges--;
            
                int newHP = HealthPoints + 20;
                if (newHP > MaxHealthPoints)
                {
                    newHP = MaxHealthPoints;
                }
            
                System.Console.WriteLine($"    {Name} récupère à {newHP}/{MaxHealthPoints} PV");
                System.Console.WriteLine($"    Charges restantes : {shieldCharges}");
            }
        }
    }
