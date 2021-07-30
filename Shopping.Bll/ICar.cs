using Shopping.Model;
using System.Collections.Generic;

namespace Shopping.Bll
{
    public interface ICar
    {
        List<CarModel> AddCar(CarModel carModel);
        List<CarModel> BulkDelCar(int[] idList);
        bool ClearCar();
        List<CarModel> DelCar(int id);
        List<CarModel> GetCar();
        List<CarModel> UpdateCar(CarModel carModel);
    }
}