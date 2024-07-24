using BusinessLayer.Services;
using DataAccessLayer.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Assignment2.View.Admin.Pages {
    public partial class RoomDialog: Window {
        public event EventHandler DataChanged;
        public event EventHandler CloseWindow;
        private readonly IRoomService _roomService;
        public RoomInformation SelectRoom { get; set; }

        public RoomDialog(IRoomService roomService, RoomInformation selectRoom) {
            InitializeComponent();
            _roomService = roomService;
            if (selectRoom.RoomId == 0) {
                SelectRoom = selectRoom;
            } else {
                LoadData(selectRoom);
                SelectRoom = selectRoom;
            }
            LoadType();

        }

        private async void LoadType() {
            var types = await _roomService.GetAllRoomTypes();
            RoomTypeComboBox.ItemsSource = types;
            RoomTypeComboBox.DisplayMemberPath = "RoomTypeName";
            RoomTypeComboBox.SelectedValuePath = "RoomTypeId";
        }

        private void LoadData(RoomInformation r) {
                RoomIdTextBox.Text = r.RoomId.ToString();
                RoomIdTextBox.IsEnabled = false;
                RoomNumberTextBox.Text = r.RoomNumber;
                DescriptionTextBox.Text = r?.RoomDetailDescription;
                CapacityTextBox.Text = r?.RoomMaxCapacity.ToString();
                PriceTextBox.Text = r?.RoomPricePerDay.ToString();
                StatusTextBox.Text = r?.RoomStatus.ToString();
                RoomTypeComboBox.SelectedValue = r?.RoomTypeId.ToString();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e) {
            if (ValidateInputs(out string errorMessage)) {
                if (SelectRoom.RoomId == 0) {
                    RoomInformation room = new RoomInformation() {
                        RoomNumber = RoomNumberTextBox.Text,
                        RoomDetailDescription = DescriptionTextBox.Text,
                        RoomMaxCapacity = int.TryParse(CapacityTextBox.Text, out int quantity) ? quantity : 0,
                        RoomPricePerDay = decimal.TryParse(PriceTextBox.Text, out decimal price) ? price : 0,
                        RoomStatus = (byte?) (!byte.TryParse(StatusTextBox.Text, out byte status) ? 0 : status),
                        RoomTypeId = int.TryParse(RoomTypeComboBox.SelectedValue?.ToString(), out int type) ? type : 0
                    };
                    await _roomService.AddRoom(room);
                    MessageBox.Show("Add success!");
                } else {
                    //RoomInformation room = new RoomInformation() {

                    SelectRoom.RoomNumber = RoomNumberTextBox.Text;
                    SelectRoom.RoomDetailDescription = DescriptionTextBox.Text;
                    SelectRoom.RoomMaxCapacity = int.TryParse(CapacityTextBox.Text, out int quantity) ? quantity : 0;
                    SelectRoom.RoomPricePerDay = decimal.TryParse(PriceTextBox.Text, out decimal price) ? price : 0;
                    SelectRoom.RoomStatus = (byte?) (!byte.TryParse(StatusTextBox.Text, out byte status) ? 0 : status);
                    SelectRoom.RoomTypeId = int.TryParse(RoomTypeComboBox.SelectedValue?.ToString(), out int type) ? type : 0;
                    //};
                    await _roomService.UpdateRoom(SelectRoom);
                    MessageBox.Show("Update success!");
                }

                DataChanged?.Invoke(this, EventArgs.Empty);
                this.Close();
            } else {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private bool ValidateInputs(out string errorMessage) {
            errorMessage = "";
            string pattern = @"^(?:[A-Z][a-z]*|[A-Z][a-z]*[\W]*)(?:\s[A-Z][a-z]*|[\W])*?$";

            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text) || DescriptionTextBox.Text.Length < 5 || DescriptionTextBox.Text.Length > 500) {
                errorMessage += "Description must be between 5 and 500 characters long.\n";
            }

            if(!int.TryParse(CapacityTextBox.Text, out int quantity) || quantity < 0 || quantity >= 4000000)
            {
                errorMessage += "Quantity must be a number between 0 and 3,999,999.\n";
            }

            if (!double.TryParse(PriceTextBox.Text, out double price) || price < 0 || price >= 4000000) {
                errorMessage += "Price must be a number between 0 and 3,999,999.\n";
            }

            if (string.IsNullOrWhiteSpace(RoomNumberTextBox.Text)) {
                errorMessage += "Room number cannot be empty.\n";
            }

            if (string.IsNullOrWhiteSpace(StatusTextBox.Text)) {
                errorMessage += "Status cannot be empty.\n";
            }

            if (RoomTypeComboBox.SelectedValue == null) {
                errorMessage += "Room type must be selected.\n";
            }

            return string.IsNullOrEmpty(errorMessage);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) {
            CloseWindow?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
    }
}
