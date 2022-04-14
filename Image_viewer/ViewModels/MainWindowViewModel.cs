using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.IO;
using Avalonia.Media.Imaging;
using System.Reactive;
using Image_viewer.Models;

namespace Image_viewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {


        bool buttonEnable = false;

        bool ButtonEnable
        {
            get => buttonEnable;
            set => this.RaiseAndSetIfChanged(ref buttonEnable, value);
        }

        public ObservableCollection<Node> Items { get; }
        public ObservableCollection<Node> SelectedItems { get; }
        public string strFolder { get; set; }

        public ObservableCollection<Image> Folders { get; set; }

        List<string> Files;

        public class Image
        {
            public string Path { get; set; }
            public Bitmap Img { get; set; }
            
            public Image(string path)
            {
                Img = new Bitmap(path);
                Path = path;
            }
        }


        public MainWindowViewModel()
        {
            string root = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            strFolder = root; 

            Items = new ObservableCollection<Node>();
            Files = new List<string>();

            Folders = new ObservableCollection<Image>();
            DriveInfo[] All_Files = DriveInfo.GetDrives();

            for (int i = 0; i < All_Files.Length; i++)
            {
                Files.Add(All_Files[i].Name);
                Node rootNodee = new Node(All_Files[i].Name);
                Items.Add(rootNodee);
            }
            /*Node rootNode = new Node(strFolder);
            rootNode.Subfolders = GetSubfolders(strFolder);
            Items.Add(rootNode);*/
        }

        public void Update(List<string> imagesRoots, string Image)
        {
            Folders.Clear();
            Folders.Add(new Image(Image));
            for (int i = 0; i < imagesRoots.Count; i++)
                Folders.Add(new Image(imagesRoots[i]));

            if(imagesRoots.Count > 1)
            {
                ButtonEnable = true;
            }
            else ButtonEnable = false;
        }





      
    }
}

   
