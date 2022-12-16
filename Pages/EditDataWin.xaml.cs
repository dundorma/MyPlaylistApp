using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Xml.Linq;
using MyPlaylistApp.EntityModel;

namespace MyPlaylistApp.Pages
{
    /// <summary>
    /// Interaction logic for EditDataWin.xaml
    /// </summary>
    public partial class EditDataWin : Window
    {
        MyPlaylistAppEntities _db = new MyPlaylistAppEntities();
        int ID;
        public EditDataWin(int MusicID)
        {
            InitializeComponent();
            ID = MusicID;

            var dataState = (from m in _db.MusicDBs where m.ID == ID select m).First();
            TBSong.Text = dataState.Song;
            TBArtist.Text = dataState.Artist;
            TBAlbum.Text = dataState.Album;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            MusicDB updateMusicDB = (from m in _db.MusicDBs where m.ID == ID select m).Single();
            updateMusicDB.Song = TBSong.Text;
            updateMusicDB.Artist = TBArtist.Text;
            updateMusicDB.Album = TBAlbum.Text;
            _db.SaveChanges();
            MainWindow.datagrid.ItemsSource = _db.MusicDBs.ToList();
            MessageBox.Show("Edit Data Success!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
