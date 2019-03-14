using System;
using System.Text;
using System.Windows;
using System.Windows.Forms;

using Dogged;

namespace IndexBrowser
{
    public partial class FileContentsWindow : Window
    {
        public FileContentsWindow()
        {
            InitializeComponent();
        }

        public void SetBlob(Blob blob)
        {
            BlobId.Text = blob.Id.ToString();
            Contents.Text = HexDisplay.CreateHexDisplay(blob.RawContent);
        }
    }
}
