using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloseMe
{
    class CloseMeContext : ApplicationContext
    {
        Random random;
        bool randomize;
        bool repeat;
        DateTime? start;
        int? wait;
        public CloseMeContext(string[] args)
        {
            Dictionary<string, string> argsmap = ParseArgs(args);
            random = new Random();
            wait = null;
            if (argsmap.ContainsKey("wait"))
            {
                try
                {
                    wait = int.Parse(argsmap["wait"]) * 1000;
                }
                catch
                {
                    Console.WriteLine("Invalid \"repeat\" parameter supplied. Program will exit.");
                    ExitThread();
                }
                if (wait <= 0)
                {
                    Console.WriteLine("Invalid \"repeat\" parameter supplied. Program will exit.");
                    ExitThread();
                }
            }
            start = null;
            if (argsmap.ContainsKey("start"))
            {
                try
                {
                    start = DateTime.ParseExact(argsmap["start"], "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    Console.WriteLine("Invalid \"start\" parameter supplied. Program will exit.");
                    ExitThread();
                }
            }
            repeat = false;
            if (argsmap.ContainsKey("repeat"))
            {
                repeat = true;
            }
            randomize = false;
            if (argsmap.ContainsKey("random"))
            {
                randomize = true;
            }
            DoRun();
        }

        private void DoRun()
        {
            if (start.HasValue)
            {
                RunByTime(start.Value);
            }
            else if (wait.HasValue)
            {
                RunByRepeat();
            }
            else
            {
                StartSpawnSession();
            }
        }

        private void RunByTime(DateTime time)
        {
            TimeSpan waittime = time.TimeOfDay - DateTime.Now.TimeOfDay;
            if (waittime.CompareTo(TimeSpan.Zero) <= 0) waittime += TimeSpan.FromDays(1);
            Timer timer = new Timer();
            timer.Interval = (int)waittime.TotalMilliseconds;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void RunByRepeat()
        {
            if (randomize) RunByRandom();
            else
            {
                Timer timer = new Timer();
                timer.Interval = wait.Value;
                timer.Tick += Timer_Tick;
                timer.Start();
            }
        }

        private void RunByRandom()
        {
            Timer timer = new Timer();
            timer.Interval = random.Next() % wait.Value + 1;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            (sender as Timer).Stop();
            StartSpawnSession();
        }

        private void StartSpawnSession()
        {
            SpawnSession session = new SpawnSession(random);
            session.Complete += Session_Complete;
            session.Start();
        }

        private void Session_Complete(object sender, EventArgs e)
        {
            if (repeat && (start.HasValue || wait.HasValue))
            {
                DoRun();
            }
            else
            {
                ExitThread();
            }
        }

        private Dictionary<string, string> ParseArgs(string[] args)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            foreach(string arg in args)
            {
                int eq = arg.IndexOf('=');
                if (eq == -1)
                {
                    ret.Add(arg, string.Empty);
                }
                else
                {
                    ret.Add(arg.Substring(0, eq), arg.Substring(eq + 1));
                }
            }
            return ret;
        }
    }
}
