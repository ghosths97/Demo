using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.SPA.Components
{
    public class Spinner
    {
        public bool loaderVisibleState { get; set; }

        public void Hide()
        {
            loaderVisibleState = false;
            SpinnerEvent?.Invoke();
        }

        public void Show()
        {
            loaderVisibleState = true;
            SpinnerEvent?.Invoke();
        }

        private Action SpinnerEvent { get; set; }

        public void AddSpinnerHandler(Action action)
        {
            SpinnerEvent += action;
        }
    }
}
