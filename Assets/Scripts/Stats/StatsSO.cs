using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Scriptable Objects/Stats")]
public class StatsSO : ScriptableObject
{
    public float CurrencyCount { get=> _currencyCount; set=> _currencyCount = value; }
    
    [SerializeField] private float _currencyCount;
}
