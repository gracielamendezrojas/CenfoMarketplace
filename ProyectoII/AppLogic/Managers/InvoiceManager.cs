//using DataAcess.Crud;
//using DTO_POJOS;
//using System;
//using System.Collections.Generic;


//namespace AppLogic.Managers
//{
//    public class InvoiceManager : BaseManager
//    {
//        private InvoiceCrudFactory InvoiceCrudFactory;

//        public InvoiceManager()
//        {
//            InvoiceCrudFactory = new InvoiceCrudFactory();
//        }

//        public void CreateInvoice(Invoice Invoice)
//        {
//            InvoiceCrudFactory.Create(Invoice);
//        }

//        public List<Invoice> RetrieveAllInvoice()
//        {
//            List<Invoice> result = InvoiceCrudFactory.RetrieveAll<Invoice>();
//            return result;
//        }

//        public Invoice RetrieveInvoice(BaseEntity entity)
//        {
//            Invoice result = InvoiceCrudFactory.Retrieve<Invoice>(entity);
//            return result;
//        }

//        public void DeleteInvoice(BaseEntity entity)
//        {
//            InvoiceCrudFactory.Delete(entity);
//        }

//        public void UpdateInvoice(BaseEntity entity)
//        {
//            InvoiceCrudFactory.Update(entity);
//        }
//    }
//}
