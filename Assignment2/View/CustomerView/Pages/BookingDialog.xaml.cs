using BusinessLayer.Services;
using DataAccessLayer.Models;
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
using System.Windows.Shapes;

namespace Assignment2.View.CustomerView.Pages {
    /// <summary>
    /// Interaction logic for BookingDialog.xaml
    /// </summary>
    public partial class BookingDialog: Window {

        public event EventHandler DataChanged;
        public event EventHandler CloseWindow;
        private readonly IBookingService _bookingService;
        private readonly int _customerId;
        public List<RoomInformation> SelectedRooms { get; set; }
        public BookingDialog(IBookingService bookingService, List<RoomInformation> selectedRoom, int customerId) {
            InitializeComponent();
            _bookingService = bookingService;
            SelectedRooms = selectedRoom;
            _customerId = customerId;
        }

        // AddBooking(decimal totalPrice, int customerId, List<BookingDetail> bookingDetails)
        private async void SaveButton_Click(object sender, RoutedEventArgs e) {
            if (FromDatePicker.SelectedDate == null || ToDatePicker.SelectedDate == null) {
                MessageBox.Show("Please select both start and end dates.");
                return;
            }

            DateOnly startDate = DateOnly.FromDateTime(FromDatePicker.SelectedDate.Value);
            DateOnly endDate = DateOnly.FromDateTime(ToDatePicker.SelectedDate.Value);

            if (endDate < startDate) {
                MessageBox.Show("End date cannot be earlier than start date.");
                return;
            }

            try {
                decimal? totalPrice = 0;
                List<BookingDetail> bookingDetails = new List<BookingDetail>();
                foreach (var room in SelectedRooms) {
                    totalPrice += room.RoomPricePerDay;
                    MessageBox.Show(room.RoomPricePerDay.ToString());
                }

                foreach (var room in SelectedRooms) {
                    var tmp = new BookingDetail() {
                        RoomId = room.RoomId,
                        StartDate = startDate,
                        EndDate = endDate,
                        ActualPrice = room.RoomPricePerDay,
                    };
                }

                await _bookingService.AddBooking(totalPrice, _customerId, bookingDetails);
                DataChanged?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Booking successfully created.");
                this.Close();
            } catch (Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) {
            CloseWindow?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
    }
}
