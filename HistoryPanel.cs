namespace _23102023_FinalsOptimization;

public class HistoryPanel : UserControl
{
	public Panel historyPane { get; set; }
	public List<HistoryItem> list { get; set; }
	public int top { get; set; }
	public int defaultX { get; }
	public int defaultY { get; set; }
	
	public HistoryPanel(int x, int y)
	{
		top = -1;
		defaultX = 10;
		defaultY = 151;
		list = new List<HistoryItem>();
		
		this.Size = new System.Drawing.Size(470, 400);
		this.Location = new System.Drawing.Point(x, y);
		
		historyPane = new Panel();
		historyPane.Dock = DockStyle.Fill;
		historyPane.BackColor = ColorTranslator.FromHtml("#212529");
		
		Label historyLabel = new Label();
		historyLabel.Text = "History: ";
		historyLabel.Font = new System.Drawing.Font("Times New Roman", 12);
		historyLabel.ForeColor = ColorTranslator.FromHtml("#ffffff");
		historyLabel.Location = new System.Drawing.Point(10, 0);
		
		historyPane.AutoScroll = true;
		
		historyPane.Controls.Add(historyLabel);
		
		this.Controls.Add(historyPane);
	}
}
