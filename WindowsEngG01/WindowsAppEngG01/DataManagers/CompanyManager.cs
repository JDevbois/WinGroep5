using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
                    UserId = "1",
                    IsSpotlighted = true,
                    Logo = new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/d/db/Nieuw_logo_Hogent_2018.jpg/266px-Nieuw_logo_Hogent_2018.jpg"),
                    Name = "HOGENT",
                    Street = "Valentin Vaerwyckweg",
                    Number = "1",
                    City = "Gent",
                    PostalCode = "9000",
                    Type = "School",
                    Website = "http://www.hogent.be"
                },
                new Company
                {
                    UserId = "2",
                    IsSpotlighted = true,
                    Logo = new Uri("https://iomics.ugent.be/secrify/images/ugent-logo.png"),
                    Name = "UGent",
                    Street = "St Pieters Nieuwstraat",
                    Number = "33",
                    City = "Gent",
                    PostalCode = "9000",
                    Type = "School",
                    Website = "http://www.ugent.be"
                },
                new Company
                {
                    UserId = "3",
                    IsSpotlighted = false,
                    Logo = new Uri("http://www.flanderstoday.eu/sites/default/files/webimages/ft360-biz_nightshop.jpg"),
                    Name = "Jos's Delhi",
                    Street = "Valentin Vaerwyckweg",
                    Number = "2",
                    City = "Gent",
                    PostalCode = "9000",
                    Type = "Night Shop"
                },
                new Company
                {
                    UserId = "1",
                    IsSpotlighted = false,
                    Logo = new Uri("https://heimdal.xcdn.eu/wp-content/uploads/2018/03/HeimdalH-150x150.png"),
                    Name = "Heimdal",
                    Street = "Achilles Musschestraat",
                    Number = "69",
                    City = "Gent",
                    PostalCode = "9000",
                    Type = "Other",
                    Website = "http://wwww.heimdal.be"
                }
            };

        private static String GETString = "http://localhost:54089/api/Companies";
        private static String POSTString = "http://localhost:54089/api/Companies";

        #region CRUD Company

        internal async Task AddCompanyAsync(Company company)
        {
            if (UseBackend.Backend)
            {
                var companyJson = JsonConvert.SerializeObject(company);

                HttpClient client = new HttpClient();
                var res = await client.PostAsync(POSTString, new StringContent(companyJson, System.Text.Encoding.UTF8, "application/json")).ConfigureAwait(false);
            }
            else
            {
                _companies.Add(company);
            }
        }

        public List<Company> GetCompanies()
        {
            if (UseBackend.Backend)
            {
                HttpClient client = new HttpClient();
                var json = client.GetStringAsync(new Uri(GETString)).Result;
                var lst = JsonConvert.DeserializeObject<List<Company>>(json);
                return lst;
            }

            return _companies;
        }

        public List<Company> GetSpotlightCompanies()
        {
            return GetCompanies().Where(c => c.IsSpotlighted).ToList();
        }

        public Company FindCompanyById(String id)
        {
            if (UseBackend.Backend)
            {
                HttpClient client = new HttpClient();
                var json = client.GetStringAsync(new Uri(String.Format("{0}/{1}", GETString, id))).Result;
                var res = JsonConvert.DeserializeObject<Company>(json);
                return res;
            }
            return GetCompanies().First(c => c.Id.Equals(id));
        }

        internal async Task UpdateCompanyAsync(Company company)
        {
            if (UseBackend.Backend)
            {
                var companyJson = JsonConvert.SerializeObject(company);

                HttpClient client = new HttpClient();
                var res = await client.PutAsync(String.Format("{0}/{1}", POSTString, company.Id), new StringContent(companyJson, System.Text.Encoding.UTF8, "application/json"));

            } else
            {
                if (new CompanyManager().GetCompanies().FindIndex(c => c.Id == company.Id) >= 0)
                {
                    _companies.Replace(c => c.Id == company.Id, company);
                }
                else
                {
                    _companies.Add(company);
                }
            }
        }

        internal static async Task DeleteCompanyAsync(string Companyid)
        {
            if (UseBackend.Backend)
            {
                HttpClient client = new HttpClient();
                var res = await client.DeleteAsync(new Uri(String.Format("{0}/{1}", GETString, Companyid)));

            }
            else
            {
                _companies.RemoveAll(c => c.Id.Equals(Companyid));
            }
        }
        #endregion

        #region CRUD Promotions
        internal static void AddPromotion(string id, Promotion promotion)
        {
            promotion.CompanyId = id;
            new CompanyManager().GetCompanies().Find(c => c.Id.Equals(id)).Promotions.Add(promotion);

            foreach (var user in new UserManager().GetUsers())
            {
                if (UserManager.IsUserSubscribed(user.Id, promotion.CompanyId))
                {
                    NotificationManager.CreateNotificationAsync(user.Id, promotion.CompanyId, promotion.Id, (int)Notification.AllowedNotificationTypes.CREATED);
                }
            }
        }

        internal static Promotion FindPromotion(string companyId, string promotionId, int identifier)
        {
            if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.PROMOTION))
            {
                return new CompanyManager().GetCompanies().Find(c => c.Id.Equals(companyId)).Promotions.Find(p => p.Id.Equals(promotionId));
            }
            else if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.EVENT))
            {
                return new CompanyManager().GetCompanies().Find(c => c.Id.Equals(companyId)).Events.Find(p => p.Id.Equals(promotionId));
            }
            else if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.DISCOUNTCODE))
            {
                return new CompanyManager().GetCompanies().Find(c => c.Id.Equals(companyId)).DiscountCoupons.Find(p => p.Id.Equals(promotionId));
            }
            return null;
        }



        internal static void AddEvent(string id, Event promotion)
        {
            promotion.CompanyId = id;
            new CompanyManager().GetCompanies().Find(c => c.Id.Equals(id)).Events.Add(promotion);

            foreach (var user in new UserManager().GetUsers())
            {
                if (UserManager.IsUserSubscribed(user.Id, promotion.CompanyId))
                {
                    NotificationManager.CreateNotificationAsync(user.Id, promotion.CompanyId, promotion.Id, (int)Notification.AllowedNotificationTypes.CREATED);
                }
            }
        }

        internal static void AddDiscountCoupon(string id, DiscountCoupon promotion)
        {
            promotion.CompanyId = id;
            new CompanyManager().GetCompanies().Find(c => c.Id.Equals(id)).DiscountCoupons.Add(promotion);

            foreach (var user in new UserManager().GetUsers())
            {
                if (UserManager.IsUserSubscribed(user.Id, promotion.CompanyId))
                {
                    NotificationManager.CreateNotificationAsync(user.Id, promotion.CompanyId, promotion.Id, (int)Notification.AllowedNotificationTypes.CREATED);
                }
            }
        }


        internal static void UpdatePromotion(string companyId, Promotion promotion, int identifier)
        {
            if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.DISCOUNTCODE))
            {
                new CompanyManager().GetCompanies().Find(c => c.Id.Equals(companyId)).DiscountCoupons.Replace(c => c.Id.Equals(promotion.Id), (DiscountCoupon)promotion);
            }
            else if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.EVENT))
            {
                new CompanyManager().GetCompanies().Find(c => c.Id.Equals(companyId)).Events.Replace(c => c.Id.Equals(promotion.Id), (Event)promotion);
            }
            else
            {
                new CompanyManager().GetCompanies().Find(c => c.Id.Equals(companyId)).Promotions.Replace(c => c.Id.Equals(promotion.Id), promotion);
            }

            foreach (var user in new UserManager().GetUsers())
            {
                if (UserManager.IsUserSubscribed(user.Id, promotion.CompanyId))
                {
                    NotificationManager.CreateNotificationAsync(user.Id, promotion.CompanyId, promotion.Id, (int)Notification.AllowedNotificationTypes.UPDATED);
                }
            }
        }

        internal static void DeletePromotion(string companyId, Promotion promotion, int identifier)
        {
            if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.DISCOUNTCODE))
            {
                new CompanyManager().GetCompanies().Find(c => c.Id.Equals(companyId)).DiscountCoupons.RemoveAll(dc => dc.Id.Equals(promotion.Id));
            }
            else if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.EVENT))
            {
                new CompanyManager().GetCompanies().Find(c => c.Id.Equals(companyId)).Events.RemoveAll(dc => dc.Id.Equals(promotion.Id));
            }
            else
            {
                new CompanyManager().GetCompanies().Find(c => c.Id.Equals(companyId)).Promotions.RemoveAll(dc => dc.Id.Equals(promotion.Id));
            }

            foreach (var user in new UserManager().GetUsers())
            {
                if (UserManager.IsUserSubscribed(user.Id, promotion.CompanyId))
                {
                    NotificationManager.CreateNotificationAsync(user.Id, promotion.CompanyId, promotion.Id, (int)Notification.AllowedNotificationTypes.DELETED);
                }
            }
        }

        #endregion

        public static bool CompanyExists(String id)
        {
            return new CompanyManager().GetCompanies().Exists(c => c.Id.Equals(id));
        }

        public List<Company> Search(String nameFilter, String typeFilter, bool hasPromotionsFilter)
        {
            var result = new CompanyManager().GetCompanies();
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
                result = result.Where(c => c.Promotions.Count > 0 || c.Events.Count > 0 || c.DiscountCoupons.Count > 0).ToList();
            }
            return result;
        }

        internal static string ReturnPromotionType(string companyid, string promotionid)
        {
            var company = new CompanyManager().FindCompanyById(companyid);
            foreach (var item in company.Promotions)
            {
                if (promotionid.Equals(item.Id))
                {
                    return "Promotion";
                }
            }
            foreach (var item in company.Events)
            {
                if (promotionid.Equals(item.Id))
                {
                    return "Event";
                }
            }
            foreach (var item in company.DiscountCoupons)
            {
                if (promotionid.Equals(item.Id))
                {
                    return "Discount Coupon";
                }
            }
            return null;
        }

        public static void CheckForExpiredPromotions(String companyId)
        {
            new CompanyManager().GetCompanies().Find(c => c.Id.Equals(companyId)).Promotions.RemoveAll(p => DateTimeOffset.Compare(p.EndDate, DateTimeOffset.Now) < 0);
            new CompanyManager().GetCompanies().Find(c => c.Id.Equals(companyId)).Events.RemoveAll(p => DateTimeOffset.Compare(p.EndDate, DateTimeOffset.Now) < 0);
            new CompanyManager().GetCompanies().Find(c => c.Id.Equals(companyId)).DiscountCoupons.RemoveAll(p => DateTimeOffset.Compare(p.EndDate, DateTimeOffset.Now) < 0);
        }

    }
}
