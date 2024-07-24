using DataAccessLayer.Models;

namespace BusinessLayer.Services
{
    public interface IBookingService
    {
        Task AddBooking(decimal? totalPrice, int customerId, List<BookingDetail> bookingDetails);

        Task UpdateBooking(BookingReservation reservation, BookingDetail bookingDetail);

        Task<string> CheckIn(BookingReservation reservation);

        Task<string> CheckOut(BookingReservation reservation);

        Task<string> CancelBooking(BookingReservation reservation);

        Task<IEnumerable<BookingReservation>> GetAllBookings();

        Task<IEnumerable<BookingDetail>> GetBookingsByCustomerId(int customerId);

        Task<BookingReservation?> GetBookingReservationById(int bookingReservationId);

        Task<IEnumerable<BookingDetail>> GetBookingDetails();

        Task<BookingDetail?> GetBookingDetailById(int id);
    }
}
