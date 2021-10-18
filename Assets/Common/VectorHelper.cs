
    using UnityEngine;

    public static class VectorHelper
    {
        public static Quaternion Calculate2DLookRotation(Vector3 sourceVector, Vector3 targetVector)
        {
            return Quaternion.LookRotation(Vector3.forward,  targetVector - sourceVector);
        }
    }
