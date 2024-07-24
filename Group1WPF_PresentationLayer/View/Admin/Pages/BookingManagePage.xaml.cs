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
    /// Interaction logic for BookingManagePage.xaml
    /// </summary>
    public partial class BookingManagePage: UserControl {
        private readonly IBookingService _bookingService;
        public ObservableCollection<BookingDetail> Bookings { get; set; }

        public BookingManagePage(IBookingService bookingService) {
            InitializeComponent();
            _bookingService = bookingService;
            Bookings = new ObservableCollection<BookingDetail>();
            DataContext = this;

            LoadData();
            BookingDataGrid.ItemsSource = Bookings;
        }

        private async void LoadData() {
            Bookings.Clear();
            var bookings = await _bookingService.GetBookingDetails();
            foreach (var booking in bookings) {
                Bookings.Add(booking);
            }
        }
    }
}
