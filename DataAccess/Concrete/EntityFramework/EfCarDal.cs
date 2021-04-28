using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal:EfEntityRepositoryBase<Car,CarRentalContext> , ICarDal
    {
        public List<CarDetail> GetCarDetail()
        {
            using (var context = new CarRentalContext())
            {
                 var result = from c in context.Cars
                    join b in context.Brands on c.BrandId equals b.Id 
                    join clr in context.Colors on c.ColorId equals clr.Id
                    select new CarDetail
                    {
                        CarName = c.CarName,
                        BrandName = b.BrandName,
                        ColorName = clr.ColorName,
                        DailyPrice = c.DailyPrice
                    };

                return result.ToList();
            }
        }
    
    }
}
