
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
    using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace htsApp.Models
{
    public partial class HTSData:EntityData
    {
        

        [Display(Name = "HTS site")]
        public string NameofSite { get; set; }


        [Display(Name = "Outreach site")]
        public string OutreachName { get; set; }


        [Display(Name = "District name")]
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

        
        [Display(Name = "Client code")]
        public string ClientCode { get; set; }


        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Type of attendance")]
        public TypeofAttendance TypeofAttendance { get; set; }

        public int? Age { get; set; }


        [Required(ErrorMessage = "Jaza!")]
        public Sex Sex { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Marital status")]
        public MaritalStatus MaritalStatus { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Residence shehia")]
        public string ResidenceShehia { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Residence district")]
        public string ResidenceDistrict { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        public Occupation Occupation { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Clients Category")]
        public ClientCategory ClientsCategory { get; set; }


        [Required(ErrorMessage = "Jaza!")]
        public Disabled Disabled { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "HTC Approach")]
        public HTSApproach HTCApproach { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Type of counselling")]
        public TypeOfCounselling TypeofCounselling { get; set; }

        [Required(ErrorMessage = "Jaza!")]
        [Display(Name = "Reason for testing")]
        public ReasonTesting Reasonfortesting { get; set; }


        [Required(ErrorMessage = "Jaza!")]
        public Result Result { get; set; }

        [Display(Name = "Discordant couple")]
        public DiscordantCouple DiscordantCouple { get; set; }

        [Display(Name = "Result Positive type")]
        public string ResultPositiveType { get; set; }


        [Display(Name = "Received result")]
        public ReceivedResult Receivedresult { get; set; }

        [Display(Name = "No of male condoms issued")]
        public int? Noofmalecondomsissued { get; set; }

        [Display(Name = "No of female condoms issued")]
        public int? Nooffemalecondomsissued { get; set; }


        [Display(Name = "Referred to")]
        public string Referredto { get; set; }

        

        [Display(Name = "CTC number")]
        public string CTCNumber { get; set; }

       

        [Display(Name = "Remarks")]
        public string RemarksName { get; set; }


    }
    
    public enum ClientCategory
    {
        [Display(Name = "1.SU")]
        SU = 1,
        [Display(Name = "2.IDU")]
        IDU = 2,
        [Display(Name = "3.MSM")]
        MSM = 3,
        [Display(Name = "4.FSW")]
        FSW = 4,
        [Display(Name = "6.None of the above")]
        None_of_the_above = 6,
        [Display(Name = "5.Inmate")]
        Inmate = 5
    }

    public enum Disabled 
    {
        [Display(Name = "1.Physical disability")]
        Physical_disability =1,
        [Display(Name = "2.Blind")]
        Blind =2,
        [Display(Name = "3.Dumb")]
        Dumb =3,
        [Display(Name = "4.Deaf")]
        Deaf =4,
        [Display(Name = "5.Mental handicap")]
        Mental_Handicap =5,
        [Display(Name = "6.Albino")]
        Albino =6,
        [Display(Name = "7.None of the above")]
        None_of_the_above =7
    }
    public enum HTSApproach
    {
        [Display(Name = "1.VCT")]
        VCT=1,
        [Display(Name = "2.PITC")]
        PITC=2,
        [Display(Name = "3.Mobile HTC")]
        MobileHTC=3,
        [Display(Name = "4.Home Based HTC")]
        HomeBasedHTC=4
       }
    public enum MaritalStatus
    {
        [Display(Name = "1.Single")]
        Single=1,
        [Display(Name = "2.Married")]
        Married=2,
        [Display(Name = "3.Cohabiting")]
        Cohabiting=3,
        [Display(Name = "4.Divorced")]
        Divorced=4,
        [Display(Name = "5.Widowed")]
        Widowed=5 
        }
    public enum TypeOfCounselling 
    {
        [Display(Name = "1.Individual")]
        Individual=1,
        [Display(Name = "2.Married")]
        Married=2,
        [Display(Name = "3.Unmarried")]
        Unmarried=3,
        [Display(Name = "4.Parent & Child")]
        Parent_Child=4
       
    }
    public enum ReasonTesting 
    {
        [Display(Name = "1.Plan for marriage")]
        Plan_for_marriage=1,
        [Display(Name = "2.Contact or partner HIV positive")]
        Contact_or_partner_HIV_positive=2,
        [Display(Name = "3.TB patient")]
        TBpatient=3,
        [Display(Name = "4.Know HIV status")]
        Know_HIV_status=4,
        [Display(Name = "5.STI patient")]
        STIpatient=5,
        [Display(Name = "6.Medical examination")]
        Medical_examination=7,
        [Display(Name = "8.Pregnancy")]
        Pregnancy=8,
        [Display(Name = "9.PEP")]
        PEP=9,
        [Display(Name = "10.Hepatitis B/C+ Client")]
        Hepatitis_B_C_Client = 10,
        [Display(Name = "11.HIV+ Client")]
        HIVClient = 11,
        [Display(Name = "12.TB Presumptive")]
        TBPresumptive = 12,
        [Display(Name = "13.Partner/Family index case")]
        Partnerindexcase = 13
    }
    public enum Result
    {
        [Display(Name = "1.Negative")]
        Negative=1,
        [Display(Name = "2.Positive")]
        Positive=2,
        [Display(Name = "3.Indeterminant")]
        Indeterminant=3,
        [Display(Name = "4.NotTested")]
        NotTested=4
        }
    public enum Sex
    {
        [Display(Name = "1.Male")]
        Male=1,
        [Display(Name = "2.Female")]
        Female=2,
        [Display(Name = "3.Other")]
        Other=3
    }
    public enum DiscordantCouple
    {
        [Display(Name = "1.Discordant")]
        Discordant=1,
        [Display(Name = "2.Not Applicable")]
        NotApplicable 
    }
    public enum ReceivedResult
    {
        [Display(Name = "1.Yes")]
        Yes=1,
        [Display(Name = "0.No")]
        No=0 
        
        
    }
    public enum TypeofAttendance
    {
        [Display(Name = "1.First Time")]
        FirstTime=1,
        [Display(Name = "2.Return")]
        Return=2,
       
    }
    public enum Occupation
    {
        [Display(Name = "1. Government Employed")]
        GovernmentEmployed=1,
        [Display(Name = "2. Private Employed")]
        PrivateEmployed = 2,
        [Display(Name = "3. Self Employed")]
        SelfEmployed = 3,
        [Display(Name = "4. People with Uniform")]
        PeoplewithUniform = 4,
        [Display(Name = "5. Jobless")]
        Jobless = 5,
        [Display(Name = "6. Student")]
        Student = 6,
        [Display(Name = "7. Child")]
        Child = 7,


    }

}
