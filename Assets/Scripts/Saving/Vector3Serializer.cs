
using UnityEngine;

namespace RPG.Saving
{
    [System.Serializable]
    public class Vector3Serializer 
    {
        float x, y, z;

        public Vector3Serializer(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}
