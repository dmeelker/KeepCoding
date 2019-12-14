using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KeepCoding.Core.Parsing
{
    public class CameraFileParser
    {
        public async Task<Camera[]> ReadCamerasFromFile(string fileName)
        {
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream))
            {
                return await ReadCameras(streamReader);
            }
        }

        public async Task<Camera[]> ReadCameras(TextReader reader)
        {
            await SkipColumnHeader(reader);
            return await ParseCameras(reader);
        }

        private static async Task SkipColumnHeader(TextReader reader)
        {
            await reader.ReadLineAsync();
        }

        private async Task<Camera[]> ParseCameras(TextReader reader)
        {
            var cameras = new List<Camera>();
            string currentLine;
            do
            {
                currentLine = await reader.ReadLineAsync();
                if (currentLine != null)
                {
                    var camera = ParseLine(currentLine);
                    if (camera != null)
                        cameras.Add(camera);
                }
            } while (currentLine != null);

            return cameras.ToArray();
        }

        internal Camera ParseLine(string currentLine)
        {
            var parts = currentLine.Split(';');

            if (parts.Length == 3)
            {
                var name = parts[0];
                var number = ParseCameraNumberFromName(name);

                if (!ParseDouble(parts[1], out var latitude))
                    return null;

                if (!ParseDouble(parts[2], out var longitude))
                    return null;

                return new Camera
                {
                    Number = number,
                    Name = name,
                    Latitude = latitude,
                    Longitude = longitude
                };
            }
            else
            {
                // Incorrectly formatted line, skip!
                return null;
            }
        }

        internal int ParseCameraNumberFromName(string name)
        {
            var match = new Regex(@"^\w+-\w+-(\d+)", RegexOptions.Singleline).Match(name);
            if (match.Success)
                return Convert.ToInt32(match.Groups[1].Value);
            else
                return 0;
        }

        private static bool ParseDouble(string input, out double output)
        {
            return double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out output);
        }
    }
}
