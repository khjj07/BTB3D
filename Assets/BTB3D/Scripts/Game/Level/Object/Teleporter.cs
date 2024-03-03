using BTB3D.Scripts.Game.Data;
using BTB3D.Scripts.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BTB3D.Scripts.Game.Level.Object
{
    public abstract class Teleporter : StaticLevelObject, IInteractable
    {
        public abstract void OnInteract();
    }
}
