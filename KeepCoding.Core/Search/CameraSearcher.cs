using System;
using System.Linq;

namespace KeepCoding.Core.Search
{
    public interface ICameraSearcherDataSource
    {
        Camera[] GetAllCameras();
    }

    public class SearchRequest
    {
        public string Name { get; set; }

        public bool NameFilterSet => !string.IsNullOrEmpty(Name);
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

            if (searchRequest.NameFilterSet)
                query = FilterByName(query, searchRequest.Name);

            return query.ToArray();
        }

        private static IQueryable<Camera> FilterByName(IQueryable<Camera> query, string name)
        {
            return query.Where(camera => camera.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}
