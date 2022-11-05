using UnityEngine;
using UnityEngine.Events;

public class AIInput : MonoBehaviour {
    public UnityEvent<Vector2> MovementPerformed { get; } = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> AttackPerformed { get; } = new UnityEvent<Vector2>();

    [SerializeField] protected PlayerVariable _playerRef;
}
