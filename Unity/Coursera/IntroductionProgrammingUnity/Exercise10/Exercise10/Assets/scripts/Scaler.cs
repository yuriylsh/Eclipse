using UnityEngine;

namespace Assets.scripts
{
    public static class Scaler
    {
        public static void Scale(this Transform transform, float x = 1f, float y = 1f, float z = 1f) 
            => Scale(transform, new Vector3(x, y, z));

        public static void Scale(this Transform transform, Vector3 scaleBy)
        {
            var newLocalScale = transform.localScale;
            newLocalScale.Scale(scaleBy);
            transform.localScale = newLocalScale;
        }
    }
}