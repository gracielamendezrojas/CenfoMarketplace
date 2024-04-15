using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Managers
{
    public class OrganizationManager
    {
        private OrganizationCrudFactory orgCrudFactory;

        public OrganizationManager()
        {
            orgCrudFactory = new OrganizationCrudFactory();
        }

        public void CreateOrg(Organization org)
        {
            orgCrudFactory.Create(org);
        }

        public List<Organization> GetOrgs()
        {
            return orgCrudFactory.RetrieveAll<Organization>();
        }

        public void DeleteOrg(Organization org)
        {
            orgCrudFactory.Delete(org);
        }

        public Organization GetOrg(Organization org)
        {
            return orgCrudFactory.Retrieve<Organization>(org);
        }
        public void EditOrg(Organization org)
        {
            orgCrudFactory.Update(org);
        }
    }
}
