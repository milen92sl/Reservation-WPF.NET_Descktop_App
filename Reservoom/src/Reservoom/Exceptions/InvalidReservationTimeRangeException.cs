using ReservetionDesktopApp.Models;
using System;

namespace ReservetionDesktopApp.Exceptions
{
    public class InvalidReservationTimeRangeException : Exception
    {
        public Reservation Reservation { get; }

        public InvalidReservationTimeRangeException(Reservation reservation)
        {
            Reservation = reservation;
        }
    }
}
