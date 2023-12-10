using System;
using System.Reflection;

public class Global_Values
{
    // Global Specific
    public static float GameDifficulty { get; private set; } = 1f; // How difficult
    public static float DropRateFactor { get; private set; } = 1f;
    public static bool MobAgentOnly { get; private set; } = true;

    // Player Specific
    public static float MoveSpeed { get; private set; } = 1f;
    public static float PlayerMaxHealth { get; private set; } = 100f;

    public static float PickupRadius { get; private set; } = 1f;

    // Mob Specific
    public static bool DisableHealthbars { get; private set; } = false;
    public static bool HideHealthbars { get; private set; } = false;

    // Abilities
    public static float DashPowerFactor { get; private set; } = 2f;
    public static float DashTime { get; private set; } = 2f;

    // Weapons
    public static float WeaponDamageFactor { get; private set; } = 1f;
    public static float WeaponForceFactor { get; private set; } = 1f;
    public static float WeaponHitFactor { get; private set; } = 1f;

    public static void UpdateValue(string property, object value)
    {
        Type valueType = typeof(Global_Values);
        PropertyInfo prop = valueType.GetProperty(property);
        if (prop != null)
        {
            prop.SetValue(null, value);
        }
    }
    
    public static object GetValue(string property)
    {
        Type valueType = typeof(Global_Values);
        PropertyInfo prop = valueType.GetProperty(property);
        var value = prop.GetValue(null, null);
        return value;
    }
}
