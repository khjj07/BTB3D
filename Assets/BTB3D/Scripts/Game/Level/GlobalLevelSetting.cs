using BTB3D.Scripts.Game.Level.Object;
using BTB3D.Scripts.Util;
using UnityEngine;

namespace BTB3D.Scripts.Game.Level
{
    [CreateAssetMenu(fileName = "Global LevelObject Setting",menuName = "Level/Global LevelObject Setting")]
    public class GlobalLevelSetting : ScriptableSingleton<GlobalLevelSetting>
    {
        public LevelObject defaultTilePrefab;
        public Transform dummy;
        public Mesh playerMesh;
    }
}
