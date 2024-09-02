using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentDayText;

    private int _currentDay;

    public void IncrementDay()
    {
        _currentDay++;
        currentDayText.text = $"DAY {_currentDay}";
    }
}