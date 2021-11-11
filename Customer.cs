class Customer
{
    private int customerId;
    private string firstName;
    private string lastName;
    private string phone;
    private int bookings;

    public Customer(int cId,string fname,string lname, string ph)
    {
        bookings = 0;
        customerId = cId;
        firstName = fname;
        lastName = lname;
        phone = ph;
    }

    public int getId()
    {
        return customerId;
    }

    public string getFirstName()
    {
        return firstName;
    }

    public string getLastName()
    {
        return lastName;
    }

    public string getPhone()
    {
        return phone;
    }

    public int getNumBookings()
    {
        return bookings;
    }

    public string toString()
    {
        return $"Customer {customerId}:\n\tName: {firstName} {lastName}\n\tPhone: {phone}\n\tBookings: {bookings}";
    }
}
