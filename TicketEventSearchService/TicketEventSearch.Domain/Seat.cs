namespace TicketEventSearch.Domain;

public class Seat
{
    public Guid SeatId { get; private set; }
    public Guid SchemeId { get; private set; }
    public string Sector { get; private set; }
    public int? Row { get; private set; }
    public int? SeatNumber { get; private set; }
    public virtual Scheme? Scheme { get; private set; }
    public virtual ICollection<Ticket> Tickets { get; private set; }

    private Seat() { }

    public Seat(Guid seatId, Guid schemeId, string sector, int? row, int seatNumber)
    {
        #region seatId validation
        if (string.IsNullOrWhiteSpace(seatId.ToString()))
        {
            throw new ArgumentException("Wrong SeatId", nameof(seatId));
        }
        #endregion

        #region schemeId validation
        if (string.IsNullOrWhiteSpace(schemeId.ToString()))
        {
            throw new ArgumentException("Wrong SchemeId", nameof(schemeId));
        }
        #endregion

        #region sector validation
        if (string.IsNullOrWhiteSpace(sector))
        {
            throw new ArgumentException("Sector is empty", nameof(sector));
        }

        if (sector.Length > 50)
        {
            throw new ArgumentException("Sector length more then 50", nameof(sector));
        }
        #endregion

        #region row validation
        if (row is not null && (row <= 0 || row > 200))
        {
            throw new ArgumentException("Incorrect Row value", nameof(row));
        }
        #endregion

        #region seatNumber validation
        if (seatNumber <= 0 || seatNumber > 2000)
        {
            throw new ArgumentException("Wrong Seat number", nameof(seatNumber));
        }
        #endregion

        SeatId = seatId;
        SchemeId = schemeId;
        Sector = sector;
        Row = row;
        SeatNumber = seatNumber;
    }
}
