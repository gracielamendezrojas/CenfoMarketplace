using DataAcess.Crud;
using DTO_POJOS;
using System;
using System.Collections.Generic;


namespace AppLogic.Managers
{
    public class RoleXUserManager : BaseManager
    {
        private RoleXUserCrudFactory RoleXUserCrudFactory;

        public RoleXUserManager()
        {
            RoleXUserCrudFactory = new RoleXUserCrudFactory();
        }

        public void CreateRoleXUser(RoleXUser RoleXUser)
        {
            RoleXUserCrudFactory.Create(RoleXUser);
        }

        public List<RoleXUser> RetrieveAllRoleXUser()
        {
            List<RoleXUser> result = RoleXUserCrudFactory.RetrieveAll<RoleXUser>();
            return result;
        }

        public RoleXUser RetrieveRoleXUser(BaseEntity entity)
        {
            RoleXUser result = RoleXUserCrudFactory.Retrieve<RoleXUser>(entity);
            return result;
        }

        public void DeleteRoleXUser(BaseEntity entity)
        {
            RoleXUserCrudFactory.Delete(entity);
        }

        public void UpdateRoleXUser(BaseEntity entity)
        {
            RoleXUserCrudFactory.Update(entity);
        }
    }
}
