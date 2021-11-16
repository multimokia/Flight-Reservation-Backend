using System;
using System.Collections.Generic;

class CustomerManager
{
    private Dictionary<string, Customer> _customers;

    public CustomerManager()
    {
        _customers = new Dictionary<string, Customer>();
    }

    public void AddCustomer(string firstName, string lastName, string phoneNumber)
    {
        Customer customer = new Customer(firstName, lastName, phoneNumber);

        //If we already have a customer w/ this specification, we throw an exception
        if (hasCustomer(customer.Id))
            { throw new DuplicateCustomerException(customer); }

        //Otherwise add the customer to the dictionary
        _customers.Add(customer.Id, customer);
    }

    public void RemoveCustomer(string customerId)
    {
        Customer customer = null;

        _customers.TryGetValue(customerId, out customer);

        if (customer == null)
            { throw new CustomerNotFoundException(customerId); }

        //A customer may only be deleted if and only if they have no bookings
        if (customer.NumberOfBookings > 0)
            { throw new InvalidOperationException("A customer can only be removed if they have no bookings."); }

        _customers.Remove(customerId);
    }

    /// <summary>
    /// Checks if the given if is in the customers dictionary
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    public bool hasCustomer(string customerId)
    {
        return _customers.ContainsKey(customerId);
    }
}
