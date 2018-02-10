using System;
using System.Collections.Generic;
using System.Linq;
using Address_Book.Controllers;

namespace Address_Book.Models
{
    public class Contact
    {
        public static Dictionary<int,Contact> AllContacts = new Dictionary<int,Contact>();
        private static int _ID = 0;
        private Address _address;
        private string _contactName, _phoneNumber;
        public static Dictionary<int,Contact> GetAllContacts()
        {
            return AllContacts;
        }
        public Contact(string contactName ="Error, initialized as null.", string phoneNumber="1111111111", string streetAddress="", string city="", string state="", string zipCode="")
        {
            _contactName = contactName;
            _phoneNumber = FormatPhone(phoneNumber);
            _address = new Address (streetAddress,city,state,zipCode);
            SaveContact();
        }
        public string GetName()
        {
            return _contactName;
        }
        public void SetName(string name)
        {
            _contactName = name;
        }
        public string GetPhone(bool isFormated)
        {
            if (isFormated == true)
            {
                return _phoneNumber;
            }
            else 
            {
                return new String(_phoneNumber.Where(Char.IsDigit).ToArray());
            }
        }
        public void SetPhone(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }
        public int GetID()
        {
            return _ID;
        }
        public Address GetAddress()
        {
            return _address;
        }
        public void SaveContact()
        {
            _ID++;
            AllContacts.Add(_ID,this);
        }
        private string FormatPhone(string phoneNumber) 
        {
            if (phoneNumber.Length == 10)
            {
                return String.Format("{0:(###) ###-####}", Convert.ToInt64(phoneNumber));
                
            }
            else
            {
                return "Error";
            }
        }
    }

    public class Address
    {
        private List<string> _containedAddress = new List<string>();
        private string _fullAddress, _streetAddress, _city, _state, _zipCode;
        public bool SetFullAddress() //returns true if successful
        {
            if (string.IsNullOrEmpty(_streetAddress) || string.IsNullOrEmpty(_city)|| string.IsNullOrEmpty(_state) || string.IsNullOrEmpty(_zipCode))
            {
                return false;
            }
            else
            {
                _containedAddress.Clear();
                _fullAddress = String.Format("{0}, {1}, {2} {3}", _streetAddress, _city, _state, _zipCode);
                _containedAddress.Add(_fullAddress);
                _containedAddress.Add(_streetAddress);
                _containedAddress.Add(_city);
                _containedAddress.Add(_state);
                _containedAddress.Add(_zipCode);
                return true;
            }
        }
        public Address(string streetAddress, string city, string state, string zipCode)
        {
            if (string.IsNullOrEmpty(streetAddress) || string.IsNullOrEmpty(city)|| string.IsNullOrEmpty(state) || string.IsNullOrEmpty(zipCode))
            {
                return;
            }
            else
            {
                _streetAddress = streetAddress;
                _city = city;
                _state = state;
                _zipCode = zipCode;
                SetFullAddress();
            }
        }
        public void SetStreetAddress(string streetAddress)
        {
            _streetAddress = streetAddress;
        }
        public void SetCity(string city)
        {
            _city = city;
        }
        public void SetState(string state)
        {
            _state = state;
        }
        public void SetZipCode(string zipCode)
        {
            _zipCode = zipCode;
        }
        public string GetFullAddress()
        {
            return _fullAddress;
        }
        public string GetStreetAddress()
        {
            return _streetAddress;
        }
        public string GetCity()
        {
            return _city;
        }
        public string GetState()
        {
            return _state;
        }
        public string GetZipCode()
        {
            return _zipCode;
        }
    }
}