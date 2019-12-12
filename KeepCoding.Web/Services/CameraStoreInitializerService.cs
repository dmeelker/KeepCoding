using KeepCoding.Core;
using KeepCoding.Core.Parsing;
using KeepCoding.Core.Storage;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KeepCoding.Web.Services
{
    public class CameraStoreInitializerService : IHostedService
    {
        private readonly CameraStore cameraStore;

        public CameraStoreInitializerService(CameraStore cameraStore)
        {
            this.cameraStore = cameraStore;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            cameraStore.Cameras = await ReadCamerasFromFile();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task<Camera[]> ReadCamerasFromFile()
        {
            using (var fileStream = File.OpenRead("data\\cameras-defb.csv"))
            using (var streamReader = new StreamReader(fileStream))
            {
                return await new CameraFileParser().ReadCameras(streamReader);
            }
        }
    }
}
