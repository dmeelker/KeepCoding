using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeepCoding.Core.Search
{
    public interface ICameraSearcherDataSource
    {
        Camera[] GetAllCameras();
    }

    public class SearchRequest
    {
        public string Name { get; set; }
    }

    public class CameraSearcher
    {
        private readonly ICameraSearcherDataSource dataSource;

        public CameraSearcher(ICameraSearcherDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public Camera[] SearchCameras(SearchRequest searchRequest)
        {
            var query = dataSource.GetAllCameras().AsQueryable();

            if (!string.IsNullOrEmpty(searchRequest.Name))
                query = query.Where(camera => camera.Name.IndexOf(searchRequest.Name, StringComparison.OrdinalIgnoreCase) >= 0);

            return query.ToArray();
        }
    }
}
