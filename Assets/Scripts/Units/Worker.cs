using UnityEngine;

public class Worker : AIUnit {
    [SerializeField]
    private int _grantedXP;
    public int GrantedEXP => _grantedXP;
}
