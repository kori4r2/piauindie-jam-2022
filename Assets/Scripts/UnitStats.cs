using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UnitStats")]
public class UnitStats : ScriptableObject {
    [SerializeField] private int maxHP;
    public int MaxHP => maxHP;
    [SerializeField] private int attackPower;
    public int AttackPower => attackPower;
    [SerializeField, Range(0f, 20f)] private float moveSpeed = 5f;
    public float MoveSpeed => moveSpeed;
}
