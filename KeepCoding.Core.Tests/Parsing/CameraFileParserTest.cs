using FluentAssertions;
using KeepCoding.Core.Parsing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KeepCoding.Core.Tests.Parsing
{
    public class CameraFileParserTest
    {
        [Fact]
        public async Task ReadCameras_HeaderLineIsIgnored()
        {
            var input = new StringBuilder();
            input.AppendLine("Camera;Latitude;Longitude");

            var parsedCameras = await ReadCamerasFromString(input.ToString());

            parsedCameras.Should().HaveCount(0);
        }

        [Fact]
        public async Task ReadCameras()
        {
            var input = new StringBuilder();
            input.AppendLine("Camera;Latitude;Longitude");
            input.AppendLine("This is hardly a valid line");
            input.AppendLine("UTR-CM-501 Neude rijbaan voor Postkantoor;52.093421;5.118278");

            var parsedCameras = await ReadCamerasFromString(input.ToString());

            parsedCameras.Should().HaveCount(1);
            var camera = parsedCameras.First();
            camera.Number.Should().Be(501);
            camera.Name.Should().Be("UTR-CM-501 Neude rijbaan voor Postkantoor");
            camera.Latitude.Should().Be(52.093421);
            camera.Longitude.Should().Be(5.118278);
        }

        [Fact]
        public void ParseLine_FieldsAreExtractedCorrectly()
        {
            var camera = new CameraFileParser().ParseLine("UTR-CM-501 Neude rijbaan voor Postkantoor;52.093421;5.118278");

            camera.Name.Should().Be("UTR-CM-501 Neude rijbaan voor Postkantoor");
            camera.Latitude.Should().Be(52.093421);
            camera.Longitude.Should().Be(5.118278);
        }

        [Fact]
        public void ParseLine_IncorrectLatitudeTypeReturnsNull()
        {
            var camera = new CameraFileParser().ParseLine("UTR-CM-501 Neude rijbaan voor Postkantoor;BROKEN;5.118278");
            camera.Should().BeNull();
        }

        [Fact]
        public void ParseLine_IncorrectLongitudeTypeReturnsNull()
        {
            var camera = new CameraFileParser().ParseLine("UTR-CM-501 Neude rijbaan voor Postkantoor;52.093421;BROKEN");
            camera.Should().BeNull();
        }

        [Fact]
        public void ParseLine_IncorrectNumberOfFieldsReturnsNull()
        {
            var camera = new CameraFileParser().ParseLine("This does not have the correct number of fields");
            camera.Should().BeNull();
        }

        [Theory]
        [InlineData("UTR-CM-530 Westplein / Van Sijpesteijnkade", 530)]
        [InlineData("UTR-CM-743-Boelesteinlaan - Queeckhovenplein", 743)]
        public void ParseCameraNumberFromName(string input, int expectedId)
        {
            var parsedId = new CameraFileParser().ParseCameraNumberFromName(input);
            parsedId.Should().Be(expectedId);
        }

        private async Task<Camera[]> ReadCamerasFromString(string input)
        {
            using (var reader = new StringReader(input))
            {
                var parser = new CameraFileParser();
                return await parser.ReadCameras(reader);
            }
        }
    }
}
