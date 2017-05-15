using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloseMe
{
    class SpawnSession
    {
        HashSet<MainForm> displayed_forms;
        bool time_is_up;
        int factor;
        Random random;
        public SpawnSession(Random _random)
        {
            displayed_forms = new HashSet<MainForm>();
            time_is_up = false;
            factor = 2;
            random = _random;
        }

        public void Start()
        {
            Timer stoptimer = new Timer();
            stoptimer.Interval = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;
            stoptimer.Tick += Stoptimer_Tick;
            stoptimer.Start();
            MakeNewForm();
        }

        private void MakeNewForm()
        {
            const int length = 200;
            MainForm form = new MainForm();
            displayed_forms.Add(form);
            Rectangle screenbounds = Screen.FromControl(form).Bounds;
            if (screenbounds.Width > length)
            {
                form.Left = random.Next() % (screenbounds.Width - length + 1);
            }
            else
            {
                form.Left = 0;
            }
            if (screenbounds.Height > length)
            {
                form.Top = random.Next() % (screenbounds.Height - length + 1);
            }
            else
            {
                form.Top = 0;
            }
            form.BackColor = Color.FromArgb(random.Next() % 255, random.Next() % 255, random.Next() % 255);
            form.FormClosed += Form_FormClosed;
            form.Show();
        }

        private void Stoptimer_Tick(object sender, EventArgs e)
        {
            (sender as Timer).Stop();
            time_is_up = true;
            List<MainForm> formlist = new List<MainForm>(displayed_forms);
            foreach (MainForm form in formlist)
            {
                form.Close();
            }
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            displayed_forms.Remove(sender as MainForm);
            if (!time_is_up)
            {
                for(int i = 0; i < factor; ++i)
                {
                    MakeNewForm();
                }
                factor *= 2;
            }
            if (displayed_forms.Count == 0)
            {
                OnComplete(new EventArgs());
            }
        }

        protected virtual void OnComplete(EventArgs e)
        {
            if (Complete != null)
            {
                Complete(this, e);
            }
        }

        public event EventHandler Complete;
    }
}
