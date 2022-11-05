using UnityEngine;

public class AIInput : MonoBehaviour {
    public System.Action<Vector2> MovementPerformed;
    public System.Action<Vector2> AttackPerformed;

    [SerializeField] protected PlayerVariable _playerRef;
}
