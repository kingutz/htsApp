
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace htsApp.Models
{
    public partial class HTSData:EntityData
    {
        

        [Display(Name = "Name Of HTS Site")]
        public string NameofSite { get; set; }


        [Display(Name = "Name of outreach Site")]
        public string OutreachName { get; set; }


        [Display(Name = "District Name")]
        public string District { get; set; }


        [Column(TypeName = "nvarchar(255)")]
        public string Month { get; set; }


        public int? Year { get; set; }

        [Display(Name = "Outreach Organizer")]
        public string OutreachOrganizer { get; set; }


        [Display(Name = "Public")]
        public bool PublicData { get; set; }


        [Display(Name = "Private")]
        public bool PrivateData { get; set; }

        public bool CSO { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime? DateData { get; set; }

        
        [Display(Name = "Client Code")]
        public string ClientCode { get; set; }


        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Type of Attendance")]
        public int TypeofAttendance { get; set; }

        public int? Age { get; set; }


        [Required(ErrorMessage = "Jaza!")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Residence Shehia")]
        public string ResidenceShehia { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Residence District")]
        public string ResidenceDistrict { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Clients Category")]
        public int ClientsCategory { get; set; }


        [Required(ErrorMessage = "Jaza!")]
        public int Disabled { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "HTC Approach")]
        public int HTCApproach { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Type of Counselling")]
        public int TypeofCounselling { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Reason for testing")]
        public int Reasonfortesting { get; set; }


        [Required(ErrorMessage = "Jaza!")]
        public string Result { get; set; }

        [Display(Name = "Discordant Couple")]
        public string DiscordantCouple { get; set; }

        [Display(Name = "Result Positive type")]
        public string ResultPositiveType { get; set; }


        [Display(Name = "Received result")]
        public string Receivedresult { get; set; }

        [Display(Name = "No of male condoms issued")]
        public int? Noofmalecondomsissued { get; set; }

        [Display(Name = "No of female condoms issued")]
        public int? Nooffemalecondomsissued { get; set; }


        [Display(Name = "Referred to")]
        public string Referredto { get; set; }


       
        [Display(Name = "CTC Number")]
        public string CTCNumber { get; set; }
            

    }
   
}
