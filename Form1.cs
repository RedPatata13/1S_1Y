using System.Drawing;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _23102023_FinalsOptimization;

public partial class Form1 : Form
{
	
	public InputBox inputExpressionBox;
	public OutputBox outputResultBox;
	private leftButtons[] lButtons;
	private rightButtons[] rButtons;
	private HistoryPanel history;
	
	public bool deg = true;
	
	private string[] leftButtonsText = new string[]{ "7", "8", "9", "+", "4", "5", "6", "-", "1", "2", "3", "=", "0", "*", "/" }; 
	
	private string[] rightButtonsText = new string[]{ "sin()", "cos()", "tan()", "bspace", "CLEAR", "asin()", "acos()", "atan()", "neg()", "pi", "hsin()", "hcos()", "htan()", "C", "p", "^", "%", "abs()", "log()", "ln()", "!", "sqrt()", "cbrt()", "DEG", "ans"  };
	
	private HashSet<int> specialLButtons = new HashSet<int>{ 3, 7, 11 };
	
	private HashSet<string> special = new HashSet<string> { "log()", "ln()", "sin()", "cos()", "tan()", "sqrt()", "cbrt()", "asin()", "acos()", "atan()", "hsin()","hcos()", "htan()","abs()", "neg()" };
	

    public Form1()
    {
        InitializeComponent();
		InitializeleftButtons();
		InitializeRightButtons();
		InitializeHistory();
    }
	private void InitializeHistory(){
		history = new HistoryPanel(1150, 50);
		this.Controls.Add(history);
	}
	
	private void InitializeleftButtons()
	{
		int buttonCount = 15;
        lButtons = new leftButtons[buttonCount];

        for (int i = 0; i < buttonCount; i++)
        {
			lButtons[i] = new leftButtons();
            lButtons[i].Text = leftButtonsText[i];
            lButtons[i].Name = leftButtonsText[i];
			
            lButtons[i].Location = new Point((20 + i * 125) % (125 * 4) + 630, 50 * (i / 4) + 200);
			
			if(specialLButtons.Contains(i)){
				lButtons[i].Size = new System.Drawing.Size(63, 50);
				if(i == 11) lButtons[i].Size = new Size(63, 100);
			}
			if(i != 11){
				lButtons[i].Click += new EventHandler(Button_Click);
            }
            this.Controls.Add(lButtons[i]);
        }
		lButtons[11].Click += new EventHandler(OutputBox_Result);
	}
	
	private void InitializeRightButtons()
	{
		int buttonCount = 25;
		rButtons = new rightButtons[buttonCount];
		
		for(int i = 0; i < buttonCount; i++)
		{
			rButtons[i] = new rightButtons();
			rButtons[i].Text = rightButtonsText[i];
			rButtons[i].Name = rightButtonsText[i];
			
			
			rButtons[i].Location = new Point((20 + i * 100) % (100 * 5) + 75, 50 * (i / 5) + 200);
			
			rButtons[i].Click += new EventHandler(Button_Click);
			

			this.Controls.Add(rButtons[i]);
		}
		rButtons[23].Click += new EventHandler(Change_Mode);//23 is RAD and 24 is DEG
		rButtons[23].Click -= Button_Click;
		rButtons[23].BackColor = ColorTranslator.FromHtml("#eb6123");

		rButtons[24].Click += new EventHandler(get_ans);
		rButtons[24].Click -= Button_Click;
		
	}
	
	private void Change_Mode(object sender, EventArgs e)
	{
		if(sender is Button button)
		{

			if(button.Text == "DEG")
			{
				deg = false;
				button.Text = "RAD";
				
			} 
			else 
			{
				deg = true;
				button.Text = "DEG";
			}
			
		}
	}
	
	private void get_ans(object sender, EventArgs e)
	{
		int len = history.list[0].Result.Text.Length;
		int nextPos = inputExpressionBox.SelectionStart + len;
		string getText = history.list[0].Result.Text;
		
		inputExpressionBox.Text = inputExpressionBox.Text.Insert(inputExpressionBox.SelectionStart, getText);
		inputExpressionBox.SelectionStart = nextPos;
	}
	
	private void InputBox_Click(object sender, EventArgs e)
	{
		if (inputExpressionBox.Text == "Input Expression here: ")
		{
			inputExpressionBox.Text = "";
			inputExpressionBox.ForeColor = SystemColors.ControlText;
			inputExpressionBox.ReadOnly = false;
		}
	}
	private void OutputBox_Result(object sender, EventArgs e)
	{
		int yOffSet = 35;
		int xOffSet = 10;
		
		string expression = inputExpressionBox.Text;
		Calculator calculator = new Calculator();
		
		Calculate calc = new Calculate(calculator);
		string processed = calc.Result(expression, deg);
		
		outputResultBox.Text = processed;
		DateTime current = DateTime.Now;
		

		newHistory(xOffSet, yOffSet, expression, processed, current);
	}
	
