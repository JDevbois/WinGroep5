﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib;
using WindowsAppEngG01.Utils;
using WindowsAppEngG01.ViewModels;

namespace WindowsAppEngG01.DataManagers
{
    public class CompanyManager
    {

        private static List<Company> _companies = new List<Company>
            {
                new Company
                {
                    Id = 1,
                    UserId = 1,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "HOGENT"
                },
                new Company
                {
                    Id = 2,
                    UserId = 2,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "UGent"
                },
                new Company
                {
                    Id = 3,
                    UserId = 3,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "Jos's Delhi"
                },
                new Company
                {
                    Id = 4,
                    UserId = 1,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "Heimdal"
                }
            };

        //TODO filter companies on bool spotlighted
        public List<Company> GetSpotlightCompanies()
        {
            return GetCompanies();
        }

        public List<Company> GetCompanies()
        {
            return _companies;
        }

        internal void AddCompany(Company company)
        {
            _companies.Add(company);
        }

        public Company FindCompanyById(int id)
        {
            return GetCompanies().First(c => c.Id == id);
        }

        internal void UpdateCompany(Company company)
        {
            _companies.Replace(c => c.Id == company.Id, company);
        }
    }
}
