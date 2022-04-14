using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Image_viewer.Models;
using Image_viewer.ViewModels;
using System.IO;
using System.Linq;


namespace Image_viewer.Views
{
    public partial class MainWindow : Window
    {
        private Carousel _carousel;
        private Button _goBack;
        private Button _goNext;
        //private int imgCount = 0;


        public MainWindow()
        {
            this.InitializeComponent();

            /*_goBack.Click += (h,z) => GoTo(-1);
            _goNext.Click += (h,z) => GoTo(1);*/

            _goBack.Click += (h, z) => _carousel.Previous();
            _goNext.Click += (h, z) => _carousel.Next();

        }



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _carousel = this.FindControl<Carousel>("carousel");
            _goBack = this.FindControl<Button>("GoBack");
            _goNext = this.FindControl<Button>("GoNext");


        }

        private void TransitionChanged(object sender, PointerReleasedEventArgs e)
        {

            string[] Filter = new[] { ".ico", ".png", ".jpg", ".jpeg" };
            TreeViewItem Item = sender as TreeViewItem;
            Node selected = Item.DataContext as Node;

            if (selected != null && Filter.Any(selected.FullRoot.ToLower().EndsWith)) // Проверка формата файла
            {
                string path = selected.FullRoot.Substring(0, selected.FullRoot.IndexOf(selected.FileName));
                var files = Directory.EnumerateFiles(path).Where(file => Filter.Any(file.ToLower().EndsWith)).ToList();
                files.Remove(selected.FullRoot);  // Удалить дубликат 1го выбранного
                var context = this.DataContext as MainWindowViewModel;
                context.Update(files, selected.FullRoot);

            }

        }

        private void LoadNodes(object sender, TemplateAppliedEventArgs e)
        {
            ContentControl treeViewItem = sender as ContentControl;
            Node selected = treeViewItem.DataContext as Node;
            if (selected != null) selected.GetSubfolders();
        }
    }
}
