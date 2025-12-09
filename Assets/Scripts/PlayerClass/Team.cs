namespace PlayerClass
{
    public class Team
    {
        public string Name;
        public int Id;

        public int MaxHealthPoints;
        public int HealthPoints;

        public Team(string name,int id)
        {
            Name = name;
            Id = id;
            HealthPoints = 190;
            MaxHealthPoints = HealthPoints;
        }

        public void Attack(int damage)
        {
            
            HealthPoints -= damage;
            if (HealthPoints <= 0)
            {
                HealthPoints = 0;
            }
        }
        
        public bool Kill()
        {
            return HealthPoints == 0;
        }

        public void setLife(int Life)
        {
            this.HealthPoints = Life;
        }
        
        
    }
}