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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyPlaylistApp.EntityModel;
using MyPlaylistApp.Pages;

namespace MyPlaylistApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyPlaylistAppEntities _db = new MyPlaylistAppEntities();
        public static DataGrid datagrid;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            DGridMusic.ItemsSource = _db.MusicDBs.ToList();
            datagrid = DGridMusic;
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            int ID = (DGridMusic.SelectedItem as MusicDB).ID;
            EditDataWin editDataWin = new EditDataWin(ID);
            editDataWin.ShowDialog();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int ID = (DGridMusic.SelectedItem as MusicDB).ID;
            var deleteMusic = _db.MusicDBs.Where(m => m.ID == ID).Single();
            _db.MusicDBs.Remove(deleteMusic);
            _db.SaveChanges();
            DGridMusic.ItemsSource = _db.MusicDBs.ToList();
            MessageBox.Show("Delete Data Success!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddDataWin addDataWin = new AddDataWin();
            addDataWin.ShowDialog();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = _db.MusicDBs.Where(x => x.Song.Contains(TBoxSearch.Text) || x.Artist.Contains(TBoxSearch.Text) || x.Album.Contains(TBoxSearch.Text)).ToList();
            DGridMusic.ItemsSource = result;
        }
    }
}
