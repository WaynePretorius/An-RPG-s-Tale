
using UnityEngine;

namespace RPG.Saving
{
    //make the class serializible
    [System.Serializable]
    public class Vector3Serializer 
    {
        //variables declared
        float x, y, z;

        //turn the vector to 3 floats
        public Vector3Serializer(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        //turn 3 floats into a vector3
        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}
