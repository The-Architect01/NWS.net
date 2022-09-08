namespace NWS.net {
    public static class NWSExtensions {

        public static readonly string[] Special = { "Tsunami Warning", "Tornado Warning", "Flash Flood Warning", "Hurricane Warning", "Typhoon Warning", "Dust Storm Warning", "Extreme Wind Warning", "Blizzard Warning", "Ice Storm Warning", "Tsunami Watch", "Tornado Watch", "Flash Flood Watch", "Hurricane Watch", "Typhoon Watch", "Dust Storm Watch", "Extreme Wind Watch", "Blizzard Watch", "Ice Storm Watch", "Severe Thunderstorm Watch", "Severe Thunderstorm Warning","Tropical Storm Warning", "Tropical Storm Watch" };

        public static string GetSpecial(string Event) {
            return Event switch {
                "Tsunami Warning"=> "Tsunami",
                "Tornado Warning"=> "Tornado",
                "Tropical Storm Warning"=> "TropicalStorm",
                "Flash Flood Warning"=> "Flood",
                "Hurricane Warning"=> "Hurricane",
                "Typhoon Warning"=> "Hurricane",
                "Dust Storm Warning"=> "Dust",
                "Extreme Wind Warning"=> "Wind",
                "Blizzard Warning"=> "Blizzard",
                "Ice Storm Warning"=> "Ice",
                "Tsunami Watch"=> "Tsunami",
                "Tornado Watch"=> "SevereStorm",
                "Flash Flood Watch"=> "Flood",
                "Hurricane Watch"=> "Hurricane",
                "Typhoon Watch"=> "Hurricane",
                "Tropical Storm Watch" => "TropicalStorm",
                "Dust Storm Watch" => "Dust",
                "Extreme Wind Watch"=> "Wind",
                "Blizzard Watch"=> "Blizzard",
                "Ice Storm Watch"=> "Ice",
                "Severe Thunderstorm Watch"=> "SevereStorm",
                "Severe Thunderstorm Warning"=> "SevereStorm",
                _ => ""
            };
        }

        public static readonly string[] EmergencyConditions = { "Tsunami Warning", "Tornado Warning", "Flash Flood Warning", "Hurricane Warning", "Typhoon Warning", "Dust Storm Warning", "Extreme Wind Warning", "Blizzard Warning", "Ice Storm Warning" };

        public static T[] ReplaceAll<T>(this T[] array, T Search, T Replace) {
            List<T> ListArray = new List<T>();
            foreach (T item in array) {
                if (item.Equals(Search)) { ListArray.Add(Replace); } else { ListArray.Add(item); }
            }
            return ListArray.ToArray();
        }

        public static T[] NumbersOnly<T>(this T[] array, T Replacement) {
            List<T> ListArray = new List<T>();
            foreach (T item in array) {
                try {
                    float.Parse(item.ToString());
                    ListArray.Add(item);
                } catch {
                    ListArray.Add(Replacement);
                }
            }
            return ListArray.ToArray();
        }

        public static float CalculateHeatIndex(float Temperature, float RelativeHumidity) {
            float alpha = -42.379f + (2.04901523f * Temperature);
            float beta = (10.1433127f * RelativeHumidity);
            float gamma = -(.22475541f * Temperature * RelativeHumidity);
            float delta = (float)-((6.83783f * Math.Pow(10, -3)) * Math.Pow(Temperature, 2));
            float epsilon = -(float)((5.481717f * Math.Pow(10, -2)) * Math.Pow(RelativeHumidity, 2));
            float zeta = (float)((1.22874f * Math.Pow(10, -3)) * Math.Pow(Temperature, 2) * RelativeHumidity);
            float eta = (float)((8.5282f * Math.Pow(10, -4)) * Temperature * Math.Pow(RelativeHumidity, 2));
            float theta = (float)-((1.99f * Math.Pow(10, -6)) * Math.Pow(Temperature, 2) * Math.Pow(RelativeHumidity, 2));
            return alpha + beta + gamma + delta + epsilon + zeta + eta + theta;
        }

        public static float CalculateWindChill(float Temperature, float WindSpeed) {
            float alpha = 35.74f + (.6215f * Temperature);
            float beta = (float)-(35.75f * Math.Pow(WindSpeed, .16f));
            float delta = (float)(.4275f * Temperature * Math.Pow(WindSpeed, .16f));
            return alpha + beta + delta;
        }

        public static CurrentWeather EmergencyType(string Weather) {
            return Weather switch {
                "Tsunami Warning" => CurrentWeather.Tsunami,
                "Tornado Warning" => CurrentWeather.Tornado,
                "Flash Flood Warning" => CurrentWeather.Flood,
                "Hurricane Warning" => CurrentWeather.Hurricane,
                "Typhoon Warning" => CurrentWeather.Hurricane,
                "Dust Storm Warning" => CurrentWeather.DustStorm,
                "Extreme Wind Warning" => CurrentWeather.Wind,
                "Blizzard Warning" => CurrentWeather.Blizzard,
                "Ice Storm Warning" => CurrentWeather.Blizzard,
                _ => CurrentWeather.MultipleAlerts
            };
        }

        public static CurrentWeather CalculateWeatherType(string WeatherType1, string WeatherType2 = "") {
            if (EmergencyConditions.Contains(WeatherType1)) { return EmergencyType(WeatherType1); }
            if (!WeatherType1.Contains("value weather-type=\"")) { return CurrentWeather.Unknown; }
            WeatherType1 = WeatherType1.Replace("value weather-type=\"", "");
            WeatherType1 = WeatherType1.Remove(WeatherType1.IndexOf("\""));
            WeatherType1 = WeatherType1.Substring(0, 1).ToUpper() + WeatherType1.Substring(1);
            if (WeatherType2.Contains("value additive=\"and\" weather-type=\"")) {
                WeatherType2 = WeatherType2.Replace("value additive=\"and\" weather-type=\"", "");
                WeatherType2 = WeatherType2.Remove(WeatherType2.IndexOf("\""));
                WeatherType2 = WeatherType2.Substring(0, 1).ToUpper() + WeatherType2.Substring(1);
            } else { WeatherType2 = ""; }
            if (WeatherType2 != "") {
                foreach (string Type in Enum.GetNames(typeof(CurrentWeather))) {
                    if (WeatherType2.Contains(Type)) { return (CurrentWeather)Enum.Parse(typeof(CurrentWeather), Type); }
                }
            }
            foreach (string Type in Enum.GetNames(typeof(CurrentWeather))) {
                if (WeatherType1.Contains(Type)) { return (CurrentWeather)Enum.Parse(typeof(CurrentWeather), Type); }
            }
            return CurrentWeather.Fair;
        }

        [Serializable]
        public struct HighLow {
            public float High;
            public float Low;
            public HighLow(float H, float L) {
                High = float.IsNaN(H) ? H : H > L ? H : L;
                Low = float.IsNaN(H) ? L : H > L ? L : H;
            }
        }

        [Serializable]
        public enum CurrentWeather {
            Unknown,
            Fair,
            PartlyCloudy,
            Overcast,
            Snow,
            Rain,
            WinteryMix,
            Storm,
            Tornado,
            Hurricane,
            TropicalStorm,
            Dust,
            Fog,
            Blizzard,
            Flood,
            Tsunami,
            DustStorm,
            MultipleAlerts,
            Wind,
        }
    }
}