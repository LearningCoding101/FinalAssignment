using BusinessLayer.Services;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Assignment2.View.Admin.Pages {
    /// <summary>
    /// Interaction logic for ReservationManage.xaml
    /// </summary>
    public partial class ReservationManage: UserControl {
        private readonly IBookingService _bookingService;
        public ObservableCollection<BookingReservation> Reservations { get; set; }

        public ReservationManage(IBookingService bookingService) {
            InitializeComponent();
            _bookingService = bookingService;
            Reservations = new ObservableCollection<BookingReservation>();
            DataContext = this;

            LoadData();
            ReservationDataGrid.ItemsSource = Reservations;
        }

        private async void LoadData() {
            Reservations.Clear();
            var reservations = await _bookingService.GetAllBookings();
            foreach (var reservation in reservations) {
                Reservations.Add(reservation);
            }
        }

        private async void CheckInButton_Click(object sender, RoutedEventArgs e) {
            if (ReservationDataGrid.SelectedItem is BookingReservation selectedReservation) {
                string noti = await _bookingService.CheckIn(selectedReservation);
                LoadData();
                MessageBox.Show(noti);
            } else {
                MessageBox.Show("Please select a reservation.");
            }
        }

        private async void CheckOutButton_Click(object sender, RoutedEventArgs e) {
            if (ReservationDataGrid.SelectedItem is BookingReservation selectedReservation) {
                string noti = await _bookingService.CheckOut(selectedReservation);
                LoadData();
                MessageBox.Show(noti);
            } else {
                MessageBox.Show("Please select a reservation.");
            }
        }
    }
}
