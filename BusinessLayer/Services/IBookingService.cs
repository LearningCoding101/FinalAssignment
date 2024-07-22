using DataAccessLayer.Models;

namespace BusinessLayer.Services
{
    public interface IBookingService
    {
        Task AddBooking(int roomId, DateTime startDate, DateTime endDate, decimal actualPrice, int customerId);

        Task UpdateBooking(BookingReservation reservation, BookingDetail bookingDetail);

        Task<bool> CancelBooking(int bookingReservationId);

        Task<IEnumerable<BookingReservation>> GetAllBookings();

        Task<IEnumerable<BookingReservation>> GetBookingsByCustomerId(int customerId);

        Task<BookingReservation?> GetBookingReservationById(int bookingReservationId);

        Task<IEnumerable<BookingDetail>> GetBookingDetails();

        Task<BookingDetail?> GetBookingDetailById(int id);
    }
}
