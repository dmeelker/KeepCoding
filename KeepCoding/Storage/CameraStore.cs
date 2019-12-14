using KeepCoding.Core.Search;

namespace KeepCoding.Core.Storage
{
    public class CameraStore : ICameraSearcherDataSource
    {
        public Camera[] Cameras { get; set; }

        public Camera[] GetAllCameras()
        {
            return Cameras;
        }
    }
}
