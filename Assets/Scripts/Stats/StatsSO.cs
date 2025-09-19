using UnityEngine;

[CreateAssetMenu(fileName = "StatsSO", menuName = "Scriptable Objects/StatsSO")]
public class StatsSO : ScriptableObject
{
    public float CurrencyCount { get=> _currencyCount; set=> _currencyCount = value; }
    
    [SerializeField] private float _currencyCount;
}
