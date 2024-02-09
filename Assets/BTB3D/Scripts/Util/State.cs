using System;
using UnityEngine;
using UnityEngine.Events;

namespace BTB3D.Scripts.Util
{
    public abstract class State<T> : MonoBehaviour
    {
        public T next;
        public T previous;
        public UnityEvent onActiveEvent;
        public UnityEvent onInactiveEvent;

        public virtual void OnActive()
        {
            onActiveEvent.Invoke();


        }

        public virtual void OnInactive()
        {
            onInactiveEvent.Invoke();
        }
    }
}
