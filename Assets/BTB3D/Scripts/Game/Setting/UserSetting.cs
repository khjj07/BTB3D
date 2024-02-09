using System;
using UnityEngine;


namespace BTB3D.Scripts.Game.Setting
{
    [Serializable]
    public class UserSetting
    {
        [Serializable]
        public class Sound
        {
            public int masterVolume;
            public int BGMVolume;
            public int effectVolume;
        }
        [Serializable]
        public class Graphics
        {
            public enum Degree
            {
                Low,
                Medium,
                High,
                VeryHigh
            }
            public enum Quality
            {
                Low,
                Medium,
                High,
                Custom
            }
            public enum AntiAliasing
            {
               _2X=2,
               _4X=4,
               _8X=8,
            }
            public Quality quality;
            public FullScreenMode fullScreenMode;
            public Resolution resolution;
            public int maximumFPS;
            public bool verticalSynd;
            public bool motionBlur;
            public AntiAliasing antiAliasing;
            public Degree renderDistance;
            public Degree textureQulity;
            public Degree shadowQulity;
            public Degree effectQulity;
            public float fov;
        }

        public Sound soundSetting;
        public Graphics graphicsSetting;
    }
}
