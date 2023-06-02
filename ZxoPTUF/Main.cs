using System.IO.Compression;
using System.Windows.Forms;
using ZxoTests;

namespace ZxoPTUF
{
    public partial class Main : Form
    {
        private string dataPath => tbDataFile.Text;
        private string outputPath => tbOutput.Text;

        SpriteExtractor? Extractor;
        public Main()
        {
            InitializeComponent();
            filedialog.Filter = "Undertale Data Files|*.win;*.undertale;*.dat|All Files|*.*";
            //output will be a zip
            outputDialog.Filter = "Zip Files|*.zip|All Files|*.*";
            progressLabel.Text = string.Empty;
            //set the outputDialog to only take folders
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = filedialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbDataFile.Text = filedialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = outputDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbOutput.Text = outputDialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dataPath))
            {
                MessageBox.Show("Please select a data file");
                return;
            }
            if (string.IsNullOrWhiteSpace(outputPath))
            {
                MessageBox.Show("Please select an output file");
                return;
            }
            if (Extractor != null)
            {
                MessageBox.Show("Already extracting!");
                return;
            }
            
            Extractor = new SpriteExtractor(dataPath, outputPath, tbFilter.Text);
            Extractor.Start();
            tick.Start();
        }

        private int dotCount = 0;
        private void tick_Tick(object sender, EventArgs e)
        {
            if (Extractor == null) return;

            int progress = Extractor.Progress;
            progressBar.Value = progress;


            if (progress == 0)
            {
                tick.Interval = 1000;
                string dot = ".";
                for (int i = 0; i < dotCount; i++)
                    dot += ".";
                dotCount++;
                dotCount %= 3;
                progressLabel.Text = "Opening data file" + dot;
            }
            else
            {
                tick.Interval = 33;
                progressLabel.Text = $"Extracting: {Extractor.Current}/{Extractor.Total}";
            }

            if (Extractor.Progress == 100)
            {
                tick.Stop();
                progressLabel.Text = "Done!";
                MessageBox.Show("Done!");
                Extractor = null;
            }
        }
    }
}