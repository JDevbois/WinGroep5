using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib;
using WindowsAppEngG01.ViewModels;

namespace WindowsAppEngG01.DataManagers
{
    public class CompanyManager
    {
        //TODO filter companies on bool spotlighted
        public List<Company> GetSpotlightCompanies()
        {
            return GetCompanies();
        }

        public List<Company> GetCompanies()
        {
            return new List<Company>
            {
                new Company
                {
                    Id = 1,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "HOGENT"
                },
                new Company
                {
                    Id = 2,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "HOGENT"
                },
                new Company
                {
                    Id = 3,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "HOGENT"
                },
                new Company
                {
                    Id = 4,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "Heimdal"
                }
            };
        }

        public Company FindCompanyById(int id)
        {
            return GetCompanies().First(c => c.Id == id);
        }
    }
}
