using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Kalkulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double result = 0d;
        string operandOrOperator = "";
        bool operandOrOperatorPressed = false;
        bool equalSignPressed = false;
        bool equalsClickCalledFromOperator = false;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (display.Content.ToString().Equals("Dzielenie przez zero"))
            {
                Clear_Click(this, null);
            }

            // usunięcie początkowego zera z wyświetlacza
            if (display.Content.ToString().Equals("0") || operandOrOperatorPressed)
            {
                display.Content = "";
            }

            // po kliknięciu znaku = wyświetli się wynik. Jeśli wtedy kliknę jakąś cyfrę, to wynik zostanie wykasowany
            // i na wyświetlaczu pojawi się kliknięta cyfra. Np. jeśli wykonam działanie 2 + 3 = 5, a następnie kliknę 8, to
            // na wyświetlaczu pojawi się sama ósemka, nie zostanie ona dopisana do wyniku poprzedniego działania
            if (equalSignPressed)
            {
                if (button.Content.ToString().Equals("."))
                {
                    display.Content = "0" + button.Content.ToString();
                }
                else
                {
                    display.Content = button.Content.ToString();
                }
                equalSignPressed = false;
            }

            // jeżeli na wyświetlaczu już znajduje się kropka, to nie dodaję kolejnej
            else if (button.Content.ToString().Equals("."))
            {
                if (display.Content.Equals(""))
                {
                    display.Content = "0" + button.Content.ToString();
                }
                else if (!display.Content.ToString().Contains(".") && !equalSignPressed)
                {
                    display.Content = display.Content.ToString() + button.Content.ToString();
                }
            }

            // zabezpieczenie przed wprowadzaniem liczb typu 007
            else if (button.Content.ToString().Equals("0"))
            {
                if (!display.Content.ToString().StartsWith("0") || display.Content.ToString().Contains(".") && !equalSignPressed)
                {
                    display.Content = display.Content.ToString() + button.Content.ToString();
                }
            }
            else
            {
                display.Content = display.Content.ToString() + button.Content.ToString();
            }
            operandOrOperatorPressed = false;
        }


        private void Operator_Click(object sender, RoutedEventArgs e)
        {

            Button button = (Button)sender;

            if (display.Content.ToString().Equals("Dzielenie przez zero"))
            {
                Clear_Click(this, null);
            }

            if (!operandOrOperatorPressed)
            {
                equalsClickCalledFromOperator = true;
                /* zasymulowanie wciśnięcia przycisku =, 
                   dzięki czemu możliwe jest ciągłe wykonywanie operacji bez potrzeby klikania przycisku = */
                Equals_Click(this, null);
                equalsClickCalledFromOperator = false;
            }

            operandOrOperator = button.Content.ToString();

            if (Double.TryParse(display.Content.ToString().Replace('.', ','), out result))
            {
                equation.Content = result.ToString().Replace(',', '.') + " " + operandOrOperator;
            }
            operandOrOperatorPressed = true;
        }


        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (display.Content.ToString().Equals("Dzielenie przez zero"))
            {
                Clear_Click(this, null);
            }

            if (!equalsClickCalledFromOperator)
            {
                equalSignPressed = true;
            }

            equation.Content = " ";
            double currentValue = Double.Parse(display.Content.ToString().Replace('.', ','));

            switch (operandOrOperator)
            {
                case "+":
                    result += currentValue;
                    display.Content = result.ToString().Replace(',', '.');
                    break;
                case "-":
                    result -= currentValue;
                    display.Content = result.ToString().Replace(',', '.');
                    break;
                case "x":
                    result *= currentValue;
                    display.Content = result.ToString().Replace(',', '.');
                    break;
                case "÷":
                    if (currentValue == 0.0)
                    {
                        display.Content = "Dzielenie przez zero";
                        operandOrOperator = "";
                        return;
                    }
                    else
                    {
                        result /= currentValue;
                        display.Content = result.ToString().Replace(',', '.');
                    }
                    break;
                default:
                    break;
            }
            result = Double.Parse(display.Content.ToString().Replace('.', ','));
            operandOrOperator = "";
            equalSignPressed = false;
        }


        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            display.Content = "0";
        }


        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            result = 0;
            display.Content = "0";
            equation.Content = "";
            operandOrOperator = "";
        }

        private void BackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (display.Content.ToString().Equals("Dzielenie przez zero"))
            {
                Clear_Click(this, null);
            }
            else
            {
                if (!String.IsNullOrEmpty(display.Content.ToString()))
                {
                    display.Content = display.Content.ToString().Remove(display.Content.ToString().Length - 1);
                }
                if (display.Content.ToString().Equals("") || display.Content.ToString().Equals("-"))
                {
                    display.Content = "0";
                }
            }
            operandOrOperatorPressed = false;
        }

        private void PlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (display.Content.ToString().Equals("Dzielenie przez zero"))
            {
                Clear_Click(this, null);
            }
            else
            {
                double currentValue = Double.Parse(display.Content.ToString().Replace('.', ','));
                display.Content = (-currentValue).ToString().Replace(',', '.');
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.D0 || e.Key == Key.NumPad0)
            {
                zero.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.D1 || e.Key == Key.NumPad1)
            {
                one.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.D2 || e.Key == Key.NumPad2)
            {
                two.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.D3 || e.Key == Key.NumPad3)
            {
                three.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.D4 || e.Key == Key.NumPad4)
            {
                four.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.D5 || e.Key == Key.NumPad5)
            {
                five.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.D6 || e.Key == Key.NumPad6)
            {
                six.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.D7 || e.Key == Key.NumPad7)
            {
                seven.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (Keyboard.IsKeyDown(Key.RightShift) && e.Key == Key.D8 || e.Key == Key.Multiply)
            {
                multiply.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.D8 || e.Key == Key.NumPad8)
            {
                eight.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.D9 || e.Key == Key.NumPad9)
            {
                nine.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.OemComma || e.Key == Key.OemPeriod)
            {
                dot.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.OemPlus || e.Key == Key.Add)
            {
                plus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.OemMinus || e.Key == Key.Subtract)
            {
                minus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.OemQuestion || e.Key == Key.Divide)
            {
                divide.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.Enter)
            {
                equals.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.Delete)
            {
                clearEntry.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.Escape)
            {
                clear.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.Back)
            {
                backSpace.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (e.Key == Key.N)
            {
                plusMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}

