using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Dogged;

namespace IndexBrowser
{
    public partial class DetailsWindow : Window
    {
        public DetailsWindow()
        {
            InitializeComponent();

            OK.Click += (sender, args) => Close();
        }

        public void SetIndexEntry(IndexEntry entry)
        {
            Path.Text = entry.Path;
            Id.Text = entry.Id.ToString();
            Stage.Text = ((int)entry.Stage).ToString();
            Mode.Text = Convert.ToString((int)entry.Mode, 8);
            Change.Text = entry.ChangeTime.Seconds.ToString() + ":" + entry.ChangeTime.Nanoseconds.ToString();
            Modify.Text = entry.ModificationTime.Seconds.ToString() + ":" + entry.ModificationTime.Nanoseconds.ToString();
            Device.Text = entry.Device.ToString();
            Inode.Text = entry.Inode.ToString();
            UserId.Text = entry.UserId.ToString();
            GroupId.Text = entry.GroupId.ToString();
            Size.Text = entry.FileSize.ToString();
            Flags.Text = ((int)entry.Flags).ToString();
        }
    }
}
