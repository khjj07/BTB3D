using BTB3D.Scripts.Game.Manager;
using BTB3D.Scripts.Interface;

namespace BTB3D.Scripts.Game.Level.Object
{
    public class GameStartSwitch : StaticLevelObject, IInteractable
    {
        public void OnInteract()
        {
            GameManager.instance.GameStart();
        }

    }
}
