using KeepCoding.Core;
using KeepCoding.Core.Parsing;
using KeepCoding.Core.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KeepCoding.API.Services
{
    public class CameraStoreInitializerService : IHostedService
    {
        private readonly ILogger<CameraStoreInitializerService> logger;
        private readonly CameraStore cameraStore;

        public CameraStoreInitializerService(ILogger<CameraStoreInitializerService> logger, CameraStore cameraStore)
        {
            this.logger = logger;
            this.cameraStore = cameraStore;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            cameraStore.Cameras = await ReadCamerasFromFile("data\\cameras-defb.csv");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task<Camera[]> ReadCamerasFromFile(string fileName)
        {
            logger.LogInformation("Loading camera information from {file}", fileName);
            var cameras = await new CameraFileParser().ReadCamerasFromFile(fileName);
            logger.LogInformation("Loaded {cameraCount} cameras", cameras.Length);
            return cameras;
        }
    }
}
