namespace NWS.net {
    public class Forecast {

        public ForecastData[] ForecastData { get; private set; }

        public Forecast(string URL) {
            using WebClient wc = new();
            wc.Headers.Add("user-agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
            string Response = wc.DownloadString(URL);
            using StringReader parser = new(Response);
            string currentLine = string.Empty;
            List<string> dataInput = new();
            List<ForecastData> data = new();
            bool start = false;
            do {
                currentLine = parser.ReadLine();
                try {
                    if (currentLine.Contains("\"number\":")) { start = true; }
                    if (currentLine.Contains("},") || currentLine.Contains("]")) {
                        if (start) {
                            start = false;
                            data.Add(new ForecastData(dataInput));
                            dataInput.Clear();
                        }
                    }
                    if (start) {
                        dataInput.Add(currentLine);
                    }
                } catch (NullReferenceException) { }
            } while (currentLine != null);
            ForecastData = data.ToArray();
        }

    }
}
