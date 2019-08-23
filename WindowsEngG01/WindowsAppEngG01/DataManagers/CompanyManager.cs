using System;
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
                    UserId = "1",
                    IsSpotlighted = true,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "HOGENT"
                },
                new Company
                {
                    UserId = "2",
                    IsSpotlighted = true,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "UGent"
                },
                new Company
                {
                    UserId = "3",
                    IsSpotlighted = false,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "Jos's Delhi"
                },
                new Company
                {
                    UserId = "1",
                    IsSpotlighted = false,
                    Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg"),
                    Name = "Heimdal"
                }
            };

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

        internal static Promotion FindPromotion(string companyId, string promotionId, int identifier)
        {
            if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.PROMOTION))
            {
                return _companies.Find(c => c.Id.Equals(companyId)).Promotions.Find(p => p.Id.Equals(promotionId));
            } else if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.EVENT))
            {
                return _companies.Find(c => c.Id.Equals(companyId)).Events.Find(p => p.Id.Equals(promotionId));
            } else if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.DISCOUNTCODE))
            {
                return _companies.Find(c => c.Id.Equals(companyId)).DiscountCoupons.Find(p => p.Id.Equals(promotionId));
            }
            return null;
        }

        public static void CheckForExpiredPromotions(String companyId)
        {
            _companies.Find(c => c.Id.Equals(companyId)).Promotions.RemoveAll(p => DateTimeOffset.Compare(p.EndDate, DateTimeOffset.Now) < 0);
            _companies.Find(c => c.Id.Equals(companyId)).Events.RemoveAll(p => DateTimeOffset.Compare(p.EndDate, DateTimeOffset.Now) < 0);
            _companies.Find(c => c.Id.Equals(companyId)).DiscountCoupons.RemoveAll(p => DateTimeOffset.Compare(p.EndDate, DateTimeOffset.Now) < 0);
        }

        internal static void DeleteCompany(string Companyid)
        {
            _companies.RemoveAll(c => c.Id.Equals(Companyid));
        }

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

        public static bool CompanyExists(String id)
        {
            return _companies.Exists(c => c.Id.Equals(id));
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

        internal static void AddEvent(string id, Event promotion)
        {
            promotion.CompanyId = id;
            _companies.Find(c => c.Id.Equals(id)).Events.Add(promotion);

            foreach (var user in new UserManager().GetUsers())
            {
                if (UserManager.IsUserSubscribed(user.Id, promotion.CompanyId))
                {
                    NotificationManager.CreateNotification(user.Id, promotion.CompanyId, promotion.Id, (int)Notification.AllowedNotificationTypes.CREATED);
                }
            }
        }

        internal static void AddDiscountCoupon(string id, DiscountCoupon promotion)
        {
            promotion.CompanyId = id;
            _companies.Find(c => c.Id.Equals(id)).DiscountCoupons.Add(promotion);

            foreach (var user in new UserManager().GetUsers())
            {
                if (UserManager.IsUserSubscribed(user.Id, promotion.CompanyId))
                {
                    NotificationManager.CreateNotification(user.Id, promotion.CompanyId, promotion.Id, (int)Notification.AllowedNotificationTypes.CREATED);
                }
            }
        }

        #region CRUD Promotions
        internal static void AddPromotion(string id, Promotion promotion)
        {
            promotion.CompanyId = id;
            _companies.Find(c => c.Id.Equals(id)).Promotions.Add(promotion);

            foreach (var user in new UserManager().GetUsers())
            {
                if (UserManager.IsUserSubscribed(user.Id, promotion.CompanyId))
                {
                    NotificationManager.CreateNotification(user.Id, promotion.CompanyId, promotion.Id, (int)Notification.AllowedNotificationTypes.CREATED);
                }
            }
        }

        internal static void UpdatePromotion(string companyId, Promotion promotion, int identifier)
        {
            if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.DISCOUNTCODE))
            {
                _companies.Find(c => c.Id.Equals(companyId)).DiscountCoupons.Replace(c => c.Id.Equals(promotion.Id), (DiscountCoupon)promotion);
            }
            else if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.EVENT))
            {
                _companies.Find(c => c.Id.Equals(companyId)).Events.Replace(c => c.Id.Equals(promotion.Id), (Event)promotion);
            }
            else
            {
                _companies.Find(c => c.Id.Equals(companyId)).Promotions.Replace(c => c.Id.Equals(promotion.Id), promotion);
            }

            foreach (var user in new UserManager().GetUsers())
            {
                if (UserManager.IsUserSubscribed(user.Id, promotion.CompanyId))
                {
                    NotificationManager.CreateNotification(user.Id, promotion.CompanyId, promotion.Id, (int)Notification.AllowedNotificationTypes.UPDATED);
                }
            }
        }

        internal static void DeletePromotion(string companyId, Promotion promotion, int identifier)
        {
            // TODO CREATE NOTIFICATIONS
            if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.DISCOUNTCODE))
            {
                _companies.Find(c => c.Id.Equals(companyId)).DiscountCoupons.RemoveAll(dc => dc.Id.Equals(promotion.Id));
            }
            else if (identifier.Equals((int)AddPromotionPassThroughElement.IDENTIFIERS.EVENT))
            {
                _companies.Find(c => c.Id.Equals(companyId)).Events.RemoveAll(dc => dc.Id.Equals(promotion.Id));
            }
            else
            {
                _companies.Find(c => c.Id.Equals(companyId)).Promotions.RemoveAll(dc => dc.Id.Equals(promotion.Id));
            }

            foreach (var user in new UserManager().GetUsers())
            {
                if (UserManager.IsUserSubscribed(user.Id, promotion.CompanyId))
                {
                    NotificationManager.CreateNotification(user.Id, promotion.CompanyId, promotion.Id, (int)Notification.AllowedNotificationTypes.DELETED);
                }
            }
        }

        #endregion
    }
}
