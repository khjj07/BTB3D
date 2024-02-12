using System;
using System.Collections.Generic;
using UnityEditor;

namespace BTB3D.Scripts.Game.Data
{
    [Serializable]
    public class Bool2DArrayData : Scripts.Data.Data
    {
        [Serializable]
        public class BoolList
        {
            public List<bool> data = new List<bool>();
        }

        public override object GetValue()
        {
            return (object)value;
        }

        public bool[,] GetDataToArray()
        {
            bool[,] result = new bool[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    result[i, j] = value[i].data[j];
                }
            }
            return result;
        }

        public static Scripts.Data.Data Create(string name, bool[,] value, int row, int column)
        {
            var instance = CreateInstance<Bool2DArrayData>();
            EditorUtility.SetDirty(instance);
            instance.name = name;
            instance.row = row;
            instance.column = column;
            for (int i = 0; i < row; i++)
            {
                instance.value.Add(new BoolList());
                for (int j = 0; j < column; j++)
                {
                    instance.value[i].data.Add(value[i, j]);
                }
            }

            return instance;
        }
        public List<BoolList> value = new List<BoolList>();
        public int row, column;
    }
}