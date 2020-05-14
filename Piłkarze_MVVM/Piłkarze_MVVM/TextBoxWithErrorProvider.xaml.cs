using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Piłkarze_MVVM
{
    /// <summary>
    /// Interaction logic for TextBoxWithErrorProvider.xaml
    /// </summary>
    public partial class TextBoxWithErrorProvider : UserControl
    {
        public TextBoxWithErrorProvider()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent TextChangedEvent =
        EventManager.RegisterRoutedEvent("TabItemSelected",
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(TextBoxWithErrorProvider));

        public event RoutedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }

        void RaiseTextChanged()
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(TextBoxWithErrorProvider.TextChangedEvent);
            RaiseEvent(newEventArgs);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(TextBoxWithErrorProvider),
                new FrameworkPropertyMetadata(null)
            );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender is TextBox))
                return;

            TextBox tb = (TextBox)sender;

            if (tb.Text.Equals(""))
            {
                tb.BorderBrush = Brushes.Red;
            }
            else
            {
                tb.BorderBrush = Brushes.Black;
            }
            RaiseTextChanged();
        }
    }
}
