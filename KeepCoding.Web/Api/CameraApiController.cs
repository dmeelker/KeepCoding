using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KeepCoding.Core;
using KeepCoding.Core.Parsing;
using KeepCoding.Core.Search;
using KeepCoding.Core.Storage;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KeepCoding.Web.Api
{
    [ApiController]
    [Route("api")]
    public class CameraApiController : ControllerBase
    {
        private readonly CameraStore cameraStore;
        public CameraApiController(CameraStore cameraStore)
        {
            this.cameraStore = cameraStore;
        }

        [HttpGet()]
        [Route("cameras")]
        public async Task<IEnumerable<Camera>> Get()
        {
            return cameraStore.Cameras;
        }
    }
}
