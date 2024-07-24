using Assignment2.View.Admin.Pages;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Assignment2.View.CustomerView.Pages {
    /// <summary>
    /// Interaction logic for RoomsAvailable.xaml
    /// </summary>
    public partial class RoomsAvailable: UserControl, INotifyPropertyChanged{
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        private readonly IServiceProvider _serviceProvider;
        private readonly int _customerId;
        public bool IsAnyRoomSelected
        {
            get { return _selectedRooms.Count > 0; }
        }
        public ObservableCollection<RoomInformation> Rooms { get; set; }
        private List<RoomInformation> _selectedRooms;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyIsAnyRoomSelectedChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAnyRoomSelected)));
        }

        public RoomsAvailable(IRoomService roomService, IBookingService bookingService, int customerId) {
            InitializeComponent();
            _roomService = roomService;
            _bookingService = bookingService;
            _customerId = customerId;
            Rooms = new ObservableCollection<RoomInformation>();
            DataContext = this;
            _selectedRooms = new List<RoomInformation>();
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

        private void BookButton_Click(object sender, RoutedEventArgs e) {
            // Handle the booking process for selected rooms
            var newReservation = new BookingReservation();
            var newReservationDetail = new BookingDetail();
            var dialog = new BookingDialog(_bookingService, _selectedRooms, _customerId);
            dialog.DataChanged += DetailWindow_DataChanged;
            dialog.CloseWindow += DetailWindow_Close;
            dialog.Show();
            // Clear the selection on UI
            ClearCheckBoxes();
        }

        private void DetailWindow_DataChanged(object sender, EventArgs e) {
            LoadRooms();
            RoomInfoDataGrid.SelectedItem = null;
        }
        private void DetailWindow_Close(object sender, EventArgs e) {
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e) {
            var checkBox = sender as CheckBox;
            var room = checkBox.Tag as RoomInformation;

            if (checkBox.IsChecked == true) {
                _selectedRooms.Add(room);
            } else {
                _selectedRooms.Remove(room);
            }
            NotifyIsAnyRoomSelectedChanged();
        }
        private void ClearCheckBoxes() {
            foreach (var item in RoomInfoDataGrid.Items) {
                var row = RoomInfoDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null) {
                    var checkBox = FindVisualChild<CheckBox>(row);
                    if (checkBox != null) {
                        checkBox.IsChecked = false;
                    }
                }
            }
            _selectedRooms.Clear();
            NotifyIsAnyRoomSelectedChanged();

        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++) {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T) {
                    return (T) child;
                } else {
                    var childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null) {
                        return childOfChild;
                    }
                }
            }
            return null;
        }
    }
}
