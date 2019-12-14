using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeepCoding.Web.ApiClient;
using KeepCoding.Web.Business;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeepCoding.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IKeepCodingApiClient apiClient;

        public Camera[] Cameras { get; set; }
        public Dictionary<int, Camera[]> CategorizedCameras { get; set; }

        public IndexModel(IKeepCodingApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task OnGet()
        {
            Cameras = await LoadCameras();
            CategorizedCameras = CategorizeCameras(Cameras);
        }

        private async Task<Camera[]> LoadCameras()
        {
            return (await apiClient.CameraAsync()).ToArray();
        }

        private Dictionary<int, Camera[]> CategorizeCameras(Camera[] cameras)
        {
            return cameras.GroupBy(camera => CameraCategorizer.Categorize(camera.Number))
                .ToDictionary(group => group.Key, group => group.ToArray());
        }
    }
}
