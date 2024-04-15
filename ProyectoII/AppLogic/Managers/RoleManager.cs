using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;


namespace AppLogic.Managers
{
    public class RoleManager : BaseManager
    {
        private RoleCrudFactory RoleCrudFactory;

        public RoleManager()
        {
            RoleCrudFactory = new RoleCrudFactory();
        }

        public void CreateRole(Role Role)
        {
            RoleCrudFactory.Create(Role);
        }

        public List<Role> RetrieveAllRole()
        {
            List<Role> result = RoleCrudFactory.RetrieveAll<Role>();
            return result;
        }

        public Role RetrieveRole(BaseEntity entity)
        {
            Role result = RoleCrudFactory.Retrieve<Role>(entity);
            return result;
        }

        public void DeleteRole(BaseEntity entity)
        {
            RoleCrudFactory.Delete(entity);
        }

        public void UpdateRole(BaseEntity entity)
        {
            RoleCrudFactory.Update(entity);
        }
    }
}
