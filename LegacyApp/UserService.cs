using System;

namespace LegacyApp
{
    public class UserService
    {
        private IClientRepository _clientRepository;
        private ICreditLimitService _creditService;


        public UserService()
        {
            _clientRepository = new ClientRepository();
            _creditService = new UserCreditService();

        }

        public UserService(IClientRepository clientRepository,ICreditLimitService creditService)
        {
            _clientRepository = clientRepository;
            _creditService = creditService;
        }

       
        
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            
           
            VerifyNames(firstName, lastName);
            VerifyEmail(email);
            VerifyAge(dateOfBirth);



            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            SetCredditLimit(client,user);
            CheckCraditLimit(user);



            UserDataAccess.AddUser(user);
            return true;
        }


        public bool VerifyNames(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }
            else return true;
        }
        public bool VerifyEmail(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }
            else return true;

        }

        public bool VerifyAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }
            else return true;
        }

        public void SetCredditLimit(Client client,User user)
        {
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                
                
                int creditLimit = _creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
                
            }
            else
            {
                user.HasCreditLimit = true;
                int creditLimit = _creditService.GetCreditLimit(user.LastName, user.DateOfBirth); 
                user.CreditLimit = creditLimit;
                
            }
            
        }

        public bool CheckCraditLimit(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }
            else return true;
        }


    }
   


}
