using FluentAssertions;
using KeepCoding.Core.Search;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace KeepCoding.Core.Tests.Search
{
    public class CameraSearcherTest
    {
        private readonly MockCameraSearcherDataSource cameraSource = new MockCameraSearcherDataSource();
        private readonly Camera neudeSchoutenStraatCamera = CreateCamera("UTR-CM-504 Neude / Schoutenstraat", 52.092995, 5.119088);
        private readonly Camera neudeDrakenburgstraatStraatCamera = CreateCamera("UTR-CM-505 Neude / Drakenburgstraat / Vinkenurgstraat", 52.092843, 5.118351);
        private readonly Camera korteMinrebroederstraatCamera = CreateCamera("UTR-CM-510 Korte Minrebroederstraat", 52.092174, 5.119866);

        public CameraSearcherTest()
        {
            MockCameras();
        }

        [Fact]
        public void Search_NoCriteriaReturnsAllCameras()
        {
            var request = new SearchRequest();

            var searchResults = CreateCameraSearcher().SearchCameras(request);

            searchResults.Length.Should().Be(cameraSource.Cameras.Count);
        }

        [Fact]
        public void Search_NameSubstring()
        {
            var request = new SearchRequest{
                Name = "Neude"
            };

            var searchResults = CreateCameraSearcher().SearchCameras(request);

            searchResults.Should().HaveCount(2)
                .And.Contain(new[] { neudeDrakenburgstraatStraatCamera, neudeSchoutenStraatCamera });
        }

        [Fact]
        public void Search_NameSearchNotCaseSensitive()
        {
            var request = new SearchRequest
            {
                Name = "NEUDE"
            };

            var searchResults = CreateCameraSearcher().SearchCameras(request);

            searchResults.Should().HaveCount(2)
                .And.Contain(new[] { neudeDrakenburgstraatStraatCamera, neudeSchoutenStraatCamera });
        }

        private void MockCameras()
        {
            cameraSource.Cameras.Add(neudeSchoutenStraatCamera);
            cameraSource.Cameras.Add(neudeDrakenburgstraatStraatCamera);
            cameraSource.Cameras.Add(korteMinrebroederstraatCamera);
        }

        private CameraSearcher CreateCameraSearcher()
        {
            return new CameraSearcher(cameraSource);
        }

        private static Camera CreateCamera(string name, double latitude, double longitude)
        {
            return new Camera { 
                Name = name,
                Latitude = latitude,
                Longitude = longitude
            };
        }
    }

    public class MockCameraSearcherDataSource : ICameraSearcherDataSource
    {
        public List<Camera> Cameras = new List<Camera>();

        public Camera[] GetAllCameras()
        {
            return Cameras.ToArray();
        }
    }
}
