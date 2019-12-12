using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeepCoding.API.Models;
using KeepCoding.Core.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KeepCoding.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CameraController : ControllerBase
    {
        private readonly CameraStore cameraStore;
        public CameraController(CameraStore cameraStore)
        {
            this.cameraStore = cameraStore;
        }

        /// <summary>
        /// Gets all available cameras
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Camera> Get()
        {
            return cameraStore.Cameras.Select(camera => new Camera {
                Id = camera.Id,
                Name = camera.Name,
                Latitude = camera.Latitude,
                Longitude = camera.Longitude
            });
        }
    }
}
