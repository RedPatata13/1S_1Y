namespace _23102023_FinalsOptimization;

public class HistoryItem : UserControl
{
	public Label Expression {get; set; }
	public Label Result {get; set;}
	public Label DT {get; set;}
	public Label Time { get; set; }
	
	public HistoryItem(int x, int y, string expression, string result, DateTime dt)
	{
		
		this.BackColor = ColorTranslator.FromHtml("#5a5a5a");
		this.Size = new System.Drawing.Size(410, 150);
		this.Location = new System.Drawing.Point(x, y);
		
		Expression = new Label();
		Expression.Text = expression;
		Expression.Font = new System.Drawing.Font("Times New Roman", 9);
		Expression.Location = new System.Drawing.Point(10, 30);
		Expression.Size = new System.Drawing.Size(290, 20);
		Controls.Add(Expression);
		
		Result = new Label();
		Result.Text = result;
		Result.Font = new System.Drawing.Font("Times New Roman", 9);
		Result.Location = new System.Drawing.Point(10, 60);
		Result.Size = new System.Drawing.Size(290, 20);
		Controls.Add(Result);
		
		DT = new Label();
		DateTime today = DateTime.Now;
		string totalTime = Convert.ToString(today);
		DT.Text = totalTime;
		DT.Font = new System.Drawing.Font("Times New Roman", 9);
		DT.Location = new System.Drawing.Point(10, 90);
		DT.Size = new System.Drawing.Size(290, 20);
		Controls.Add(DT);

		
		CopyButton copyExpression = new CopyButton(Expression.Location.X + 300, 30, Expression);
		Controls.Add(copyExpression);
		
		CopyButton copyResult = new CopyButton(Result.Location.X + 300, 60, Result);
		Controls.Add(copyResult);

	}
}

public class CopyButton : Button 
{
	public CopyButton(int x, int y, Label target)
	{
		this.Size = new System.Drawing.Size(75, 30);
		this.Location = new System.Drawing.Point(x, y);
		this.Text = "Copy";
		this.Click += (sender, e) =>
		{
			Clipboard.SetText(target.Text);
		};
	}
}
