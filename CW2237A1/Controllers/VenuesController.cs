using CW2237A1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CW2237A1.Controllers
{
    public class VenuesController : Controller
    {
        private Manager m = new Manager();
        public ActionResult Index()
        {
            var v = m.VenueGetAll();
            return View(v);
        }

        public ActionResult Details(int? id)
        {
            var v = m.VenueGetById(id.GetValueOrDefault());

            if (v == null)
            {
                return HttpNotFound();
            }

            return View(v);
        }

        // GET: Venues/Create
        public ActionResult Create()
        {
            return View(new VenueAddViewModel());
        }

        // POST: Venues/Create
        [HttpPost]
        public ActionResult Create(VenueAddViewModel venue) { 
            if (!ModelState.IsValid)
            {
                return View(venue);
            }

            try
            {
                // Process the input
                var addedVenue = m.VenueAdd(venue);

                if (addedVenue == null)
                {
                    return View(venue);
                }
                else
                {
                    return RedirectToAction("Details", new { id = addedVenue.VenueId });
                }
            }
            catch
            {
                return View(venue);
            }
        }

        // GET: Venues/Edit
        public ActionResult Edit(int? id)
        {
            var v = m.VenueGetById(id.GetValueOrDefault());

            if (v == null)
            {
                return HttpNotFound();
            }

            var formVenue = m.mapper.Map<VenueBaseViewModel, VenueEditFormViewModel>(v);

            return View(formVenue);
        }

        // POST: Venues/Edit
        [HttpPost]
        public ActionResult Edit(int? id, VenueEditViewModel venue)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = venue.VenueId });
            }

            // Data Tampering
            if (id.GetValueOrDefault() != venue.VenueId)
            {
                return RedirectToAction("Index");
            }

            var edited = m.VenueEdit(venue);

            if (edited == null)
            {
                // Problem updating
                return RedirectToAction("Edit", new { id = venue.VenueId });
            }

            return RedirectToAction("Details", new { id = venue.VenueId });
        }

        // GET: Venues/Delete
        public ActionResult Delete(int? id)
        {
            var v = m.VenueGetById(id.GetValueOrDefault());

            if (v == null) { 
                return RedirectToAction("Index"); 
            }

            return View(v);
        }

        // POST: Venues/Delete
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            m.VenueDelete(id.GetValueOrDefault());

            return RedirectToAction("Index");
        }
    }
}