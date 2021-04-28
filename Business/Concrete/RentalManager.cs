using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class RentalManager:IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(x => x.Id == rentalId));
        }

        public IResult Add(Rental rental)
        {
            if (IsCarAvailable(rental.CarId))
            {
                _rentalDal.Add(rental);
                return new SuccessResult("Kiralama ekleme başarılı");
            }

            return new ErrorResult("Araba henüz teslim edilmediği için kiralanamaz ! ");
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult("Kiralama silme başarılı");
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult("Kiralama güncelleme başarılı");
        }

        public bool IsCarAvailable(int carId)
        {
            var result = _rentalDal.Get(x => x.CarId == carId && x.ReturnDate == null);
            if (result==null)
            {
                return false;
            }

            return true;
        }
    }
}
