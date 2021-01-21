using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fort.Models;

namespace Fort.Models
{
    public interface ICityRepository
    {
        Task<ServiceResponse<int>> AddCountry(Country cty);
        List<CityViewModel> GetAll(int LoginUserId);
        CityViewModel GetCity(int LoginUserId,int? CityId);
      
        Task<ServiceResponse<int>> AddCity(int LoginUserId,City city);
        Task<ServiceResponse<int>> DeleteCity(int? CityId);
        Task<ServiceResponse <int>> UpdateCity(int LoginUserId,City city);

    }
}
