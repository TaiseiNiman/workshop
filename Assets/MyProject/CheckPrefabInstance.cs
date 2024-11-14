using UnityEngine;
using UnityEngine.Events;
using System;

namespace MyProject
{
    [Serializable]
    public class UnityEventT<T> : UnityEvent<T> { }
}