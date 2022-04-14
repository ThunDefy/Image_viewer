using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.Collections.ObjectModel;

namespace Image_viewer.Models
{
    public class Node : INotifyPropertyChanged
    {

        public ObservableCollection<Node>? AllNodes { get; set; }
        public string FileName { get; set; }
        public string FullRoot { get; set; }

        public Node(string fullRoot)
        {
            AllNodes = new ObservableCollection<Node>();
            FullRoot = fullRoot;
            FileName = Path.GetFileName(fullRoot);
            if (FullRoot.Length <= 3) FileName = fullRoot;
        }

        public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
			else return;
		}       

        public void GetSubfolders()
        {
            try
            {
                string[] Filter = new[] { ".ico", ".png", ".jpg", ".jpeg" };

                IEnumerable<string> items = Directory.EnumerateDirectories(FullRoot, "*", SearchOption.TopDirectoryOnly); // Папки
                IEnumerable<string> files = Directory.EnumerateFiles(FullRoot).Where(file => Filter.Any(file.ToLower().EndsWith)).ToList(); // Изображения

                foreach (string item in items) AllNodes.Add(new Node(item));
                foreach (string file in files) { AllNodes.Add(new Node(file)); };

            }
            catch { }

        }
    }
}