	private void newHistory(int x, int y, string expression, string result, DateTime dt){
		++history.top;
		DateTime current = Convert.ToDateTime(dt);
		HistoryItem log = new HistoryItem(x, y, expression, result, dt);
		
		history.list.Insert(0, log);
		history.historyPane.Controls.Add(log);
		
		for (int i = 1; i < history.list.Count; i++)
		{
			history.list[i].Top += log.Height + 1;
		}

		history.top++;
	}
	
	public class Calculate
	{
		public readonly ICalculator _calculator;
		public Calculate(ICalculator calculator)
		{
			_calculator = calculator;
		}
		
		public string Result(string expression, bool degrees)
		{
			try
			{
				double result = _calculator.Evaluate(expression, degrees);
				string finalRes = Convert.ToString(result);
				
				return finalRes;
			}
			catch
			{
				return "Invalid Expression";
			}
		}
	}
	private void Button_Click(object sender, EventArgs e)
    {
        if (inputExpressionBox.Text == "Input Expression here: ")
        {
            inputExpressionBox.Text = "";
            inputExpressionBox.ForeColor = SystemColors.ControlText;
            inputExpressionBox.ReadOnly = false;
        }
		if (sender is Button button)
		{			
			if(button.Text == "CLEAR")
			{
				inputExpressionBox.Text = "";
			} 
			else if(button.Text == "bspace")
			{
				if(inputExpressionBox.SelectionStart > 0){
					int newCaretPos = inputExpressionBox.SelectionStart - 1;
					inputExpressionBox.Text = inputExpressionBox.Text.Remove(newCaretPos, 1);
					inputExpressionBox.SelectionStart = newCaretPos;
					inputExpressionBox.Focus();
				}
			}
			else if(special.Contains(button.Text))
			{
				int currentPos = inputExpressionBox.SelectionStart;
				inputExpressionBox.Text = inputExpressionBox.Text.Insert(currentPos, button.Text);
				inputExpressionBox.SelectionStart = currentPos + button.Text.Length - 1;
				inputExpressionBox.Focus();
			}
			else 
			{
				int nextPos = inputExpressionBox.SelectionStart + 1;
				inputExpressionBox.Text = inputExpressionBox.Text.Insert(inputExpressionBox.SelectionStart, button.Text);
				inputExpressionBox.SelectionStart = nextPos;
			}
		}
    }
}

public interface ICalculator
{
	double Evaluate(string expression, bool mode);
	double PerformOperationBinary(string op, double val1, double val2);
	double PerformOperationUnary(string op, double val, bool mode);
	List<string> Tokenize(string expression);
	List<string> ConvertToRPN(List<string> tokens);
	double factorial(double n);
	int precedence(string op);
}

public class Calculator : ICalculator
{
	private static readonly HashSet<string> binaryOperators = new HashSet<string> { "+", "-", "*", "/", "^", "%", "C", "P" };
    private static readonly HashSet<string> unaryOperators = new HashSet<string> { "log", "ln", "sin", "cos", "tan", "sqrt", "cbrt", "asin", "acos", "atan", "hsin","hcos", "htan","abs", "neg", "pi", "!"};

    public double Evaluate(string expression, bool mode)
    {
        List<string> tokens = Tokenize(expression);
        List<string> rpn = ConvertToRPN(tokens);
        Stack<double> nums = new Stack<double>();
		

        foreach (string token in rpn){
            if (!unaryOperators.Contains(token) && !binaryOperators.Contains(token)){
                nums.Push(double.Parse(token));
            }
            else if (binaryOperators.Contains(token)){
                double val2 = nums.Pop();
                double val1 = nums.Pop();
                nums.Push(PerformOperationBinary(token, val1, val2)); 
            }
            else if (unaryOperators.Contains(token)){
                double val = nums.Pop();
                nums.Push(PerformOperationUnary(token, val, mode)); 
            }
        }

        if (nums.Count == 1){
            return nums.Pop();
        }
        else{
            throw new ArgumentException("Invalid expression");
        }
    }

    public double PerformOperationBinary(string op, double val1, double val2)
    {
        switch (op)
        {
            case "+":
                return val1 + val2;
            case "-":
                return val1 - val2;
            case "*":
                return val1 * val2;
            case "/":
                return val1 / val2;
            case "^":
                return Math.Pow(val1, val2);
            case "%":
                return val1 % val2;
			case "C":
				double combinations = factorial(val1)/(factorial(val2) * factorial(val1 - val2));
				return (combinations > 0)? combinations: 0;
			case "P":
				return factorial(val1)/factorial(val1 - val2);
				
            default:
                return 0;
        }
    }

