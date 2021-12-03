using Business.Abstract;
using Business.Concreate;
using DataAccess.Abstract;
using DataAccess.Concreate;
using DataAccess.Concreate.EntityFramework;
using Entities.Concreate;
using System;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            //CarManagerTest();
            UserManagerTest();
        }

        private static void CarManagerTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            Car car = new Car();
            car.CarId =4; car.DailyPrice = 20; car.Descripton = "qhasqai"; car.BrandId = 2; car.CarName = "nissan"; car.ColorId = 122; car.ModelYear = 2005;


            carManager.Add(car);

            foreach (var item in carManager.GetAll().Data)
            {
                Console.WriteLine(item.CarName);
            }

        }
        private static void UserManagerTest()
        {
            UserManager userManager = new UserManager(new EfUsersDal());
            Users user1=new Users{ Id=2,FirstName="Berkay",LastName="Hellagun",Email="bhellagun@gmail.com",Password="selam"};
            
            userManager.Update(user1);
            foreach (var item in userManager.GetAll().Data)
            {
                Console.WriteLine(item.FirstName);
            }
        }
    }
}
