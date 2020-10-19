using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;
                                                                                            //20197760
namespace CalculatorAndroidTut
{
    [Activity(Label = "Calculadora - 2019-7760", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private TextView calculatorText;

        private string[] numbers = new string[2];
        private string @operator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Crear la vista de nuestro Main form.
            SetContentView(Resource.Layout.Main);

            calculatorText = FindViewById<TextView>(Resource.Id.calculator_text_view);
        }

                // Establecer interoperabilidad con Java para exportar clickeo de
        [Java.Interop.Export("ButtonClick")]

                // Establecer funcion de los botones de mostrar cuando son presionados
        public void ButtonClick (View v)
        {
            Button button = (Button)v;
            if ("0123456789.".Contains(button.Text))
                AddDigitOrDecimalPoint(button.Text);
            else if ("÷×+-".Contains(button.Text))
                AddOperator(button.Text);
            else if ("=" == button.Text)
                Calculate();
            else
                Erase();
        }

                //Declaracion de decimales.
        private void AddDigitOrDecimalPoint(string value)
        {
            int index = @operator == null ? 0 : 1;

            if (value == "." && numbers[index].Contains("."))
                return;

            numbers[index] += value;

            UpdateCalculatorText();
        }
                //Retortar operacion.
        private void AddOperator(string value)
        {
            if (numbers[1] != null)
            {
                Calculate(value);
                return;
            }

            @operator = value;

            UpdateCalculatorText();
        }
                // Funciones de cada calculo (suma, multiplicacion, division, resta) y division de los numeros en el TBX (EVITAR TENER QUE USAR 2 TEXT BOXES)
        private void Calculate(string newOperator = null)
        {
            double? result = null;
            double? first = numbers[0] == null ? null : (double?)double.Parse(numbers[0]);
            double? second = numbers[1] == null ? null : (double?)double.Parse(numbers[1]);

            switch (@operator)
            {
                case "÷":
                    result = first / second;
                    break;
                case "×":
                    result = first * second;
                    break;
                case "+":
                    result = first + second;
                    break;
                case "-":
                    result = first - second;
                    break;
            }

            if (result != null)
            {
                numbers[0] = result.ToString();
                @operator = newOperator;
                numbers[1] = null;
                UpdateCalculatorText();
            }
        }
                // Funcion Borrar (Boton grande de borrar)
        private void Erase()
        {
            numbers[0] = numbers[1] = null;
            @operator = null;
            UpdateCalculatorText();
        }
             //Actualizacion del texto en la calculadora.
        private void UpdateCalculatorText() => calculatorText.Text = $"{numbers[0]} {@operator} {numbers[1]}";
    }
}

