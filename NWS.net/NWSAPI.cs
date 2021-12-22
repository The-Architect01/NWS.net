﻿global using System;
global using System.ComponentModel;
global using System.IO;
global using System.Net;
global using System.Collections.Generic;

namespace NWS.net {
    public class NWSAPI {

        #region Non Visible
        static readonly double[] DEFAULT = new double[] { 38.9072, -77.0369 };
        private double[] Position { get; set; } = new double[2];
        #endregion

        public Location Location { get { return new Location(Position); } }
        public Forecast Forecast { get { return new Forecast(Location.ForecastURL); } }
        public Alerts Alerts { get { return new Alerts(Position); } }
        public WeatherMaps WeatherMap { get { return new WeatherMaps(Location.RadarStation); } }

        public NWSAPI(string IP) {
            using WebClient wc = new();
            wc.Headers.Add("user-agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
            string LOCATION_API = wc.DownloadString("http://ip-api.com/json/" + IP);
            try {
                Position[0] = double.Parse(LOCATION_API.Split("\"lon\":")[1].Split(",")[0]);
                Position[1] = double.Parse(LOCATION_API.Split("\"lat\":")[1].Split(",")[0]);
            } catch (Exception) {
                Position = DEFAULT;
            }
        }

        public void SetPosition(double Latitude, double Longitude) {
            Position[0] = Latitude;
            Position[1] = Longitude;
        }
        public void SetPosition(string Latitude, string Longitude) {
            try {
                Position[0] = int.Parse(Latitude);
                Position[1] = int.Parse(Longitude);
            } catch (Exception) {
                Position = DEFAULT;
            }
        }
        public void SetPosition(string IP) {
            using WebClient wc = new();
            wc.Headers.Add("user-agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
            string LOCATION_API = wc.DownloadString("http://ip-api.com/json/" + IP);
            try {
                Position[0] = double.Parse(LOCATION_API.Split("\"lon\":")[1].Split(",")[0]);
                Position[1] = double.Parse(LOCATION_API.Split("\"lat\":")[1].Split(",")[0]);
            } catch (Exception) {
                Position = DEFAULT;
            }
        }
        public void SetPosition() {
            using WebClient wc = new();
            wc.Headers.Add("user-agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
            string LOCATION_API = wc.DownloadString("http://ip-api.com/json/");
            try {
                Position[0] = double.Parse(LOCATION_API.Split("\"lon\":")[1].Split(",")[0]);
                Position[1] = double.Parse(LOCATION_API.Split("\"lat\":")[1].Split(",")[0]);
            } catch (Exception) {
                Position = DEFAULT;
            }
        }

        public void Reset() {
            Position = DEFAULT;
        }
        
        public NWSAPI() {
            Position = DEFAULT;
        }

        public NWSAPI(double Latitude, double Longitude) {
            Position[0] = Latitude;
            Position[1] = Longitude;
        }

        public NWSAPI(string Latitude, string Longitude) {
            try {
                Position[0] = int.Parse(Latitude);
                Position[1] = int.Parse(Longitude);
            } catch (Exception) {
                Position = DEFAULT;
            }
        }
    }
}
