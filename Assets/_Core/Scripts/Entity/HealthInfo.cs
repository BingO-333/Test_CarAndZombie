namespace Game
{
    public struct HealthInfo
    {
        public HealthInfo(float currentHealth, float baseHealth)
        {
            CurrentHealth = currentHealth;
            BaseHealth = baseHealth;
        }

        public float CurrentHealth { get; private set; }
        public float BaseHealth { get; private set; }
    }
}