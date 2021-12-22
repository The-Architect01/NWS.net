namespace NWS.net {
    public class Alert {

        public DateTime Effective { get; private set; }
        public DateTime Expires { get; private set; }
        public string MessageType { get; private set; }
        public string Severity { get; private set; }
        public string Certainty { get; private set; }
        public string Urgency { get; private set; }
        public string Event { get; private set; }
        public string SendingOffice { get; private set; }
        public string Headline { get; private set; }
        public string Description { get; private set; }
        public string Instructions { get; private set; }
        public string Response { get; private set; }
        public string MessageHeadline { get; private set; }
        public List<string> AffectedZones { get; private set; } = new();

        public Alert(List<string> Data) {
            bool Zone = false;
            bool NWSHead = false;
            foreach (string dataPoint in Data) {
                if (dataPoint.Contains("\"effective\": ")) {
                    string time = dataPoint.Split("\"effective\": ")[1].Split("\"")[1];
                    Effective = new DateTime(int.Parse(time.Split("-")[0]),
                        int.Parse(time.Split("-")[1]), int.Parse(time.Split("-")[2].Split("T")[0]),
                        int.Parse(time.Split("-")[2].Split("T")[1]), 0, 0);
                } else if (dataPoint.Contains("\"expires\": ")) {
                    string time = dataPoint.Split("\"expires\": ")[1].Split("\"")[1];
                    Expires = new DateTime(int.Parse(time.Split("-")[0]),
                        int.Parse(time.Split("-")[1]), int.Parse(time.Split("-")[2].Split("T")[0]),
                        int.Parse(time.Split("-")[2].Split("T")[1]), 0, 0);
                } else if (dataPoint.Contains("\"messageType\": ")) {
                    MessageType = dataPoint.Split("\"messageType\": ")[1].Split("\"")[1];
                } else if (dataPoint.Contains("\"severity\": ")) {
                    Severity = dataPoint.Split("\"severity\": ")[1].Split("\"")[1];
                } else if (dataPoint.Contains("\"certainty\": ")) {
                    Certainty = dataPoint.Split("\"certainty\": ")[1].Split("\"")[1]; 
                } else if (dataPoint.Contains("\"urgency\": ")) {
                    Urgency = dataPoint.Split("\"urgency\": ")[1].Split("\"")[1];
                } else if (dataPoint.Contains("\"event\":")) {
                    Event = dataPoint.Split("\"event\": ")[1].Split("\"")[1]; 
                } else if (dataPoint.Contains("\"headline\":")) {
                    Headline = dataPoint.Split("\"headline\": ")[1].Split("\"")[1];
                } else if (dataPoint.Contains("\"description\": ")) {
                    Description = dataPoint.Split("\"description\": ")[1].Split("\"")[1];
                } else if (dataPoint.Contains("\"instructions\": ")) {
                    Instructions = dataPoint.Split("\"instructions\": ")[1].Split("\"")[1];
                } else if (dataPoint.Contains("\"NWSheadline\": ")) {
                    NWSHead = true;
                } else if (dataPoint.Contains("\"UGC\" :")) {
                    Zone = true;
                } else if (Zone) {
                    AffectedZones.Add(dataPoint.Split("\"")[1]);
                } else if (NWSHead) {
                    MessageHeadline = dataPoint.Split("\"")[1];
                } else if (dataPoint.Contains("],")) {
                    Zone = false;
                    NWSHead = false;
                }
            }
        }

    }
}
