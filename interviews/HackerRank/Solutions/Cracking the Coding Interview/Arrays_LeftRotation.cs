namespace Solutions.Cracking_the_Coding_Interview
{
    //https://www.hackerrank.com/challenges/ctci-array-left-rotation
    public static class Arrays_LeftRotation
    {
        public static string[] Rotate(string[] array, int rotations)
        {
            var length = array.Length;
            int rotationOffset = length - rotations;
            var result = new string[length];
            for (int i = 0; i < length; i++)
            {
                result[GetSwapPosition(rotationOffset + i, length)] = array[i];
            }
            return result;
        }

        private static int GetSwapPosition(int swapWith, int length)
        {
            var adjusted = swapWith - length;
            return adjusted < 0 ? swapWith : adjusted;
        }
    }
}
