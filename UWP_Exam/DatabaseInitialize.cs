using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Exam
{
    internal class DatabaseInitialize
    {
        public static bool CreateTable()
        {
            var conn = new SQLiteConnection("sqlitepcldemo.db");
            string sql = @"CREATE TABLE IF NOT EXISTS
                        Contacts (
                        Name VARCHAR( 140 ),
                        PhoneNumber VARCHAR( 140 ) UNIQUE
            );";

            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
            return true;
        }

        public static bool Save(Contact contact)
        {
            var conn = new SQLiteConnection("sqlitepcldemo.db");

            using (var contactStatement = conn.Prepare("INSERT INTO Contacts (Name, PhoneNumber) VALUES (?, ?)"))
            {
                contactStatement.Bind(1, contact.Name);
                contactStatement.Bind(2, contact.PhoneNumber);
                contactStatement.Step();
            }
            return true;
        }

        public static Contact GetContact(string name, string phone)
        {
            Contact contact = null;
            var conn = new SQLiteConnection("sqlitepcldemo.db");
            using (var statement = conn.Prepare("SELECT * FROM Contacts WHERE Name = ? AND PhoneNumber = ?"))
            {
                statement.Bind(1, name);
                statement.Bind(2, phone);
                if (SQLiteResult.DONE == statement.Step())
                {
                    contact = new Contact
                    {
                        Name = name,
                        PhoneNumber = phone
                    };
                }
            }
            return contact;
        }
    }
}