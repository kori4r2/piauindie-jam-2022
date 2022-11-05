using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UnitStats")]
public class UnitStats : ScriptableObject {
    [SerializeField] private int maxHP;
    public int MaxHP => maxHP;
    [SerializeField] private int attackPower;
    public int AttackPower => attackPower;
}
