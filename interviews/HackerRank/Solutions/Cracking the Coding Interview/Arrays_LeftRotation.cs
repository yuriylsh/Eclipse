namespace Solutions.Cracking_the_Coding_Interview
{
    //https://www.hackerrank.com/challenges/ctci-array-left-rotation
    public static class Arrays_LeftRotation
    {
        public static void Rotate(string[] array, int rotations)
        {
            var length = array.Length;
            var swapPosition = 0;
            do
            {
                var swapWith = GetSwapPosition(length - rotations + swapPosition, length);
                SwapArrayElements(array, 0, swapWith);
                swapPosition = swapWith;
            } while (swapPosition != 0);
        }

        private static int GetSwapPosition(int swapWith, int length)
        {
            var adjusted = swapWith - length;
            return adjusted < 0 ? swapWith : adjusted;
        }

        private static void SwapArrayElements(string[] array, int a, int b)
        {
            string temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }
    }
}
