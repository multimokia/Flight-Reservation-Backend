using System.Collections.Generic;

public class Customer
{
    /// <summary>
    /// Customer id
    /// </summary>
    private string _id;
    public string Id {get { return _id; }}

    /// <summary>
    /// Customer's first name
    /// </summary>
    private string _firstName;
    public string FirstName {get { return _firstName; }}

    /// <summary>
    /// Customer's last name
    /// </summary>
    private string _lastName;
    public string LastName {get { return _lastName; }}

    /// <summary>
    /// Customer's phone number
    /// </summary>
    private string _phoneNumber;
    public string PhoneNumber {get { return _phoneNumber; }}

    /// <summary>
    /// A reference to the bookings this customer has
    /// </summary>
    private List<string> _bookings;
    public List<string> Bookings {get { return _bookings; }}

    public Customer(string firstName, string lastName, string phoneNumber)
    {
        //For the sake of easy value comparison, we'll hash the id so we know if two customers
        //share the same info through the value of its id
        this._id = Utilities.HashString($"{firstName}{lastName}{phoneNumber}".Replace(" ", "").ToLower());
        this._firstName = firstName;
        this._lastName = lastName;
        this._phoneNumber = phoneNumber;
        this._bookings = new List<string>();
    }

    /// <summary>
    /// Equals function. Checks if two customers are the same
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Customer other)
    {
        return this.Id == other.Id;
    }

    /// <summary>
    /// Adds a booking to the customer's list of bookings
    /// </summary>
    /// <param name="bookingId">id of the booking to add</param>
    public void AddBookingReference(string bookingId)
    {
        //Safety check to prevent duplicate data, this should never happen
        if (!this._bookings.Contains(bookingId))
            { this._bookings.Add(bookingId); }
    }

    /// <summary>
    /// Removes a booking from the customer's list of bookings
    /// </summary>
    /// <param name="bookingId">id of the booking to remove</param>
    public void RemoveBookingReference(string bookingId)
    {
        this._bookings.Remove(bookingId);
    }

    /// <summary>
    /// ToString override
    /// </summary>
    /// <returns>A string representation of the Customer</returns>
    public override string ToString()
    {
        return $"Customer {_id}:\n\tName: {_firstName} {_lastName}\n\tPhone: {_phoneNumber}\n\tBookings: {_bookings.Count}";
    }
}
