using System.IO.Compression;
using System.Windows.Forms;
using UndertaleModLib;
using ZxoTests;

namespace ZxoPTUF
{
    public partial class Main : Form
    {
        private string dataPath => tbDataFile.Text;
        private string outputPath => tbOutput.Text;

        private (UndertaleData Data, string Path)? UndertaleData;

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

        private async void button3_Click(object sender, EventArgs e)
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

            tick.Start();
            
            try
            {
                await StartExtract();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async Task StartExtract()
        {
            if (UndertaleData == null)
                UndertaleData = (await ReadUndertaleDataAsync(dataPath), dataPath);
            else if (UndertaleData.Value.Path != dataPath)
                UndertaleData = (await ReadUndertaleDataAsync(dataPath), dataPath);
            Extractor = new SpriteExtractor(UndertaleData.Value.Data, outputPath, tbFilter.Text, cbSpriteSplit.Checked);
            await Extractor.Start();
        }

        private int dotCount = 0;
        private void tick_Tick(object sender, EventArgs e)
        {
            if (UndertaleData == default)
            {
                tick.Interval = 1000;
                string dot = ".";
                for (int i = 0; i < dotCount; i++)
                    dot += ".";
                dotCount++;
                dotCount %= 3;
                progressLabel.Text = "Opening data file" + dot;
                return;
            }
            else if (Extractor == null)
                return;

            int progress = Extractor.Progress;
            progressBar.Value = progress;

                tick.Interval = 33;
                progressLabel.Text = $"Extracting: {Extractor.Current}/{Extractor.Total}";

            if (Extractor.Progress == 100)
            {
                tick.Stop();
                progressLabel.Text = "Done!";
                Extractor = null;
            }
        }

        private static Task<UndertaleData> ReadUndertaleDataAsync(string dataPath)
        {
            return Task.Run(() =>
            {
                FileStream fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read);
                UndertaleData data = UndertaleIO.Read(fs, null);
                fs.Close();
                return data;
            });

        }
    }
}