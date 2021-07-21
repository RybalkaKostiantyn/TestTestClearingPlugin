using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTestClearingCommonLogic
{
    public class SqlParameter
    {
        public Email EmailParameter { get; set; }
        public UserId UserIdParameter { get; set; }
        public UserExternalId UserExternalIdParameter { get; set; }
        public StoreId StoreIdParameter { get; set; }
        public OrderId OrderIdParameter { get; set; }
        public CurrencyId CurrencyIdParameter { get; set; }
        public CustomParameter CustomParameter { get; set; }
    }
    public class Email
    {
        string KEY_VALUE = "@Email";
        public string Key
        {
            get { return KEY_VALUE; }
        }
        public string Value
        {
            get;
            set;
        }
    }

    public class UserId
    {
        string KEY_VALUE = "@UserId";
        public string Key
        {
            get { return KEY_VALUE; }
        }
        public string Value
        {
            get;
            set;
        }
    }

    public class UserExternalId
    {
        string KEY_VALUE = "@UserExternalId";
        public string Key
        {
            get { return KEY_VALUE; }
        }
        public string Value
        {
            get;
            set;
        }
    }

    public class StoreId
    {
        string KEY_VALUE = "@StoreId";
        public string Key
        {
            get { return KEY_VALUE; }
        }
        public string Value
        {
            get;
            set;
        }
    }

    public class OrderId
    {
        string KEY_VALUE = "@OrderId";
        public string Key
        {
            get { return KEY_VALUE; }
        }
        public string Value
        {
            get;
            set;
        }
    }

    public class CurrencyId
    {
        string KEY_VALUE = "@CurrencyId";
        public string Key
        {
            get { return KEY_VALUE; }
        }
        public string Value
        {
            get;
            set;
        }
    }

    public class CustomParameter
    {
        string KEY_VALUE = "@CustomParameter";
        public string Key
        {
            get { return KEY_VALUE; }
        }
        public string Value
        {
            get;
            set;
        }
    }
}
