using DataAccess.Abstract;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concreate
{
    public class InMemoryCarDal:ICarDal
    {
        List<Car> _car;

        public InMemoryCarDal()
        {
            _car = new List<Car>
            {
                new Car{Id=1,BrandId=1,ColorId=1,DailyPrice=230,ModelYear=2004,Descripton="Toyota" },
                new Car{Id=2,BrandId=2,ColorId=1,DailyPrice=500,ModelYear=2020,Descripton="Hyundai" },
                new Car{Id=3,BrandId=3,ColorId=1,DailyPrice=100,ModelYear=2000,Descripton="Fiat" },
                new Car{Id=4,BrandId=4,ColorId=1,DailyPrice=400,ModelYear=2009,Descripton="Nissan" }
            };
        }

        public void Add(Car car)
        {
            _car.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _car.SingleOrDefault(c => c.Id == car.Id);
            _car.Remove(car);
            Console.WriteLine(car.Id + "Numarasına sahip araba silindi.");
        }

        public List<Car> GetAll()
        {
            return _car;
        }

        public List<Car> GetById(int carId)
        {
            return _car.Where(c => c.Id == carId).ToList();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _car.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.Id = car.Id;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.Descripton = car.Descripton;
            Console.WriteLine(carToUpdate.Id + "Numaralı araç güncellendi");

        }
    }
}

