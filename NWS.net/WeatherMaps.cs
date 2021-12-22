namespace NWS.net {
    public class WeatherMaps {

        [Description("Gets the image for today's national forecast.")]
        public static string National_Forecast_Day1 { get; } = "https://www.wpc.ncep.noaa.gov/noaa/noaad1.gif?1640122487";

        [Description("Gets the image for tomorrow's national forecast.")]
        public static string National_Forecast_Day2 { get; } = "https://www.wpc.ncep.noaa.gov/noaa/noaad2.gif?1640122487";

        [Description("Gets the image for the day after tomorrow's national forecast.")]
        public static string National_Forecast_Day3 { get; } = "https://www.wpc.ncep.noaa.gov/noaa/noaad3.gif?1640122487";

        [Description("Gets the current radar for the forecast area.")]
        public string Local_Radar { get { return "https://radar.weather.gov/ridge/lite/" + RadarStation + "_loop.gif"; } }

        string RadarStation { get; set; } = "KLWX";

        public WeatherMaps(string RadarStation) { this.RadarStation = RadarStation; }

        public static string Convective_Outlook { get; } = "https://www.spc.noaa.gov/products/activity_loop.gif";
        public static string National_Warnings { get; } = "https://forecast.weather.gov/wwamap/png/US.png";

        public string GetGraphicalHazard(int Day, string Hazard) {
            string station = RadarStation[1..].ToLower();
            switch (Hazard.ToUpper()) {
                case "COLD":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/ExcessiveColdDay{0}.png", Day, station);
                case "HEAT":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/ExcessiveHeatDay{0}.png", Day, station);
                case "FIRE":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/FireWeatherDay{0}.png", Day, station);
                case "GFDI":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/FireWxGFDIDay{0}.png", Day, station);
                case "RAIN":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/FloodingDay{0}.png", Day, station);
                case "FOG":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/FogDay{0}.png", Day, station);
                case "HAIL":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/HailDay{0}.png", Day, station);
                case "LIGHTNING":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/LightningDay{0}.png", Day, station);
                case "NONT-STORM WIND":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/NonThunderstormWindDay{0}.png", Day, station);
                case "ICE":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/IceAccumulationDay{0}.png", Day, station);
                case "SNOW":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/SnowSleetDay{0}.png", Day, station);
                case "SPOTTER":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/SpotterOutlookDay{0}.png", Day, station);
                case "SEVERE T-STORM":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/SevereThunderstormsDay{0}.png", Day, station);
                case "T-STORM WIND":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/ThunderstormWindGustDay{0}.png", Day, station);
                case "TORNADO":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/TornadoDay{0}.png", Day, station);
                case "COASTAL FLOOD":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/CoastalFloodDay{0}.png", Day, station);
                case "SPRAY":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/FreezingSprayDay{0}.png", Day, station);
                case "MARINE":
                    return string.Format("https://www.weather.gov/images/{1}/EHWO/Day{0}/MarineHazardDay{0}.png", Day, station);
                default:
                    break;
            }
            return string.Empty;
        }

    }
}
