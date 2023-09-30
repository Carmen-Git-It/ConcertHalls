using AutoMapper;
using CW2237A1.Data;
using CW2237A1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

// ************************************************************************************
// WEB524 Project Template V1 == 2237-17130fab-b31b-4056-ab5f-794395118bce
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace CW2237A1.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();

                // Map from Venue design model to VenueBaseViewModel
                cfg.CreateMap<Venue, VenueBaseViewModel>();

                // Map from VenueBaseViewModel to Venue design model
                cfg.CreateMap<VenueBaseViewModel, Venue>();

                // Map from VenueAddViewModel to Venue design model
                cfg.CreateMap<VenueAddViewModel, Venue>();

                // Map from VenueBaseViewModel to VenueEditFormViewModel
                cfg.CreateMap<VenueBaseViewModel, VenueEditFormViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }


        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().

        // Returns a DbSet of VenueBaseViewModels in ascending order of company name
        public IEnumerable<VenueBaseViewModel> VenueGetAll()
        {
            return mapper.Map<IEnumerable<Venue>, IEnumerable<VenueBaseViewModel>>(ds.Venues.OrderBy(venue => venue.Company));
        }

        // Returns either a VenueBaseModel matching the given id, or null if not found
        public VenueBaseViewModel VenueGetById(int id)
        {
            var venue = ds.Venues.Find(id);

            return venue == null ? null : mapper.Map<Venue, VenueBaseViewModel>(venue);
        }

        // Returns either a VenueBaseModel matching the newly added Venue, or null if the add was not successful
        public VenueBaseViewModel VenueAdd(VenueAddViewModel venue)
        {
            var added = ds.Venues.Add(mapper.Map<VenueAddViewModel, Venue>(venue));
            ds.SaveChanges();

            // Return the new ViewModel because the AddViewModel is pared down and doesn't contain all relevant data
            // required for viewing the newly created Venue.
            return added == null ? null : mapper.Map<Venue, VenueBaseViewModel>(added);
        }

        // Returns either a VenueBaseModel matching the edited Venue, or null if the Venue was not found
        public VenueBaseViewModel VenueEdit(VenueEditViewModel venue)
        {
            var v = ds.Venues.Find(venue.VenueId);

            if (v == null)
            {
                return null;
            } 
            else
            {
                ds.Entry(v).CurrentValues.SetValues(venue);
                ds.SaveChanges();

                return mapper.Map<Venue, VenueBaseViewModel>(v);
            }
        }

        public bool VenueDelete(int id)
        {
            var venue = ds.Venues.Find(id);

            if (venue == null)
            {
                return false;
            }
            else
            {
                ds.Venues.Remove(venue);
                ds.SaveChanges();

                return true;
            }
        }
    }
}