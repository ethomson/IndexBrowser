using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;

using Dogged;

namespace IndexBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Repository repository;
        private Dogged.Index index;

        public MainWindow()
        {
            InitializeComponent();

            FileOpen.Click += (sender, args) =>
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    SetRepository(dialog.SelectedPath);
                }
            };

            Directory.MouseRightButtonDown += (sender, args) => args.Handled = true;

            Details.Click += (sender, args) =>
            {
                IndexItem item = (IndexItem)Directory.SelectedItem;

                IndexEntry entry = repository.Index[item.Name, int.Parse(item.Stage)];

                DetailsWindow dialog = new DetailsWindow();
                dialog.SetIndexEntry(entry);
                dialog.ShowDialog();
            };

            FileContents.Click += (sender, args) =>
            {
                IndexItem item = (IndexItem)Directory.SelectedItem;

                FileContentsWindow dialog = new FileContentsWindow();

                using (Blob blob = repository.Lookup<Blob>(new ObjectId(item.Id)))
                {
                    dialog.SetBlob(blob);
                }

                dialog.ShowDialog();
            };

            Repository.IsEnabled = true;
            Directory.ItemsSource = new List<IndexItem>();
            Directory.IsEnabled = false;
        }

        private void SetRepository(string filename)
        {
            if (repository != null)
            {
                repository.Dispose();
                index.Dispose();
            }

            repository = new Repository(filename);
            index = repository.Index;

            List<IndexItem> entries = new List<IndexItem>();

            foreach(IndexEntry entry in repository.Index)
            {
                entries.Add(new IndexItem()
                {
                    Name = entry.Path,
                    Stage = ((int)entry.Stage).ToString(),
                    Mode = Convert.ToString((int)entry.Mode, 8),
                    Id = entry.Id.ToString()
                });
            }

            Repository.IsEnabled = true;
            Repository.Text = filename;
            Directory.IsEnabled = true;
            Directory.ItemsSource = entries;
        }
    }

    public class IndexItem
    {
        public String Name
        {
            get;
            set;
        }

        public String Stage
        {
            get;
            set;
        }

        public String Mode
        {
            get;
            set;
        }

        public String Id
        {
            get;
            set;
        }
    }
}
