﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdScoreShow.Models
{
    public class UploadCsvFileViewModel
    {
        [Required(ErrorMessage = "A CSV file is required.")]
        [DataType(DataType.Upload)]
        [Display(Name = "Upload a CSV File here: ")]
        public HttpPostedFileBase UpLoadedCsvFile { get; set; }
    }
}