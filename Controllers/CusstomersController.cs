using Syncfusion.EJ2.Base;
using System.Collections;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using CustomerChartered.Models;
namespace CustomerChartered.Controllers
{
  public class CusstomersController : Controller
  {
	    private EserviceEntities db = new EserviceEntities();
        public ActionResult Index()
        {

            return View();
        }
      // [HttpGet]
        public ActionResult UrlDatasource(DataManagerRequest dm)
        {

            IEnumerable DataSource = db.Customers.ToList();
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentName");

            DataOperations operation = new DataOperations();   
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
               DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
			if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
			    DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<Customer>().Count();
            if (dm.Skip != 0)//Paging
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
           
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }, JsonRequestBehavior.AllowGet) : Json(DataSource);
        }
        //  [HttpPost]
        public ActionResult Insert([Bind(Include = "CustomerId,ParentId,ChildId,DepartmentId,OrderDate,ServiceId,ServiceDetailId,TimeframeId,TinNumber,StartTime,EndTime,MinuteTook,Evaluate")] Customer value)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(value);
                db.SaveChanges();
            }

            ViewBag.ChildId = new SelectList(db.Children, "ChildId", "ChildName");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.ParentId = new SelectList(db.Parents, "ParentId", "ParentName");
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName");
            ViewBag.ServiceDetailId = new SelectList(db.ServiceDetails, "ServiceDetailId", "ServiceDetailName");
            ViewBag.TimeframeId = new SelectList(db.Timeframes, "TimeframeId", "TimeframeName");
            
            return Json(value);
        }
        public ActionResult Update(Customer value)
        {
            var ord = value;
            Customer val = db.Customers.Where(or => or.CustomerId == ord.CustomerId).FirstOrDefault();

            if (val != null)
            {
               val.CustomerId=ord.CustomerId;
               val.ParentId=ord.ParentId;
               val.ChildId=ord.ChildId;
               val.DepartmentId=ord.DepartmentId;
               val.OrderDate=ord.OrderDate;
               val.ServiceId=ord.ServiceId;
               val.ServiceDetailId=ord.ServiceDetailId;
               val.TimeframeId=ord.TimeframeId;
               val.TinNumber=ord.TinNumber;
               val.StartTime=ord.StartTime;
               val.EndTime=ord.EndTime;
               val.MinuteTook=ord.MinuteTook;
               val.Evaluate=ord.Evaluate;
            }
            return Json(val);
        }
        public ActionResult Delete(CRUDModel<Customer> value)
        {
            Customer removeValue = db.Customers.Where(c => c.CustomerId == (int)value.Key).FirstOrDefault();
            db.Customers.Remove(removeValue);

            db.SaveChanges();
            return Json(removeValue);
        }
   }
}