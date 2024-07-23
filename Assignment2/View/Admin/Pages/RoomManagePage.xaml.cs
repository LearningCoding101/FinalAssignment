using BusinessLayer.Services;
using DataAccessLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Assignment2.View.Admin.Pages {
    public partial class RoomManagePage: UserControl {
        private readonly IRoomService _roomService;
        private readonly IServiceProvider _serviceProvider;
        public ObservableCollection<RoomInformation> Rooms { get; set; }

        public RoomManagePage(IRoomService roomService) {
            InitializeComponent();
            _roomService = roomService;
            Rooms = new ObservableCollection<RoomInformation>();
            DataContext = this;

            LoadRooms();
        }

        private async void LoadRooms() {
            Rooms.Clear();
            var rooms = await _roomService.GetAllRooms();
            foreach (var room in rooms) {
                Rooms.Add(room);
            }
            RoomInfoDataGrid.ItemsSource = Rooms;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            var newRoom = new RoomInformation();
            var dialog = new RoomDialog(_roomService, newRoom);
            dialog.DataChanged += DetailWindow_DataChanged;
            dialog.CloseWindow += DetailWindow_Close;
            dialog.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) {
            //if (BookDataGrid.SelectedItem != null) {
            //    var book = (Book) BookDataGrid.SelectedItem;
            //    DetailWindow detailWindow = new DetailWindow(book);
            //    detailWindow.DataChanged += DetailWindow_DataChanged;
            //    detailWindow.CloseWindow += DetailWindow_Close;
            //    detailWindow.Show();
            //    this.Hide();
            //} else {
            //    MessageBox.Show("Please select a book to edit.");
            //}

        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e) {
            //if (BookDataGrid.SelectedItem == null) {
            //    MessageBox.Show("Please select an book to delete.");

            //} else {
            //    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this book?", "Confirmation", MessageBoxButton.YesNo);
            //    if (result == MessageBoxResult.Yes) {
            //        var book = (Book) BookDataGrid.SelectedItem;
            //        bookService.DeleteBook(book);
            //        FillDataGrid();
            //        BookDataGrid.SelectedItem = null;
            //    } else {
            //        BookDataGrid.SelectedItem = null;
            //    }
            //}

        }

        private void DetailWindow_DataChanged(object sender, EventArgs e) {
            LoadRooms();
            RoomInfoDataGrid.SelectedItem = null;
        }
        private void DetailWindow_Close(object sender, EventArgs e) {
        }

    }
}
