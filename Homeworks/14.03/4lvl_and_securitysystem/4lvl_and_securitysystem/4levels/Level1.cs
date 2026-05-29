using System;

namespace Levels
{
    public delegate void LogHandler(string message);

    class OrderProcessor
    {
        public event LogHandler Logger;

        public void Process()
        {
            Logger?.Invoke("Заказ принят");
            Logger?.Invoke("Платеж прошел");
            Logger?.Invoke("Заказ отправлен");
        }
    }
}