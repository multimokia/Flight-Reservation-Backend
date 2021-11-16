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
    /// Number of bookings the customer has
    /// </summary>
    private int _numberOfBookings;
    public int NumberOfBookings {get { return _numberOfBookings; }}

    public Customer(string firstName, string lastName, string phoneNumber)
    {
        //For the sake of easy value comparison, we'll hash the id so we know if two customers
        //share the same info through the value of its id
        this._id = Utilities.HashString($"{firstName}{lastName}{phoneNumber}".Replace(" ", "").ToLower());
        this._firstName = firstName;
        this._lastName = lastName;
        this._phoneNumber = phoneNumber;
        this._numberOfBookings = 0;
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
    /// ToString override
    /// </summary>
    /// <returns>A string representation of the Customer</returns>
    public override string ToString()
    {
        return $"Customer {_id}:\n\tName: {_firstName} {_lastName}\n\tPhone: {_phoneNumber}\n\tBookings: {_numberOfBookings}";
    }
}
