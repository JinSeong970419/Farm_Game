public delegate void HealthDelegate();

interface IHealth
{
    float HP { get; set; }
    float MaxHP { get; }

    HealthDelegate onHealthChange { get; set; }
}