    public double PerformOperationUnary(string op, double val, bool mode)
    {
		
        switch (op)
        {
            case "sin":
				val = (mode)? val: val * (Math.PI / 180);
                return Math.Sin(val);
            case "cos":
				val = (mode)? val: val * (Math.PI / 180);
                return Math.Cos(val);
            case "tan":
				val = (mode)? val: val * (Math.PI / 180);
                return Math.Tan(val);
            case "log":
                return Math.Log(val, 10);
            case "ln":
                return Math.Log(val);
            case "asin":
				val = (mode)? val: val * (Math.PI / 180);
                return Math.Asin(val);
            case "acos":
				val = (mode)? val: val * (Math.PI / 180);
                return Math.Acos(val);
            case "atan":
				val = (mode)? val: val * (Math.PI / 180);
                return Math.Atan(val);
            case "hsin":
				val = (mode)? val: val * (Math.PI / 180);
                return Math.Sinh(val);
            case "hcos":
				val = (mode)? val: val * (Math.PI / 180);
                return Math.Cosh(val);
            case "htan":
				val = (mode)? val: val * (Math.PI / 180);
                return Math.Tanh(val);
            case "sqrt":
                return Math.Sqrt(val);
            case "cbrt":
                return Math.Cbrt(val);
            case "abs":
                return Math.Abs(val);
			case "neg":
				return val * -1; 
			case "!":
				return factorial(val);
            default:
                return 0;
        }
    }

    public List<string> Tokenize(string expression)
    {
        List<string> tokens = new List<string>();
        string pattern = @"(\d+(\.\d*)?)|(\b(log|ln|sin|cos|tan|sqrt|cbrt|asin|acos|atan|hsin|hcos|htan|abs|neg|pi)\b)|([+\-*/^%CP!()])|(-\d+(\.\d*)?)|(\()|(\))";

        MatchCollection matches = Regex.Matches(expression, pattern);

        foreach (Match match in matches){
            tokens.Add(match.Value);
        }

        return tokens;
    }

    public List<string> ConvertToRPN(List<string> tokens){
        List<string> rpn = new List<string>();
        Stack<string> operatorStack = new Stack<string>();
		int currentIndex = 0;
        foreach (string token in tokens){
            if (double.TryParse(token, out _) || token == "pi"){
				if(token == "pi"){
					rpn.Add("3.14159265358979");
				}else {
					rpn.Add(token);
				}
            }
			else if(token == "-")
			{
				if(currentIndex == 0){
					operatorStack.Push("neg");
				} else {
					if(binaryOperators.Contains(tokens[currentIndex - 1]) || unaryOperators.Contains(tokens[currentIndex - 1]) || tokens[currentIndex - 1] == "("){
						operatorStack.Push("neg");
					}else if(double.TryParse(tokens[currentIndex - 1], out _)){
						operatorStack.Push("-");
					}else if(tokens[currentIndex - 1] == ")"){
						operatorStack.Push("-");
					}
				}
				
			}
            else if ((binaryOperators.Contains(token) || unaryOperators.Contains(token)) && token != "-")
            {
                while (operatorStack.Count > 0 && precedence(token) <= precedence(operatorStack.Peek()))
                {
                    rpn.Add(operatorStack.Pop());
                }
                operatorStack.Push(token);
            }
            else if (token == "("){
                operatorStack.Push(token);
            }
            else if (token == ")"){
                while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                {
                    rpn.Add(operatorStack.Pop());
                }
                if (operatorStack.Count > 0 && operatorStack.Peek() == "(")
                {
                    operatorStack.Pop();
                }
                else{
                    throw new ArgumentException("Mismatched parentheses");
                }
            }
			currentIndex++;
        }
        while (operatorStack.Count > 0)
        {
            rpn.Add(operatorStack.Pop());
        }

        return rpn;
    }
	public double factorial(double n)
    {
        double result = 1;

        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }
    public int precedence(string op)
    {
        switch (op)
        {
            case "+":
            case "-":
                return 1;
            case "*":
            case "/":
			case "abs":
			
                return 2;
            case "sin":
            case "cos":
            case "tan":
            case "log":
            case "ln":
        	case "asin":
    		case "acos":
    		case "atan":
    		case "hsin":
    		case "hcos":
    		case "htan":
			case "neg":
			case "C":
			case "P":
                return 3;
            case "^":
            case "sqrt":
            case "cbrt":
                return 4;
            default:
                return 0;
        }
    }
}
