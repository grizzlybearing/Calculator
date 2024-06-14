//разрешенные символы : +-/*^, 0-9, ()
//функция для формирования ОПЗ и его вычисления
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
namespace Calculator {

public partial class MainWindow : Window
{
        private double Solving(string math)
        {
            int Priority(string op)//определение приоритета операции
            {
                switch (op)
                {
                    case "^": return 3; break;
                    case "/": return 2; break;
                    case "*": return 2; break;
                    case "+": return 1; break;
                    case "-": return 1; break;
                    default: return 0;
                }
            }

            List<string> Opz = new List<string>();
            Stack<string> Interim = new Stack<string>();
            Stack<double> Operands = new Stack<double>();
            const string operations = "+-/*^";
            const string numbers = "0123456789";
            string num = "";
            bool WasPoint = false, IsNeg = false;
            int OpenBrackets = 0;
            double op, op1, op2;

            for (int i = 0; i < math.Length; i++)
            {
                if (math[i] == '-' && (i == 0 || (i != 0 && math[i - 1] == '(')))
                { //обработка негативных чисел
                    IsNeg = true;
                    continue;
                }
                if (operations.Contains(math[i]) && i != 0)
                {
                    if (i + 1 != math.Length && math[i - 1] != '(' && operations.Contains(math[i - 1]) == false)
                    { //формирование ОПЗ при операции
                        if (Interim.Count == 0) Interim.Push(math[i].ToString());
                        else
                            if (Priority(Interim.Peek()) < Priority(math[i].ToString())) Interim.Push(math[i].ToString()); // если стек пуст или приоритет текущей операции выше, чем на вершине стека, то добавляем операцию в стек 
                        else
                        {
                            while (Priority(Interim.Peek()) >= Priority(math[i].ToString())) // иначе добавляем в ОПЗ все операции с равным или более высоким приоритетом
                            {
                                Opz.Add(Interim.Pop());
                                if (Interim.Count == 0) break;
                            }
                            Interim.Push(math[i].ToString()); // добавляем текущую операцию в стек
                        }
                        continue;
                    }
                    else throw new Exception("Некорректный ввод");
                }
                if (math[i] == '(' && (i + 1 != math.Length))
                {
                    if (i == 0 || (i != 0 && (operations.Contains(math[i - 1]) || math[i - 1] == '(')))
                    {
                        Interim.Push("(");
                        OpenBrackets++;
                        continue;
                    }
                    else throw new Exception("Некорректный ввод");
                }
                if (math[i] == ')' && i != 0 && OpenBrackets > 0)
                {
                    if (math[i - 1] == ')' || numbers.Contains(math[i - 1]))
                    {
                        while (Interim.Peek() != "(")
                        {
                            Opz.Add(Interim.Pop());
                        }
                        Interim.Pop();
                        OpenBrackets--;
                        continue;
                    }
                    else throw new Exception("Закрывающая скобка без открывающей");
                }
                if (numbers.Contains(math[i])) // добавление в ОПЗ числа
                {
                    if (IsNeg) num = "-";          // негативное число
                    num += math[i].ToString();
                    i++;
                    while (i < math.Length)
                    {
                        if ((math[i] != ',') && numbers.Contains(math[i]) == false) break;      //пока число или строка не закончатся

                        if (math[i] == ',')     //проверка на корректность ввода десятичной дроби
                        {
                            if (WasPoint == true) throw new Exception("Некорректный ввод дробного числа");
                            else WasPoint = true;
                        }

                        num += math[i].ToString();
                        i++;
                    }
                    if (num.Last() == ',') throw new Exception("Некорректный ввод дробного числа");
                    Opz.Add(num);
                    num = "";
                    WasPoint = false;
                    i--;
                    continue;
                }
                throw new Exception("Некорректный ввод");

            }
            if (OpenBrackets > 0) throw new Exception("Открывающих скобок больше, чем закрывающих");

            while (Interim.Count > 0)
            {
                Opz.Add(Interim.Pop());
            }

            foreach (string str in Opz)
            {                                   //расчет ОПЗ
                if (double.TryParse(str, out op)) Operands.Push(op);        //если число, то добавляем в стек для операндов
                else
                {
                    op2 = Operands.Pop();
                    op1 = Operands.Pop();
                    switch (str)                                            //иначе выполняем опрецию и помещаем в стек результат операции
                    {
                        case "+": op = op1 + op2; break;
                        case "-": op = op1 - op2; break;
                        case "/": if (op2 != 0) { op = op1 / op2; break; } else throw new Exception("Деление на 0");
                        case "*": op = op1 * op2; break;
                        case "^": op = Math.Pow(op1, op2); break;
                    }
                    Operands.Push(op);
                }
            }
            return Operands.Pop();
        }
}
}
