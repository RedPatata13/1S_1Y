namespace _23102023_FinalsOptimization;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        int padding = 50;
        this.ClientSize = new Size(1600 + 2 * padding, 400 + 2 * padding);
		this.Text = "me and the voices in my head partying at 3am";
		this.BackColor = ColorTranslator.FromHtml("#212529");
		this.FormBorderStyle = FormBorderStyle.FixedSingle; 
		
		
		this.inputExpressionBox = new InputBox();
        this.inputExpressionBox.Location = new System.Drawing.Point(50, 50);
		this.Controls.Add(inputExpressionBox);
		this.inputExpressionBox.Click += new System.EventHandler(this.InputBox_Click);

        this.outputResultBox = new OutputBox();
        this.outputResultBox.Location = new Point(50, 100);
		this.outputResultBox.Text = "Result: ";
		this.Controls.Add(outputResultBox);
    }
	
	public class InputBox : TextBox
	{
		public InputBox()
		{
			this.Size = new System.Drawing.Size(1050, 150);
			this.Text = "Input Expression here: ";
			this.ReadOnly = true;
			this.Font = new System.Drawing.Font("Courier New", 12);
			this.ForeColor = ColorTranslator.FromHtml("#d3d3d3");
			this.BackColor = ColorTranslator.FromHtml("#5a5a5a");
			this.TextChanged += InputBox_TextChanged; 
		}

		private void InputBox_TextChanged(object sender, EventArgs e)
		{
			this.ForeColor = Color.White;
		}
	}


	public class OutputBox : TextBox
	{
		public OutputBox()
		{
			this.Size = new System.Drawing.Size(1050, 150);
			this.Font = new System.Drawing.Font("Courier New", 12);
			this.ReadOnly = false;
			this.ForeColor = ColorTranslator.FromHtml("#d3d3d3");
			this.BackColor = ColorTranslator.FromHtml("#5a5a5a");

		}
	}


    public class leftButtons : Button
    {
        public leftButtons()
        {
            this.Size = new System.Drawing.Size(125, 50);
            this.Font = new System.Drawing.Font("Times New Roman", 10);
			this.BackColor = ColorTranslator.FromHtml("#212529");
			this.ForeColor = ColorTranslator.FromHtml("#ffffff");
        }
    }
	
	public class specialLeftButtons : leftButtons
	{
		public specialLeftButtons()
		{
			this.Size = new System.Drawing.Size(63, 50);
		}
	}
	
	public class rightButtons : Button
	{
		public rightButtons()
		{
			this.Size = new System.Drawing.Size(100, 50);
            this.Font = new System.Drawing.Font("Times New Roman", 10);
			this.BackColor = ColorTranslator.FromHtml("#212529");
			this.ForeColor = ColorTranslator.FromHtml("#ffffff");

		}
	}

    #endregion
}
