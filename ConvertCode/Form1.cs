using CSharpToTypescript;
using System.Windows.Forms;

namespace ConvertCode
{
    public partial class Form1 : Form
    {
        private ConvertorEngine _convertor;
        public Form1(ConvertorEngine convertor)
        {
            InitializeComponent();
            _convertor = convertor;
        }

        private void convertButton_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.codeArea.Text))
                this.codeArea.Text = _convertor.Convert(this.codeArea.Text);
        }
    }
}
