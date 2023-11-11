namespace _23102023_FinalsOptimization;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
		
		Form forms = new Form1();
		forms.Icon = new Icon("skadut.ico");
        Application.Run(forms);
    }    
}