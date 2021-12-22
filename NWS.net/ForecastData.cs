namespace NWS.net {
    public class ForecastData {

        public string Day { get; set; } = "Today";
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; } = DateTime.Today;
        public bool Daytime { get; set; } = true;
        public int Temperature { get; set; }= 32;
        public string TemperatureUnit { get; set; } = "F";
        public string TemperatureTrend { get; set; } = string.Empty;
        public int WindSpeed { get; set; } = 0;
        public string WindSpeedUnit { get; set; } = "mph";
        public string WindDirection { get; set; } = "N";
        public string ShortForcast { get; set; } = string.Empty;
        public string DetailedForecast { get; set; } = string.Empty;

        public ForecastData(List<string> Items) {
            foreach(string data in Items) {
                if(data.Contains("\"name\": ")) {
                    Day = data.Split("\"name\": ")[1].Split("\"")[1];
                }else if(data.Contains("\"startTime\": ")) {
                    string time = data.Split("\"startTime\": ")[1].Split("\"")[1];
                    Start = new DateTime(int.Parse(time.Split("-")[0]), 
                        int.Parse(time.Split("-")[1]), int.Parse(time.Split("-")[2].Split("T")[0]),
                        int.Parse(time.Split("-")[2].Split("T")[1].Split(":")[0]), 0, 0);
                } else if(data.Contains("\"endTime\": ")) {
                    string time = data.Split("\"endTime\": ")[1].Split("\"")[1];
                    End = new DateTime(int.Parse(time.Split("-")[0]),
                        int.Parse(time.Split("-")[1]), int.Parse(time.Split("-")[2].Split("T")[0]), 
                        int.Parse(time.Split("-")[2].Split("T")[1].Split(":")[0]), 0, 0);
                } else if(data.Contains("\"isDaytime\": ")) {
                    Daytime = bool.Parse(data.Split("\"isDaytime\": ")[1].Split(",")[0]);
                } else if(data.Contains("\"temperature\": ")) {
                    Temperature = int.Parse(data.Split("\"temperature\": ")[1].Split(",")[0]);
                } else if(data.Contains("\"temperatureUnit\": ")) {
                    TemperatureUnit = data.Split("\"temperatureUnit\": ")[1].Split("\"")[1];
                } else if(data.Contains("\"temperatureTrend\": ")) {
                    try {
                        TemperatureTrend = data.Split("\"temperatureTrend\": ")[1].Split("\"")[1];
                    } catch (IndexOutOfRangeException) { TemperatureTrend = string.Empty; }
                } else if(data.Contains("\"windSpeed\": ")) {
                    WindSpeed = int.Parse(data.Split("\"windSpeed\": ")[1].Split("\"")[1].Split(" ")[0]);
                    WindSpeedUnit = data.Split("\"windSpeed\": ")[1].Split("\"")[1].Split(" ")[1];
                } else if(data.Contains("\"windDirection\" : ")) {
                    WindDirection = data.Split("\"windDirection\": ")[1].Split("\"")[1].Split(" ")[1];
                } else if(data.Contains("\"shortForecast\": ")) {
                    ShortForcast = data.Split("\"shortForecast\": ")[1].Split(",")[0];
                } else if(data.Contains("\"detailedForecast\": ")) {
                    DetailedForecast = data.Split("\"detailedForecast\":")[1].Split("\"")[1].Split(" ")[1];
                }
            }
        }

    }
}
