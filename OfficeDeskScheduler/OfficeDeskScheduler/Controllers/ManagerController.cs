﻿using Microsoft.AspNetCore.Mvc;
using Services.DataService;
using Services.EntityModels;

namespace OfficeDeskScheduler.Controllers
{
    public class ManagerController : Controller
    {

        private readonly UserDataService userDataService;
        private readonly TeamDataService teamDataService;
        private readonly DeskDataService deskDataService;
        private readonly DeskBookingDataService deskBookingDataService;
        public ManagerController(UserDataService _userDataService, TeamDataService _teamDataService, DeskDataService _deskDataService, DeskBookingDataService _deskBookingDataService)
        {
            userDataService = _userDataService;
            teamDataService = _teamDataService;
            deskDataService = _deskDataService;
            deskBookingDataService = _deskBookingDataService;
        }
        public IActionResult Index()
        {
            long managerId = 4;
            List<Team> teamList = teamDataService.GetAllByManagerId(managerId);
            return View(teamList);
        }

        public async Task<IActionResult> Map()
        {
            return View();
        }
        public async Task<IActionResult> Booking()
        {
            long managerId = 2;
            List<DeskBooking> booking = deskBookingDataService.GetAll(managerId);
            return View(booking);
        }
        [HttpGet]
        public async Task<IActionResult> BookingCreate( )
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookingCreate(DeskBooking deskBooking)
        {
            deskBooking.BookedBy = 2;
            deskBookingDataService.CreateNewDeskBooking(deskBooking);
            return RedirectToAction("Booking", "Manager");
        }


    }
}
