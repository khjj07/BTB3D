using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BTB3D.Scripts.Game.ETC;
using BTB3D.Scripts.Game.Level.Object;
using BTB3D.Scripts.Game.Setting;
using BTB3D.Scripts.Util;
using UnityEngine;

namespace BTB3D.Scripts.Game.Manager
{
    public class DataManager : Singleton<DataManager>
    {
        public UserSetting userSetting;
        
        [SerializeField]
        private LevelAsset[] levels;

        [SerializeField]
        private SlotData[] slots = new SlotData[5];

        public int CountLevels()
        {
            return levels.Length;
        }

        public LevelAsset GetLevel(int index)
        {
            return levels[index];
        }

        public void SaveSlotData(int index)
        {
            var slot = slots[index];
            FileStream fileStream =
                new FileStream(Application.dataPath + "/slotdata" + index + ".dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(fileStream,slot);
            fileStream.Close();
        }

        public void LoadSlotData(int index)
        {
            FileStream fileStream = new FileStream(Application.dataPath + "/slotdata" + index + ".dat", FileMode.Open);
            slots[index] = (SlotData) new BinaryFormatter().Deserialize(fileStream);
        }

        public void SaveUserSetting()
        {
            FileStream fileStream =
                new FileStream(Application.dataPath + "/usersetting.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(fileStream, userSetting);
            fileStream.Close();
        }

        public void LoadUserSetting()
        {
            FileStream fileStream = new FileStream(Application.dataPath + "/usersetting.dat", FileMode.Open);
            userSetting = (UserSetting)new BinaryFormatter().Deserialize(fileStream);
        }

    }
}
