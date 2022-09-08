namespace NWS.net {

    public class CurrentConditions {
        public static readonly string BASE_API = "https://forecast.weather.gov/MapClick.php?&lat=21.1063&lon=-157.0519&FcstType=json";

        public float Temperature;
        public float DewPoint;
        public float RelativeHumidity;
        public float WindSpeed;
        public float WindDirection;
        public float WindGustSpeed;
        public string CurrentConditon;
        public NWSExtensions.CurrentWeather CurrentWeather;
        public float Visibility;
        public float Pressure;
        public float WindChill;
        public float FeelsLike;

        public CurrentConditions(double[] Location) {
            using WebClient wc = new WebClient();
            wc.Headers.Add("user-agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
            string Response = wc.DownloadString($"https://forecast.weather.gov/MapClick.php?&lat={Location[0]}&lon={Location[1]}&FcstType=json");
            using StringReader parser = new StringReader(Response);
            string currentLine = string.Empty;
            do {
                try {
                    currentLine = parser.ReadLine();
                    if (currentLine.Contains("\"Temp\":")) {
                        Temperature = float.Parse(currentLine.Split(new string[] { "\"Temp\":" }, StringSplitOptions.None)[1].Replace(",\n", "").Replace("\"", ""));
                    } else if (currentLine.Contains("\"Dewp\":")) {
                        DewPoint = float.Parse(currentLine.Split(new string[] { "\"Dewp\":" }, StringSplitOptions.None)[1].Replace(",", "").Replace("\"", ""));
                    } else if (currentLine.Contains("\"Relh\":")) {
                        RelativeHumidity = float.Parse(currentLine.Split(new string[] { "\"Relh\":" }, StringSplitOptions.None)[1].Replace(",", "").Replace("\"", ""));
                    } else if (currentLine.Contains("\"Winds\":")) {
                        WindSpeed = float.Parse(currentLine.Split(new string[] { "\"Winds\":" }, StringSplitOptions.None)[1].Replace(",", "").Replace("\"", ""));
                    } else if (currentLine.Contains("\"Windd\":")) {
                        WindDirection = float.Parse(currentLine.Split(new string[] { "\"Windd\":" }, StringSplitOptions.None)[1].Replace(",", "").Replace("\"", ""));
                    } else if (currentLine.Contains("\"Gust\":")) {
                        WindGustSpeed = float.Parse(currentLine.Split(new string[] { "\"Gust\":" }, StringSplitOptions.None)[1].Replace(",", "").Replace("\"", ""));
                    } else if (currentLine.Contains("\"Weather\":")) {
                        CurrentConditon = currentLine.Split(new string[] { "\"Weather\":" }, StringSplitOptions.None)[1].Replace(",", "").Replace("\"", "");
                        CurrentWeather = (CurrentConditon.ToLower().Trim()) switch {
                            "fair/clear" => NWSExtensions.CurrentWeather.Fair,
                            "a few clouds" => NWSExtensions.CurrentWeather.PartlyCloudy,
                            "partly cloudy" => NWSExtensions.CurrentWeather.PartlyCloudy,
                            "mostly cloudy" => NWSExtensions.CurrentWeather.PartlyCloudy,
                            "overcast" => NWSExtensions.CurrentWeather.Overcast,
                            "fair/clear and windy" => NWSExtensions.CurrentWeather.Fair,
                            "a few clouds and windy" => NWSExtensions.CurrentWeather.PartlyCloudy,
                            "partly cloudy and windy" => NWSExtensions.CurrentWeather.PartlyCloudy,
                            "mostly cloudy and windy" => NWSExtensions.CurrentWeather.PartlyCloudy,
                            "overcast and windy" => NWSExtensions.CurrentWeather.Overcast,
                            "snow" => NWSExtensions.CurrentWeather.Snow,
                            "rain/snow" => NWSExtensions.CurrentWeather.WinteryMix,
                            "rain/sleet" => NWSExtensions.CurrentWeather.WinteryMix,
                            "snow/sleet" => NWSExtensions.CurrentWeather.WinteryMix,
                            "freezing rain" => NWSExtensions.CurrentWeather.WinteryMix,
                            "rain/freezing rain" => NWSExtensions.CurrentWeather.WinteryMix,
                            "freezing rain/snow" => NWSExtensions.CurrentWeather.WinteryMix,
                            "sleet" => NWSExtensions.CurrentWeather.WinteryMix,
                            "rain" => NWSExtensions.CurrentWeather.Rain,
                            "rain showers (high cloud cover)" => NWSExtensions.CurrentWeather.Rain,
                            "rain showers (low cloud cover)" => NWSExtensions.CurrentWeather.Rain,
                            "thunderstorm (high cloud cover)" => NWSExtensions.CurrentWeather.Storm,
                            "thunderstorm (medium cloud cover)" => NWSExtensions.CurrentWeather.Storm,
                            "thunderstorm (low cloud cover)" => NWSExtensions.CurrentWeather.Storm,
                            "tornado" => NWSExtensions.CurrentWeather.Tornado,
                            "hurricane conditions" => NWSExtensions.CurrentWeather.Hurricane,
                            "tropical storm conditions" => NWSExtensions.CurrentWeather.TropicalStorm,
                            "dust" => NWSExtensions.CurrentWeather.Dust,
                            "smoke" => NWSExtensions.CurrentWeather.Dust,
                            "haze" => NWSExtensions.CurrentWeather.Fog,
                            "hot" => NWSExtensions.CurrentWeather.Fair,
                            "cold" => NWSExtensions.CurrentWeather.Fair,
                            "blizzard" => NWSExtensions.CurrentWeather.Blizzard,
                            "thunderstorm light rain and windy" => NWSExtensions.CurrentWeather.Storm,
                            "thunderstorm rain and windy" => NWSExtensions.CurrentWeather.Storm,
                            "thunderstorm heavy rain and windy" => NWSExtensions.CurrentWeather.Storm,
                            "thunderstorm" => NWSExtensions.CurrentWeather.Storm,
                            _ => NWSExtensions.CurrentWeather.Fair
                        };
                    } else if (currentLine.Contains("\"Visibility\":")) {
                        Visibility = float.Parse(currentLine.Split(new string[] { "\"Visibility\":" }, StringSplitOptions.None)[1].Replace(",", "").Replace("\"", ""));
                    } else if (currentLine.Contains("\"SLP\":")) {
                        Pressure = float.Parse(currentLine.Split(new string[] { "\"SLP\":" }, StringSplitOptions.None)[1].Replace(",", "").Replace("\"", ""));
                    } else if (currentLine.Contains("\"WindChill\":")) {
                        try {
                            WindChill = float.Parse(currentLine.Split(new string[] { "\"WindChill\":" }, StringSplitOptions.None)[1].Replace(",", "").Replace("\"", ""));
                        } catch { WindChill = float.NaN; }
                    }
                } catch { };
            } while (currentLine != null);
            FeelsLike = !float.IsNaN(WindChill) ? WindChill : NWSExtensions.CalculateHeatIndex(Temperature, RelativeHumidity);
        }

        public override string ToString() {
            return $"{Temperature}\n" + $"{DewPoint}\n" + $"{RelativeHumidity}\n" + $"{WindSpeed}\n" + $"{WindDirection}\n" + $"{WindGustSpeed}\n" + $"{CurrentConditon}\n" + $"{Pressure}\n" + $"{WindChill}\n" + $"{FeelsLike}";
        }
    }
}