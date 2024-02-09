using UnityEngine;

namespace BTB3D.Scripts.Util
{
    public abstract class StateManager<T> : MonoBehaviour where T : State<T>
    {
        public T _currentState;

        public virtual void Start()
        {
            if (_currentState != null)
            {
                Change(_currentState);
            }
        }

        public virtual void Next()
        {
          
            if (_currentState.next != null)
            {
                Change(_currentState.next);
            }
        }

        public virtual void Previous()
        {
            if (_currentState.previous != null)
            {
                Change(_currentState.previous);
            }
        }

        public virtual void Change(T state)
        {
            _currentState.OnInactive();
            _currentState = state;
            _currentState.OnActive();
        }
    }
}
