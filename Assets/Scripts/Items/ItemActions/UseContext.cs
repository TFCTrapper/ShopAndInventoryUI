using System;
using UnityEngine;

[Serializable]
public struct UseContext
{
    public string User => _user;
    public string Target => _target;
    public float LastUseTime { get => _lastUseTime; private set => _lastUseTime = value; }

    [SerializeField] private string _user;
    [SerializeField] private string _target;
    [SerializeField] private float _lastUseTime;

    public UseContext(string user = "", string target = "")
    {
        _user = user;
        _target = target;
        _lastUseTime = Time.time;
    }
}
