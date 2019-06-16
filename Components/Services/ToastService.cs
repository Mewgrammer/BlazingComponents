using System;
using System.Timers;

namespace BlazingComponents.Lib.Services
{
    public enum ToastLevel
    {
        Info,
        Success,
        Warning,
        Error
    }
    public class ToastService : IDisposable
    {
        public event Action<string, ToastLevel> OnShow;
        public event Action OnHide;
        private Timer _countdown;

        public void ShowToast(string message, ToastLevel level)
        {
            OnShow?.Invoke(message, level);
            StartCountdown();
        }

        private void StartCountdown()
        {
            SetCountdown();

            if (_countdown.Enabled)
            {
                _countdown.Stop();
                _countdown.Start();
            }
            else
            {
                _countdown.Start();
            }
        }

        private void SetCountdown()
        {
            if (_countdown != null) return;
            _countdown = new Timer(5000);
            _countdown.Elapsed += HideToast;
            _countdown.AutoReset = false;
        }

        private void HideToast(object source, ElapsedEventArgs args)
        {
            OnHide?.Invoke();
        }

        public void Dispose()
        {
            _countdown?.Dispose();
        }
    }
}
