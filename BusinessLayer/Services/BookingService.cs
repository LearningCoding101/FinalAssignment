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

        public async Task AddBooking(int roomId, DateTime startDate, DateTime endDate, decimal actualPrice, int customerId)
        {
            var reservations = await _bookingReservationRepository.GetAll();
            int nextBookingReservationId = reservations.Count() + 1;

            var bookingReservation = new BookingReservation
            {
                BookingDate = DateOnly.FromDateTime(DateTime.Now),
                TotalPrice = actualPrice,
                CustomerId = customerId,
                BookingStatus = 1,
            };

            await _bookingReservationRepository.Add(bookingReservation);

            var bookingDetail = new BookingDetail
            {
                BookingReservationId = nextBookingReservationId,
                RoomId = roomId,
                StartDate = DateOnly.FromDateTime(startDate),
                EndDate = DateOnly.FromDateTime(endDate),
                ActualPrice = actualPrice,
            };

            await _bookingDetailRepository.Add(bookingDetail);
        }

        public async Task<bool> CancelBooking(int bookingReservationId)
        {
            var bookingReservation = await _bookingReservationRepository.GetById(bookingReservationId);
            if (bookingReservation == null)
            {
                return false;
            }

            bookingReservation.BookingStatus = 0;
            await _bookingReservationRepository.Update(bookingReservation);
            return true;
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

        public async Task<IEnumerable<BookingReservation>> GetBookingsByCustomerId(int customerId)
        {
            return await _bookingReservationRepository.GetReservationsByCustomerId(customerId);
        }

        public async Task UpdateBooking(BookingReservation reservation, BookingDetail bookingDetail)
        {
            await _bookingReservationRepository.Update(reservation);
            await _bookingDetailRepository.Update(bookingDetail);
        }
    }
}
