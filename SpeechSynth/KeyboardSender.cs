
using Microsoft.Extensions.Logging;

namespace SpeechSynth
{
    public class KeyboardSender
    {
        Task animator;
        bool animate = true;
        int counter = 0;

        //readonly ILogger _logger;

        public KeyboardSender()
        {
        }

        public Task Send(string st)
        {
            foreach (var c in st)
            {
                try
                {
                    SendKeys.SendWait(c.ToString());
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex.Message, ex);
                }
            }

            return Task.CompletedTask;
        }

        public Task AnimateListening()
        {
            animate = true;
            counter = 0;
            animator = new Task(() => 
            {
                while (animate)
                {
                    if(counter < 3)
                    {
                        SendKeys.SendWait(".");
                        counter++;
                    }
                    else
                    {
                        SendKeys.SendWait("{BS}{BS}");
                        counter = 1;
                    }
                    Thread.Sleep(300);
                }
            });
            animator.Start();

            return Task.CompletedTask;
        }

        public void Endlistening()
        {
            animate = false;
            var st = "";
            for(int i = 0; i < counter; i++)
            { st += "{BS}"; }
            SendKeys.SendWait(st);
        }
    }
}
