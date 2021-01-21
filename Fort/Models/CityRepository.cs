using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fort.Models
{
    public class CityRepository : ICityRepository
    {
        private FortCodeContext db;
        public CityRepository(FortCodeContext _db)
        {
            this.db = _db;
        }
        //---> Add Country
        public async Task<ServiceResponse<int>> AddCountry(Country country)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();

            if (await db.Countries.AnyAsync(x => x.Countryname.ToLower() == country.Countryname.ToLower()))
            {
                response.Success = false;
                response.Message = "Country already exist.";
                return response;
            }
            else
            {
                db.Countries.Add(country);
                await db.SaveChangesAsync();
                response.Success = true; ;
                response.Message = "Country Sucessfully Added.";
                return response;
            }
        }


        //---> Gell All Cities 
        public List<CityViewModel> GetAll(int LoginUserId)
        {
            if (db != null)
            {
                return (from u in db.Users
                        from p in db.Cities
                        from c in db.Countries
                        where p.Countryid == c.Countryid && p.CityUserId == u.UserId && u.UserId == LoginUserId
                        select new CityViewModel
                        {
                            CityId = p.CityId,
                            CityName = p.CityName,
                            Countryid = p.Countryid,
                            CountryName = c.Countryname,
                            UserId = p.CityUserId,
                            UserName = u.Name
                        }).ToList();
            }
            else
                return null;
        }

        //---> Gell Single City
        public CityViewModel GetCity(int LoginUserId, int? CityId)
        {
            if (db != null)
            {
                return (from u in db.Users
                        from c in db.Cities
                        from cc in db.Countries
                        where u.UserId == LoginUserId && u.UserId == c.CityUserId && c.Countryid == cc.Countryid && c.CityId == CityId
                        select new CityViewModel
                        {
                            CityId = c.CityId,
                            CityName = c.CityName,
                            Countryid = c.Countryid,
                            CountryName = cc.Countryname,
                            UserId = c.CityUserId,
                            UserName = u.Name
                        }).FirstOrDefault();
            }
            else
                return null;
        }

        //---> Add City
        public async Task<ServiceResponse<int>> AddCity(int LoginUserId, City city)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();

            if (await UserCityExists(LoginUserId, city.CityName, city.Countryid))
            {
                response.Success = false;
                response.Message = "Your favourite City already exist.";
                return response;
            }
            else
            {
                city.CityUserId = LoginUserId;
                db.Cities.Add(city);
                await db.SaveChangesAsync();
                response.Success = true; ;
                response.Message = "Your favourite City Sucessfully Added.";
                return response;
            }
        }

        //---> Update City
        public async Task<ServiceResponse<int>> UpdateCity(int LoginUserId, City city)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            //var cty =  db.Cities.FirstOrDefault(x => x.CityId == city.CityId && x.CityUserId == LoginUserId);
            //if (cty == null)
            //{
            //    response.Success = false;
            //    response.Message = "City Not Updated or Not Found.";
            //    return response;
            //}
            //else
            //{
            //    db.Cities.UpdateRange.Update(city);
            //    await db.SaveChangesAsync();
            //    response.Success = true; ;
            //    response.Message = "City Updated Sucessfully.";
            //    return response;
            //}


            var c = db.Cities.FirstOrDefault(c => c.CityId == city.CityId && c.CityUserId == LoginUserId);
            if (c == null)
            {
                response.Success = false;
                response.Message = "City Not Updated or Not Found.";
                return response;
            }
            else
            {
                c.CityName = city.CityName;
                c.Countryid = city.Countryid;
               // db.Cities.Update(city);
                await db.SaveChangesAsync();
                response.Success = true; ;
                response.Message = "City Updated Sucessfully.";
                return response;
            }


        }

        //---> Delete City
        public async Task<ServiceResponse<int>> DeleteCity(int? cid)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();

            var city = await db.Cities.FindAsync(cid);
            if (city == null)
            {
                response.Success = false;
                response.Message = "City Not Found.";
                return response;
            }
            else
            {
                db.Cities.Remove(city);
                await db.SaveChangesAsync();

                response.Success = true; ;
                response.Message = "City Deleted Sucessfully.";
                return response;
            }
        }


        //---> Checking User --City, Country exists or not
        public async Task<bool> UserCityExists(int loginUid, string cname, int? countryid)
        {
            if (await db.Cities.AnyAsync(x => x.CityUserId == loginUid && x.CityName.ToLower() == cname.ToLower() && x.Countryid == countryid))
                return true;
            else

                return false;
        }

    }
}
