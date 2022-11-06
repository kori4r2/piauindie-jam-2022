using System;
using UnityEngine;
using TMPro;
using Toblerone.Toolbox;

public class TimerUpdater : MonoBehaviour {
    [SerializeField] private FloatVariable timeVariable;
    [SerializeField] private TextMeshProUGUI textField;
    private VariableObserver<float> timeObserver;

    private void Awake() {
        timeObserver = new VariableObserver<float>(timeVariable, UpdateTimer);
    }

    private void UpdateTimer(float newTimeSeconds) {
        int totalSeconds = Mathf.CeilToInt(Mathf.Max(0f, newTimeSeconds));
        int seconds = 0;
        int minutes = Math.DivRem(totalSeconds, 60, out seconds);
        textField.text = $"{minutes:00}:{seconds:00}";
    }

    private void OnEnable() {
        timeObserver.StartWatching();
    }

    private void OnDisable() {
        timeObserver.StopWatching();
    }
}
