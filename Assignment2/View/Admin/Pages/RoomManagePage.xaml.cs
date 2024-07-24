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
            if (RoomInfoDataGrid.SelectedItem != null) {
                var room = (RoomInformation) RoomInfoDataGrid.SelectedItem;
                var dialog = new RoomDialog(_roomService, room);
                dialog.DataChanged += DetailWindow_DataChanged;
                dialog.CloseWindow += DetailWindow_Close;
                dialog.Show();
            } else {
                MessageBox.Show("Please select a room to edit.");
            }

        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e) {
            if (RoomInfoDataGrid.SelectedItem == null) {
                MessageBox.Show("Please select a room to delete.");
            } else {
                MessageBoxResult result = MessageBox.Show("Are you sure to delete this room?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) {
                    var room = (RoomInformation) RoomInfoDataGrid.SelectedItem;
                    await _roomService.DeleteRoom(room.RoomId);
                    LoadRooms();
                }
                RoomInfoDataGrid.SelectedItem = null;
            }

        }

        private void DetailWindow_DataChanged(object sender, EventArgs e) {
            LoadRooms();
            RoomInfoDataGrid.SelectedItem = null;
        }
        private void DetailWindow_Close(object sender, EventArgs e) {
        }

    }
}
