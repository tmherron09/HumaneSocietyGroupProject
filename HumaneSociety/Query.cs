using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        static HumaneSocietyDataContext db;

        static Query()
        {
            db = new HumaneSocietyDataContext();
        }

        internal static List<USState> GetStates()
        {
            List<USState> allStates = db.USStates.ToList();

            return allStates;
        }

        internal static Client GetClient(string userName, string password)
        {
            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            Client newClient = new Client();

            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Address addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.USStateId == stateId).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (addressFromDb == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = streetAddress;
                newAddress.City = null;
                newAddress.USStateId = stateId;
                newAddress.Zipcode = zipCode;

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                addressFromDb = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.InsertOnSubmit(newClient);

            db.SubmitChanges();
        }

        internal static void UpdateClient(Client clientWithUpdates)
        {
            // find corresponding Client from Db
            Client clientFromDb = null;

            try
            {
                clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("No clients have a ClientId that matches the Client passed in.");
                Console.WriteLine("No update have been made.");
                return;
            }

            // update clientFromDb information with the values on clientWithUpdates (aside from address)
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;

            // get address object from clientWithUpdates
            Address clientAddress = clientWithUpdates.Address;

            // look for existing Address in Db (null will be returned if the address isn't already in the Db
            Address updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.USStateId == clientAddress.USStateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (updatedAddress == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.City = null;
                newAddress.USStateId = clientAddress.USStateId;
                newAddress.Zipcode = clientAddress.Zipcode;

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;

            // submit changes
            db.SubmitChanges();
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();

            return employeeWithUserName != null;
        }


        //// TODO Items: ////

        // TODO: Allow any of the CRUD operations to occur here
        internal static void RunEmployeeQueries(Employee employee, string crudOperation)
        {
            // Switch statement for CRUD

            throw new NotImplementedException();
        }
        internal static void AddEmployee(Employee employee)
        {

            db.Employees.InsertOnSubmit(employee);
            db.SubmitChanges();
        }

        internal static Employee GetEmployeeByID(int? id)
        {

            Employee employee = db.Employees.Where(e => e.EmployeeNumber == id).SingleOrDefault();
            return employee;
        }

        internal static void UpdateEmployee(Employee employeeToUpdate)
        {
            Employee employeeInDb = null;

            // Throws exception if no employee is found.
            employeeInDb = db.Employees.Where(e => e.EmployeeNumber == employeeToUpdate.EmployeeNumber).Single();

            employeeInDb.FirstName = employeeToUpdate.FirstName != "" ? employeeToUpdate.FirstName : employeeInDb.FirstName;

            employeeInDb.LastName = employeeToUpdate.LastName != "" ? employeeToUpdate.LastName : employeeInDb.LastName;

            employeeInDb.Email = employeeToUpdate.Email != "" ? employeeToUpdate.Email : employeeInDb.Email;

            db.SubmitChanges();

        }

        internal static void RemoveEmployee(Employee employee)
        {
            db.Employees.DeleteOnSubmit(employee);
            Employee employeeToRemove = GetEmployeeByID(employee.EmployeeNumber);
            db.Employees.DeleteOnSubmit(employeeToRemove);
            db.SubmitChanges();
        }


        // TODO: Animal CRUD Operations
        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        internal static Animal GetAnimalByID(int id)
        {
            Animal animal = db.Animals.Where(a => a.AnimalId == id).SingleOrDefault();
            return animal;
        }

        internal static void UpdateAnimal(int animalId, Dictionary<int, string> updates)
        {
            Animal animalInDb = null;
            animalInDb = db.Animals.Where(a => a.AnimalId == animalId).SingleOrDefault();
            foreach (var pair in updates)
            {
                switch (pair.Key)
                {
                    case 1:
                        animalInDb.CategoryId = GetCategoryId(pair.Value);
                        break;
                    case 2:
                        animalInDb.Name = pair.Value;
                        break;
                    case 3:
                        animalInDb.Age = pair.Key;
                        break;
                    case 4:
                        animalInDb.Demeanor = pair.Value;
                        break;
                    case 5:
                        animalInDb.KidFriendly = pair.Value == "True" ? true : false;
                        break;
                    case 6:
                        animalInDb.PetFriendly = pair.Value == "True" ? true : false;
                        break;
                    case 7:
                        animalInDb.Weight = pair.Key;
                        break;
                    case 8:
                        Console.WriteLine("Can't update.");
                        break;
                    default:
                        Console.WriteLine("Input was not recognized. Please try again.");
                        break;
                }                
            }
            db.SubmitChanges();
        }

        internal static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }

        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animal> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates) // parameter(s)?
        {
            throw new NotImplementedException();
        }

        // TODO: Misc Animal Things
        internal static int? GetCategoryId(string categoryName)
        {

            //throw new NotImplementedException();

            int categoryId;
            try
            {
                categoryId = db.Categories.Where(c => c.Name.ToLower() == categoryName.ToLower()).Select(c => c.CategoryId).Single();
            }
            catch
            {
                Console.Clear();
                UserInterface.DisplayUserOptions("Category not found. Please try again or type 'exit' to continue without adding a category/breed.");
                string retryCategoryName = UserInterface.GetStringData("category/breed", "the name of the animal's");
                if (retryCategoryName.ToLower() == "exit" || retryCategoryName.ToLower() == "e")
                {
                    return null;
                }
                return GetCategoryId(retryCategoryName);
            }
            return categoryId;
        }

        internal static Room GetRoom(int animalId)
        {

            Room roomFromDb = db.Rooms.Where(r => r.AnimalId == animalId).SingleOrDefault();
            return roomFromDb;
        }
        internal static Room GetRoomEmployee(int animalId)
        {
            Room roomFromDb;
            try
            {
                roomFromDb = db.Rooms.Where(r => r.AnimalId == animalId).Single();
            }
            catch
            {
                Console.Clear();
                return AssignAnimalRoom(GetAnimalByID(animalId));
            }
            return roomFromDb;
        }

        private static Room AssignAnimalRoom(Animal animal)
        {
            Room newRoom = new Room();
            
            UserInterface.DisplayUserOptions($"Missing {animal.Name}'s room assignment.");
            int roomNumber = UserInterface.GetIntegerData("room number", $"{animal.Name}'s");
            if (db.Rooms.Select(r => r.RoomNumber).Contains(roomNumber) && db.Rooms.Where(r => r.RoomNumber == roomNumber).Select(r => r.AnimalId).SingleOrDefault() != null)
            { 
                    UserInterface.DisplayUserOptions($"This room is currently assigned. Please select another.");
                    return AssignAnimalRoom(animal);
                
            }
            else if (db.Rooms.Select(r => r.RoomNumber).Contains(roomNumber) && db.Rooms.Where(r => r.RoomNumber == roomNumber).Select(r => r.AnimalId).SingleOrDefault() == null)
            {
                newRoom = db.Rooms.Where(r => r.RoomNumber == roomNumber).Single();
                newRoom.AnimalId = animal.AnimalId;
            }
            else
            {
                newRoom.RoomNumber = roomNumber;
                newRoom.AnimalId = animal.AnimalId;
                db.Rooms.InsertOnSubmit(newRoom);
                
            }
            db.SubmitChanges();

            return newRoom;

        }

        internal static int GetDietPlanId(string dietPlanName)
        {
            throw new NotImplementedException();
        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animal animal, Client client)
        {
            throw new NotImplementedException();
        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            throw new NotImplementedException();
        }

        internal static void UpdateAdoption(bool isAdopted, Adoption adoption)
        {
            throw new NotImplementedException();
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            throw new NotImplementedException();
        }

        // TODO: Shots Stuff
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            var shotRecords = db.AnimalShots.Where(s => s.AnimalId == animal.AnimalId);
            return shotRecords;
        }

        internal static void UpdateShot(string shotName, Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}