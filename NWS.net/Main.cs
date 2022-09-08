using System.Threading;

class Program {

    public static NWS.net.NWSAPI API;

    static float Lat;
    static float Lon;

    static void Main(string[] args) {
        if(args.Length != 3) { Console.WriteLine("You must enter 3 arguments"); return; }
        Lat = float.Parse(args[0]);
        Lon = float.Parse(args[1]);
        int Update = int.Parse(args[2]);

        System.Timers.Timer T = new System.Timers.Timer(1000 * Update);
        Console.Title = "NWS API";
        Console.WriteLine($"API set to {Lat}∘N, {Lon * -1}∘W\n");

        Console.WriteLine("Starting Timer...");
        Console.WriteLine($"Timer set to fire every {Update / 60} minutes\n");
        T.AutoReset = true;
        T.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e) { Program.Update(); };
        T.Start();
        Program.Update();
        Animate();
    }

    static void Animate() {
        int counter = 0;
        int Delay = 300;
        string[] Sequence = { ".   ", "..  ","... ","...."};
        
        while (true) {
            counter++;
            Thread.Sleep(Delay);
            int Val = counter % 4;
            string Msg = Sequence[Val];
            int len = Msg.Length;
            Console.Write(Msg);
            Console.SetCursorPosition(Console.CursorLeft - len, Console.CursorTop);
        }
    }

    static void Update() {
        Console.WriteLine($"Fired {DateTime.Now}");
        API = new NWS.net.NWSAPI(Lat, Lon);
        Write();
    }

    static void Write() {
        File.WriteAllText(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) +"\\Output.txt", API.ToString());
    }

}