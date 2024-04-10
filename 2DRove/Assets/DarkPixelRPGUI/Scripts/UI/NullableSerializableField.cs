using System;
using UnityEngine;

namespace DarkPixelRPGUI.Scripts.UI
{
    [Serializable]
    public class NullableSerializableObjectField<T> : NullableSerializableField where T : class
    {
        [SerializeField] private T value;
        public T Value => hasValue ? value : null;
    }
    
    [Serializable]
    public class NullableSerializableStructField<T> : NullableSerializableField where T : struct
    {
        [SerializeField] private T value;
        public T? Value => hasValue ? value : null;
    }
    
    [Serializable]
    public class NullableSerializableField
    {
        [SerializeField] protected bool hasValue;
    }
}