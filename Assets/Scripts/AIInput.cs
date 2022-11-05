using UnityEngine;

public class AIInput : MonoBehaviour {
    private System.Action<Vector2> _movementPerformed;
    public System.Action<Vector2> MovementPerformed => _movementPerformed;



    public void Update() {
        
    }
}
