using System;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangfireHotelExampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {

        //  Fire and forget jobs

        [HttpPost("add")]
        public IActionResult Add()
        {
            BackgroundJob.Enqueue(() => SendReservationInfoEmail());
            return Ok();
        }
        
        //  Delayed jobs
        //[HttpPost("checkout")]
        //public IActionResult CheckOut()
        //{
        //    BackgroundJob.Schedule(() => SendCheckOutEmail(), TimeSpan.FromMinutes(30));
        //    return Ok();
        //}


        // Continuations jobs
        [HttpPost("checkout")]
        public IActionResult CheckOut()
        {
            var emailJob = BackgroundJob.Schedule(() => SendCheckOutEmail(), TimeSpan.FromMinutes(30));
            BackgroundJob.ContinueJobWith(emailJob, () => SendCheckOutSms());
            return Ok();
        }

        // Recurring jobs
        [HttpPost("campaign")]
        public IActionResult Campaign()
        {
            RecurringJob.AddOrUpdate(() => SendCampaignEmail(), Cron.Weekly(DayOfWeek.Wednesday,10,00));
            return Ok();
        }


        public void SendReservationInfoEmail()
        {
            //Rezervasyon Bilgi Email Gönderme İşlemleri
        }

        public void SendCheckOutEmail()
        {
            //Çıkış Email Gönderme İşlemleri
        }

        public void SendCampaignEmail()
        {
            //Kampanya Email Gönderme işlemleri
        }

        public void SendCheckOutSms()
        {
            //Çıkış Sms Gönderme İşlemleri
        }

    }
}