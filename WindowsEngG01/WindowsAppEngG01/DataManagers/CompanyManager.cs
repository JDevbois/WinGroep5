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
                    UserId = 1,
                    IsSpotlighted = true,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "HOGENT",
                    Promotions = new List<Promotion>
                    {
                        new Promotion()
                    }
                },
                new Company
                {
                    UserId = 2,
                    IsSpotlighted = true,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "UGent"
                },
                new Company
                {
                    UserId = 3,
                    IsSpotlighted = false,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "Jos's Delhi"
                },
                new Company
                {
                    UserId = 1,
                    IsSpotlighted = false,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "Heimdal"
                }
            };

        public List<Company> Search(String nameFilter, String typeFilter, bool hasPromotionsFilter)
        {
            var result = _companies;
            if (!string.IsNullOrEmpty(nameFilter))
            {
                result = result.Where(c => c.Name.Contains(nameFilter)).ToList();
            }
            if (!string.IsNullOrEmpty(typeFilter) && !typeFilter.Equals("Don't filter on type"))
            {
                result = result.Where(c => c.Type == typeFilter).ToList();
            }
            if (hasPromotionsFilter)
            {
                //TODO filter by date (promotions only after current date)
                result = result.Where(c => c.Promotions.Count > 0 || c.Events.Count > 0).ToList();
            }
            return result;
        }

        //TODO filter companies on bool spotlighted
        public List<Company> GetSpotlightCompanies()
        {
            return GetCompanies().Where(c => c.IsSpotlighted).ToList();
        }

        public List<Company> GetCompanies()
        {
            return _companies;
        }

        internal void AddCompany(Company company)
        {
            _companies.Add(company);
        }

        public Company FindCompanyById(String id)
        {
            return GetCompanies().First(c => c.Id.Equals(id));
        }

        internal void UpdateCompany(Company company)
        {
            if(_companies.FindIndex(c => c.Id == company.Id) >= 0)
            {
                _companies.Replace(c => c.Id == company.Id, company);
            } else
            {
                _companies.Add(company);
            }
        }
    }
}
