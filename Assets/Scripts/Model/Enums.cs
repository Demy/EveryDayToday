using System;
using UnityEngine;

public class Enums
{
    [Serializable]
    public enum StateChangeType
    {
        HP, Armed
    }

    [Serializable]
    public struct StateChange
    {
        [SerializeField]
        public StateChangeType type;
        [SerializeField]
        public float value;
    }

    [Serializable]
    public enum Flag
    {
        Monaster, Hp
    }
}
