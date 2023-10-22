using AutoMapper;
using EventManagement.Application.Interfaces;
using EventManagement.Domain.Entities;
using EventManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Globalization;

namespace EventManagement.Web.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EventController(IUnitOfWork unitOfWork, IMapper mapper, IHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET: EventController

        public ActionResult Index()
        {
            var events = _unitOfWork.EventRepository.GetAll().ToList();

            var modelList = _mapper.Map<List<EventModel>>(events);

            return View(modelList);
        }

        // GET: EventController/Details/5
        public ActionResult Details(int id)
        {
            var entity = _unitOfWork.EventRepository.GetById(id);

            var model = _mapper.Map<EventModel>(entity);

            return View(model);
        }

        // GET: EventController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0) return View(new EventModel { Time = DateTime.Now });

            var entity = _unitOfWork.EventRepository.GetById(id);

            var model = _mapper.Map<EventModel>(entity);

            model.PriceString = entity.Price.ToString();

            return View(model);
        }

        // POST: EventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventModel model)
        {
            try
            {
                var entity = _mapper.Map<Event>(model);

                double.TryParse(model.PriceString, CultureInfo.CurrentCulture, out double price);

                entity.Price = price;

                if (model.ImageFile != null)
                {
                    var originalFilename = Path.GetFileName(model.ImageFile.FileName);
                    string fileId = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(model.ImageFile.FileName);

                    if (SaveImageToLocal(model.ImageFile, fileId))
                        entity.Image = fileId;
                }

                if (!entity.IsPaid)
                    entity.Price = 0;

                if (entity.Id == 0)
                    _unitOfWork.EventRepository.Add(entity);
                else
                    _unitOfWork.EventRepository.Update(entity);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventController/Delete/5
        public ActionResult Delete(int id)
        {
            var entity = _unitOfWork.EventRepository.GetById(id);

            var model = _mapper.Map<EventModel>(entity);

            return View(model);
        }

        // POST: EventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EventModel model)
        {
            try
            {
                _unitOfWork.EventRepository.Delete(model.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool SaveImageToLocal(IFormFile file, string fileName)
        {
            try
            {
                var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "images");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
