using KeepCoding.Core;
using KeepCoding.Core.Parsing;
using KeepCoding.Core.Search;
using KeepCoding.Core.Storage;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace KeepCoding.Cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cameraStore = await InitializeCameraStore("data\\cameras-defb.csv");
            var searcher = new CameraSearcher(cameraStore);

            var result = searcher.SearchCameras(new SearchRequest { Name = "Neude" });

            foreach (var camera in result)
                PrintCameraDetails(camera);
        }

        private static void PrintCameraDetails(Camera camera)
        {
            Console.WriteLine($"{camera.Id} | {camera.Name} | {camera.Latitude.ToString(CultureInfo.InvariantCulture)} | {camera.Longitude.ToString(CultureInfo.InvariantCulture)}");
        }

        private static async Task<CameraStore> InitializeCameraStore(string fileName)
        {
            var cameraStore = new CameraStore();

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream))
            {
                cameraStore.Cameras = await new CameraFileParser().ReadCameras(streamReader);
            }

            return cameraStore;
        }
    }
}
