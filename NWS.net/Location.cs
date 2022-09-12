namespace NWS.net {
    public class Location {

        public string City { get; private set; } = "Washington";
        public string State { get; private set; } = "DC";
        public string County { get; private set; } = "District of Columbia";

        public TimeZoneInfo TimeZone { get; private set; } = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneTranslate.OlsenToWin32["America/New_York"]);
        public string RadarStation { get; private set; } = "KLWX";
        public string ForecastURL { get; private set; }

        public int[] GridCoordinates { get; private set; } = new int[] { 96, 70 };
        public string Zone { get; private set; } = "DCZ001";

        string BASE_API { get; } = "https://api.weather.gov/points/";
        string API_RESPONSE { get; set; }

        public Location(double[] GPS) {
            using WebClient wc = new();
            wc.Headers.Add("user-agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
            API_RESPONSE = wc.DownloadString(BASE_API + GPS[0] + "," + GPS[1]);
            using StringReader parser = new(API_RESPONSE);
            string currentLine = string.Empty;

            do {
                currentLine = parser.ReadLine();
                try {
                    if (currentLine.Contains("\"gridX\": ")) {
                        GridCoordinates[0] = int.Parse(currentLine.Split("\"gridX\": ")[1].Split(",")[0]);
                    } else if (currentLine.Contains("\"gridY\": ")) {
                        GridCoordinates[1] = int.Parse(currentLine.Split("\"gridY\": ")[1].Split(",")[0]);
                    } else if (currentLine.Contains("\"forecast\": ")) {
                        ForecastURL = currentLine.Split("\"forecast\": ")[1].Split("\"")[1];
                    } else if (currentLine.Contains("\"city\": ")) {
                        City = currentLine.Split("\"city\": ")[1].Split("\"")[1];
                    } else if (currentLine.Contains("\"state\": ")) {
                        State = currentLine.Split("\"state\": ")[1].Split("\"")[1];
                    } else if (currentLine.Contains("\"county\": ")) {
                        try {
                            County = GetCounty(currentLine.Split("\"county\": ")[1].Split("\"")[1]);
                        } catch (IndexOutOfRangeException) { }
                    } else if (currentLine.Contains("\"timeZone\": ")) {
                        string timeZone = currentLine.Split("\"timeZone\": ")[1].Split("\"")[1];
                        TimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneTranslate.OlsenToWin32[timeZone]);
                    } else if (currentLine.Contains("\"radarStation\": ")) {
                        RadarStation = currentLine.Split("\"radarStation\": ")[1].Split("\"")[1];
                    } else if (currentLine.Contains("\"forecastZone\": ")) {
                        Zone = currentLine.Split("\"forecastZone\": ")[1].Split("\"")[1].Split("/")[5];
                    }
                } catch (NullReferenceException) { }
            } while (currentLine != null);

        }
        private static string GetCounty(string URL) {
            using WebClient wc = new();
            wc.Headers.Add("user-agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
            string Response = wc.DownloadString(URL);
            using StringReader parser = new(Response);
            string currentLine = string.Empty;

            do {
                currentLine = parser.ReadLine();
                if(currentLine.Contains("\"name\": ")) {
                    return currentLine.Split("\"name\": ")[1].Split("\"")[1];
                }
            } while (currentLine != null);

            return null;
        }
    }
}
