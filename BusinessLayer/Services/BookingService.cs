using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services
{
    public class BookingService : IBookingService
    {
        private readonly BookingDetailRepository _bookingDetailRepository;
        private readonly BookingReservationRepository _bookingReservationRepository;

        public BookingService(BookingDetailRepository bookingDetailRepository, BookingReservationRepository bookingReservationRepository)
        {
            _bookingDetailRepository = bookingDetailRepository;
            _bookingReservationRepository = bookingReservationRepository;
        }

        public async Task AddBooking(decimal? totalPrice, int customerId, List<BookingDetail> bookingDetails)
        {
            var reservations = await _bookingReservationRepository.GetAll();
            int nextBookingReservationId = reservations.Count() + 1;

            var bookingReservation = new BookingReservation
            {
                BookingReservationId = nextBookingReservationId,
                BookingDate = DateOnly.FromDateTime(DateTime.Now),
                TotalPrice = totalPrice,
                CustomerId = customerId,
                BookingStatus = 1,
            };

            await _bookingReservationRepository.Add(bookingReservation);

            foreach (var bookingDetail in bookingDetails) {
                await _bookingDetailRepository.Add(bookingDetail);
            }

        }

        public async Task<string> CancelBooking(BookingReservation reservation)
        {
            if (reservation.BookingStatus == 1) {
                await _bookingReservationRepository.Update(reservation);
                return "Cancel success";
            } else if (reservation.BookingStatus == 0) {
                return "This reservation was canceled";
            } else if (reservation.BookingStatus == 3) {
                return "This reservation was checked in";
            } else {
                return "This reservation was checked out";
            }
        }

        public async Task<string> CheckIn(BookingReservation reservation) {
            if (reservation.BookingStatus == 1) {
                reservation.BookingStatus = 3; // Update the status to "Checked In"
                await _bookingReservationRepository.Update(reservation);
                return "Check-in success";
            } else if (reservation.BookingStatus == 0) {
                return "This reservation was canceled";
            } else if (reservation.BookingStatus == 3) {
                return "This reservation was already checked in";
            } else {
                return "This reservation was checked out";
            }
        }

        public async Task<string> CheckOut(BookingReservation reservation) {
            if (reservation.BookingStatus == 3) {
                reservation.BookingStatus = 4; // Update the status to "Checked In"
                await _bookingReservationRepository.Update(reservation);
                return "Check-out success";
            } else if (reservation.BookingStatus == 0) {
                return "This reservation was canceled";
            } else if (reservation.BookingStatus == 1) {
                return "This reservation has not checked in yet";
            } else {
                return "This reservation was checked out";
            }
        }

        public async Task<IEnumerable<BookingReservation>> GetAllBookings()
        {
            return await _bookingReservationRepository.GetAll();
        }

        public async Task<BookingDetail?> GetBookingDetailById(int id)
        {
            return await _bookingDetailRepository.GetById(id);
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingDetails()
        {
            return await _bookingDetailRepository.GetAll();
        }

        public async Task<BookingReservation?> GetBookingReservationById(int bookingReservationId)
        {
            return await _bookingReservationRepository.GetById(bookingReservationId);
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingsByCustomerId(int customerId)
        {
            return await _bookingDetailRepository.GetBookingsByCustomerId(customerId);
        }

        public async Task UpdateBooking(BookingReservation reservation, BookingDetail bookingDetail)
        {
            await _bookingReservationRepository.Update(reservation);
            await _bookingDetailRepository.Update(bookingDetail);
        }
    }
}
