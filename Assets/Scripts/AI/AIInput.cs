using UnityEngine;

public class AIInput : MonoBehaviour {
    public System.Action<Vector2> MovementPerformed;

    [SerializeField] protected PlayerVariable _playerRef;
}
