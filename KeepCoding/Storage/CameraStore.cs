using KeepCoding.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
