using LabWork5; 
using System;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;



class Programm
{
    public static void Main()
    {
        while (true)
        {
            string userInput;
            Console.Write("Введите выражение: ");
            userInput = Console.ReadLine();
            if (!"1234567890,+-*/()".Contains(userInput))
            {

            }

            List<Token> tokens = new List<Token>();
            tokens = GetTokensList(userInput);

            List<Token> rpnList = new List<Token>();
            rpnList = GetRPNList(tokens);

            Console.Write("\nВаше выражение в ОПЗ: ");
            Print(rpnList);

            Console.WriteLine("\n\nОтвет: " + GetResult(rpnList) + "\n\n\n<<Нажмите любую клавишу...>>");

            Console.ReadKey();
            Console.Clear();
        }
        
    }
    
    public static List<Token> GetTokensList(string userInput)
    {
        List<Token> tokens = new List<Token>();
        
        userInput = userInput.Replace(" ", "");

        int parenthesisCounter = 0;
        string currectNumber = null;
        foreach(char symbol in userInput)
        {
            if ("1234567890,".Contains(symbol)) { currectNumber += symbol; }
            else 
            {
                if (currectNumber != null) 
                {
                    tokens.Add(new Number(Convert.ToDouble(currectNumber)));
                    currectNumber = null;
                }

                if ("+-*/".Contains(symbol)) { tokens.Add(new Operation(symbol)); }

                else if ("()".Contains(symbol)) 
                { 
                    tokens.Add(new Parenthesis(symbol));
                    parenthesisCounter++;
                }
            }
        }
        if(currectNumber !=  null) { tokens.Add(new Number(Convert.ToDouble(currectNumber))); }
        if (parenthesisCounter % 2 != 0) { tokens.Add(new Parenthesis(')')); }
        return tokens;
    }

    public static void Print(List<Token> tokens)
    {
        foreach(Token token in tokens)
        {
            switch (token)
            {
                case Number number:
                    Console.Write(number.number + " ");
                    break;

                case Operation operation:
                    Console.Write(operation.operation + " ");
                    break;

                case Parenthesis parenthesis:
                    Console.Write(parenthesis.bracket + " ");
                    break;
            }
        }
    }

    public static List<Token> GetRPNList(List<Token> tokens)
    {
        List<Token> rpn = new List<Token>();
        Stack<Token> operators = new Stack<Token>();

        int counter = 0;
        foreach (Token token in tokens)
        {
            if (token is Number)
            {
                rpn.Add(token);
                if (counter == tokens.Count - 1)
                {
                    while (operators.Count > 0)
                    {
                        rpn.Add(operators.Pop());
                    }
                }
                counter++;
                continue;
            }

            if (token is Operation)
            {
                if (operators.Count == 0)
                {
                    operators.Push((Operation)token);

                    counter++;
                    continue;
                }
                while(operators.Count != 0 && typeof(Parenthesis) != operators.Peek().GetType() && ((Operation)operators.Peek()).priority >= ((Operation)token).priority)
                {
                    rpn.Add(operators.Pop());
                }
                operators.Push((Operation)token);
                counter++;
            }
            
            if(token is Parenthesis)
            {
                if (((Parenthesis)token).isOpen)
                {
                    operators.Push(token);
                    counter++;
                }
                else
                {
                    while (typeof(Parenthesis) != operators.Peek().GetType())
                    {
                        rpn.Add(operators.Pop());
                    }
                    operators.Pop();
                    counter++;
                }
            }
            if (counter == tokens.Count)
            {
                while (operators.Count > 0)
                {
                    rpn.Add(operators.Pop());
                }
            }
        }
        return rpn;
    }

    public static double GetResult(List<Token> rpn)
    {
        double resultOfOperation = 0;

        Stack<Number> numbers = new Stack<Number>();

        foreach(Token token in rpn)
        {
            if (token is Number)
            {
                numbers.Push((Number)token);
            }
            else if(token is Operation)
            {
                double number2 = (numbers.Pop()).number;
                double number1 = (numbers.Pop()).number;

                switch (((Operation)token).operation)
                {
                    case '+':
                        resultOfOperation = number1 + number2;
                        break;
                    case '-':
                        resultOfOperation = number1 - number2;
                        break;
                    case '*':
                        resultOfOperation = number1 * number2;
                        break;
                    case '/':
                        resultOfOperation = number1 / number2;
                        break;
                }
                numbers.Push(new Number(resultOfOperation));
            }
        }
        return (numbers.Pop()).number;
    }
}