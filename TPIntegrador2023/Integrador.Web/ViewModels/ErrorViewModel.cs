﻿using System.ComponentModel.DataAnnotations;

namespace Integrador.Web.ViewModels {
    public class ErrorViewModel {
        public ErrorViewModel (string errorText) {
            Error = errorText;
        }

        [Display(Name = "Error", ResourceType = typeof(Resources.Global))]
        public string Error { get; set; }
    }
}