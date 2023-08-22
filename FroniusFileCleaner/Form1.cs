namespace FroniusFileCleaner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string rootFolder = @"C:\Fronius"; // Taranacak kök klasörü belirtin
            string[] folders = Directory.GetDirectories(rootFolder, "*", SearchOption.AllDirectories);
            List<DateTime> Dates = new List<DateTime>();

            // Log dosyasý oluþturulmasý ve baþlangýç mesajýnýn yazýlmasý
            string logFilePath = Path.Combine(rootFolder, "log.txt");
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss:fff} @ Started : FroniusFileCleaner is started");
            }

            foreach (string folder in folders)
            {
                List<string> fdoFiles = new List<string>();
                string[] files = Directory.GetFiles(folder, "*.fdo");
                foreach (string file in files)
                {
                    fdoFiles.Add(file);
                }
                fdoFiles.Sort();
                fdoFiles.Reverse();

                if (fdoFiles.Count() > 1)
                {
                    fdoFiles.RemoveAt(0);
                }
                foreach (string file in fdoFiles)
                {
                    File.Delete(file);

                    using (StreamWriter writer = File.AppendText(logFilePath))
                    {
                        writer.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss:fff} @ Deleted : {Path.GetFileName(file)} is deleted");
                    }
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string rootFolder = @"C:\Fronius"; // Taranacak kök klasörü belirtin
            string logFilePath = Path.Combine(rootFolder, "log.txt");

            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss:fff} @ Stopped : FroniusFileCleaner is stopped");
            }
        }
    }
}