using UnityEngine;
using UnityEngine.Events;

public class Worker : AIUnit {
    [SerializeField]
    private int _grantedXP;
    public int GrantedEXP => _grantedXP;
    public readonly UnityEvent OnVanish = new UnityEvent();
    public void GoHome() {
        OnVanish.Invoke();
        OnVanish.RemoveAllListeners();
        OnDeath.RemoveAllListeners();
        Destroy(gameObject);
    }
}
