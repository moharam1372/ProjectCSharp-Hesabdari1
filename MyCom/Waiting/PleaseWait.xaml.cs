using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Timer = System.Timers.Timer;

namespace MyCom.Waiting
{
    /// <summary>
    /// Interaction logic for PleaseWait.xaml
    /// </summary>
    public partial class PleaseWait : Window
    {

        // public string _gameName;
        // public long _barMax;    
        public long _barValue;
        public PleaseWait(string title, long barMax)
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                this.Topmost = true;
                this.WindowStyle = WindowStyle.None;
                _lblTitleGame.Content = title;
                _barWait.Value = 0;
                _barWait.Maximum = barMax;
                Timer timer = new Timer();
                timer.Interval = 25;
                timer.Start();

                timer.Elapsed += (s1, e1) =>
                {
                    _barWait.Dispatcher.Invoke(DispatcherPriority.Background, () =>
                    {
                        if (_barWait.Value <= _barValue)
                        {
                            _barWait.Value = _barValue;
                            Thread.Sleep(1);
                        }
                        if (_barWait.Value >= barMax)
                        {
                            _barWait.Value = 100;
                            
                            timer.Dispose();
                            Close();
                        }
                    });
                };
            };


        }

        private void _barWait_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                long getPercent = Convert.ToInt32((e.NewValue * 100) / _barWait.Maximum);
                _lblShowPercent.Content = getPercent + "%";
            }
            catch (Exception exception)
            {

            }
        }
    }
}
