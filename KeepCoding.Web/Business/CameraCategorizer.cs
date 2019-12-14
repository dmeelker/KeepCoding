namespace KeepCoding.Web.Business
{
    public static class CameraCategorizer
    {
        public static int Categorize(int cameraNumber)
        {
            if (cameraNumber % 3 == 0 && cameraNumber % 5 == 0)
                return 2;
            else if (cameraNumber % 3 == 0)
                return 0;
            else if (cameraNumber % 5 == 0)
                return 1;
            else
                return 3;
        }
    }
}
