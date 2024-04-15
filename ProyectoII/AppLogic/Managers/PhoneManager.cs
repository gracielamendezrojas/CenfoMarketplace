using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class PhoneManager
    {
        private PhoneCrudFactory phCrudFactory;

        public PhoneManager()
        {
            phCrudFactory = new PhoneCrudFactory();
        }

        public void CreatePhone(Phone phone)
        {
            phCrudFactory.Create(phone);
        }

        public List<Phone> GetPhones()
        {
            return phCrudFactory.RetrieveAll<Phone>();
        }
        

        public APIResponse GetPhoneByUser(int id)
        {
            APIResponse api = new APIResponse()
            {
                TransactionDate = DateTime.Now,
            };
            try
            {
                List<Phone> list = phCrudFactory.RetrieveAll<Phone>();

                if (list != null)
                {
                    foreach (Phone phone in list)
                    {
                        if(phone.User.Equals(id))
                        {
                            api.Data = phone;
                            break; 
                        }
                    }
                    api.Status = 200;
                    api.Message = "Successful"; 
                }
            }
            catch (Exception ex)
            {
                api.Status = 400;
                api.Message = "Error"; 
                api.Data = ex; 
            }
            return api; 
        }
        public void DeletePhone(Phone phone)
        {
            phCrudFactory.Delete(phone);
        }

        public Phone GetPhone(Phone phone)
        {
            return phCrudFactory.Retrieve<Phone>(phone);
        }
        public void EditPhone(Phone phone)
        {
            phCrudFactory.Update(phone);
        }
    }
}
