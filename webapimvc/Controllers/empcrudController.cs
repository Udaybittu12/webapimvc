using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapimvc.Models;

namespace webapimvc.Controllers
{
    public class empcrudController : ApiController
    {
        cmEntities cm = new cmEntities();
        public IHttpActionResult getemp()           // view details
        {
            var results = cm.naveenpregs.ToList();
            return Ok(results);
        }
        [HttpPost]          
        public IHttpActionResult empinsert(naveenpreg empinsert)      // to insert the records
        {
            cm.naveenpregs.Add(empinsert);
            cm.SaveChanges();
            return Ok();
        }
        public IHttpActionResult getempid(int id)            // get details in table of gridview
        {
            emp empdeatils = null;
            empdeatils = cm.naveenpregs.Where(x => x.empid == id).Select(x => new emp()
            {
                empid = x.empid,
                empname = x.empname,
                empemail = x.empemail,
                emplocation = x.emplocation,
                empdesignation = x.empdesignation,
            }).FirstOrDefault<emp>();
            if (empdeatils == null)
            {
                return NotFound();
            }
            return Ok(empdeatils);
        }
        public IHttpActionResult put(emp ec)      // edit details 
        {
            var updateemp = cm.naveenpregs.Where(x => x.empid == ec.empid).FirstOrDefault<naveenpreg>();
            if (updateemp != null)
            {
                updateemp.empid = ec.empid;
                updateemp.empname = ec.empname;
                updateemp.empemail = ec.empemail;
                updateemp.emplocation = ec.emplocation;
                updateemp.empdesignation = ec.empdesignation;
                cm.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        public IHttpActionResult delete(int id)      // delete the id
        {
            var empdel = cm.naveenpregs.Where(x => x.empid == id).FirstOrDefault();
            cm.Entry(empdel).State = System.Data.Entity.EntityState.Deleted;
            cm.SaveChanges();
            return Ok();
        }


        // web api controller //
    }
}
