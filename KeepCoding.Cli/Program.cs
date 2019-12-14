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
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: KeepCoding.Cli <name query>");
                return;
            }

            var query = args[0];
            var cameraStore = await InitializeCameraStore("data\\cameras-defb.csv");
            var searcher = new CameraSearcher(cameraStore);

            SearchCamerasAndShowResults(query, searcher);
        }

        private static void SearchCamerasAndShowResults(string query, CameraSearcher searcher)
        {
            var result = searcher.SearchCameras(new SearchRequest { Name = query });

            foreach (var camera in result)
                PrintCameraDetails(camera);
        }

        private static void PrintCameraDetails(Camera camera)
        {
            Console.WriteLine($"{camera.Number} | {camera.Name} | {camera.Latitude.ToString(CultureInfo.InvariantCulture)} | {camera.Longitude.ToString(CultureInfo.InvariantCulture)}");
        }

        private static async Task<CameraStore> InitializeCameraStore(string fileName)
        {
            var cameraStore = new CameraStore();
            cameraStore.Cameras = await new CameraFileParser().ReadCamerasFromFile(fileName);

            return cameraStore;
        }
    }
}
