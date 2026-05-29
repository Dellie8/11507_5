using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Levels
{
    class Sensor
    {
        public event Action<string, DateTime> OnAlert;

        public void Trigger(string message)
        {
            OnAlert?.Invoke(message, DateTime.Now);
        }
    }

    class Siren
    {
        public void Activate(string message, DateTime time)
        {
            Console.WriteLine(
                $"ВКЛЮЧЕНА СИРЕНА: {message}");
        }
    }

    class Logger
    {
        public ObservableCollection<string> Logs
            = new ObservableCollection<string>();

        public void Log(string message, DateTime time)
        {
            Logs.Add($"[{time}] {message}");
        }
    }
}