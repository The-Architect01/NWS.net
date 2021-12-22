namespace NWS.net {
    public class Alerts {

        public Alert[] ActiveAlerts { get; private set; }

        string API_Base { get; } = "https://api.weather.gov/alerts/active?point=";

        public Alerts(double[] Location) {
            using WebClient wc = new();
            wc.Headers.Add("user-agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
            string Response = wc.DownloadString(API_Base + Location[0] + "," + Location[1]);
            using StringReader parser = new(Response);
            string currentLine = string.Empty;
            List<string> dataInput = new();
            List<Alert> data = new();
            bool start = false;
            do {
                try {
                    currentLine = parser.ReadLine();
                    if (currentLine.Contains("\"id\":")) { start = true; }
                    if (currentLine.Contains("},") || currentLine.Contains("],")) {
                        start = false;
                        data.Add(new Alert(dataInput));
                        dataInput.Clear();
                    }
                    if (start) {
                        dataInput.Add(currentLine);
                    }
                } catch (NullReferenceException) { }
            } while (currentLine != null);
            ActiveAlerts = data.ToArray();
        }

    }
}
