using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            CarManagerTest();
            //UserManagerTest();
            //RentalTest();
        }

        private static void CarManagerTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            Car car = new Car();
            car.Id =4; car.DailyPrice = 20; car.Descripton = "qashqai"; car.BrandId = 2; car.Name = "nissan"; car.ColorId = 122; car.ModelYear = 2005;


            carManager.Add(car);

        }
        private static void UserManagerTest()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            User user1=new User{ Id=1,FirstName="Berkay",LastName="Hellagun",Email="bhellagun@gmail.com"};
            
            userManager.Add(user1);
            foreach (var item in userManager.GetAll().Data)
            {
                Console.WriteLine(item.FirstName);
            }
        }
        private static void RentalTest()
        {
            RentalManager rental = new RentalManager(new EfRentalDal());
            Rental rentals = new Rental { Id = 1, CarId = 1, CustomerId = 1, ReturnDate = new DateTime(2021,12,5) };
            
            var result=rental.Add(rentals);

            if (result.Success)
            {
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            foreach (var item in rental.GetAll().Data)
            {
                Console.WriteLine("All Customer's id: "+item.CustomerId);
            }
        }
    }
}
